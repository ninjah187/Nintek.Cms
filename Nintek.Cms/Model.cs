using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintek.Cms
{
    public abstract class Model : Model<int>
    {
        public Model()
        {
        }

        public Model(int id) : base(id)
        {
        }
    }

    public abstract class Model<TId>
    {
        public TId Id { get; set; }

        public Model()
        {
        }

        public Model(TId id)
        {
            Id = id;
        }

        public virtual IEnumerable<(string key, object value)> Entries()
        {
            yield return (nameof(Id), Id);
            var entries = GetType()
                .GetProperties()
                .Where(property => property.Name != nameof(Id))
                .Select(property => (property.Name, property.GetValue(this)));
            foreach (var entry in entries)
            {
                yield return entry;
            }
        }
    }
}
