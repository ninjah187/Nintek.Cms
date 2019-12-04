using Dapper;
using Marten;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintek.Cms
{
    // rename to store / storage ?
    public class Bank : IDisposable
    {
        public IDocumentSession Session { get; }

        readonly string _connectionString;
        readonly DocumentStore _store;

        readonly Type[] _modelTypes;

        public Bank(string connectionString, Type[] modelTypes)
        {
            _connectionString = connectionString;
            _store = DocumentStore.For(_connectionString);
            _modelTypes = modelTypes;
            Session = _store.LightweightSession();
        }

        public void Dispose()
        {
            Session.Dispose();
            _store.Dispose();
        }

        public async Task<T> FirstOrDefault<T>() where T : Model
        {
            return await Session.Query<T>().FirstOrDefaultAsync();
        }

        public async Task<T> Get<T>(int id) where T : Model
        {
            return await Session.Query<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<T>> GetAll<T>() where T : Model
        {
            return await Session.Query<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<Model>> GetAll(string fullName)
        {
            return await Get<IReadOnlyList<Model>>(fullName, async (connection, tableName, type) =>
            {
                var json = await connection.QueryAsync<string>($"SELECT data FROM {tableName}");
                return json
                    .Select(data => JsonConvert.DeserializeObject(data, type))
                    .Select(obj => (Model) obj)
                    .ToList();
            }, Array.Empty<Model>());
        }

        public async Task<Model> Get(string fullName, int id)
        {
            return await Get(fullName, async (connection, tableName, type) =>
            {
                var json = await connection.QueryFirstOrDefaultAsync<string>(
                    $"SELECT data FROM {tableName} WHERE data->>'Id' = @id",
                    new { id = id.ToString() });
                return (Model) JsonConvert.DeserializeObject(json, type);
            });
        }

        public async Task<T> Get<T>(string fullName, Func<NpgsqlConnection, string, Type, Task<T>> getter, T notExistsValue = default)
        {
            var type = _modelTypes.FirstOrDefault(x => string.Equals(x.FullName, fullName, StringComparison.InvariantCultureIgnoreCase));
            ValidateModelType(type);
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var tableName = ModelMeta.GetTableName(type.FullName);
                if (!await TableExists(connection, tableName))
                {
                    return notExistsValue;
                }
                return await getter(connection, tableName, type);
            }
        }

        public Bank Delete<T>(int id) where T : Model
        {
            Session.Delete<T>(id);
            return this;
        }

        public Bank Save<T>(params T[] models) where T : Model
        {
            Session.Store(models);
            return this;
        }

        public Model Save(string slug, int id, Dictionary<string, string> model)
        {
            var (type, modelObject) = CreateBySlug(slug);
            modelObject.Id = id;
            foreach (var field in model)
            {
                var property = type.GetProperty(field.Key);
                property.SetValue(modelObject, field.Value);
            }
            Session.Store<object>(modelObject);
            return modelObject;
        }

        public Bank Save<T>(params IEnumerable<T>[] models) where T : Model
        {
            foreach (var enumerable in models)
            {
                foreach (var item in enumerable)
                {
                    Session.Store(item);
                }
            }
            return this;
        }

        public Model Create(string slug) => CreateBySlug(slug).model;

        public async Task Commit()
        {
            await Session.SaveChangesAsync();
        }

        (Type type, Model model) CreateBySlug(string slug)
        {
            var meta = GetMetaBySlug(slug);
            var type = _modelTypes.FirstOrDefault(x => x.FullName == meta.FullName);
            ValidateModelType(type);
            var model = (Model) Activator.CreateInstance(type);
            return (type, model);
        }

        async Task<bool> TableExists(IDbConnection connection, string tableName)
        {
            return await connection.ExecuteScalarAsync<bool>(
                "SELECT EXISTS (" +
                "  SELECT  1 " +
                "  FROM    information_schema.tables  " +
                "  WHERE   table_schema = 'public' " +
                "  AND     table_name = @tableName" +
                ");", new { tableName });
        }

        ModelMeta GetMetaBySlug(string slug)
        {
            return Session.Query<ModelMeta>().FirstOrDefault(x => x.Slug == slug);
        }

        static void ValidateModelType(Type type)
        {
            if (type == null)
            {
                throw new CmsException($"No model type of {type.FullName} found.");
            }
            if (!type.IsSubclassOf(typeof(Model)))
            {
                throw new CmsException($"{type.FullName} is not model type.");
            }
        }
    }
}
