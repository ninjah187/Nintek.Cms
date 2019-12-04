using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintek.Cms
{
    public class ModelMeta : Model
    {
        public string FullName { get; }
        public string Slug { get; }
        public FieldMeta[] Fields { get; }

        public ModelMeta(string fullName, FieldMeta[] fields)
        {
            FullName = fullName;
            Slug = GetSlug(fullName);
            Fields = fields;
        }

        [JsonConstructor]
        public ModelMeta(string fullName, string slug, FieldMeta[] fields)
        {
            FullName = fullName;
            Slug = slug;
            Fields = fields;
        }

        public string GetShortName()
        {
            return FullName.Split('.').Last();
        }

        public static string GetSlug(string fullName)
        {
            return fullName
                .Split('.')
                .Last()
                .Aggregate("", (slug, character) =>
                {
                    if (char.IsUpper(character))
                    {
                        var lower = char.ToLower(character);
                        if (slug.Length == 0)
                        {
                            return slug + lower;
                        }
                        else
                        {
                            return $"{slug}-{lower}";
                        }
                    }
                    else
                    {
                        return slug + character;
                    }
                });
        }

        public static string GetTableName(string name)
        {
            return "mt_doc_" + name
                .Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries)
                .Last()
                .ToLower();
        }
    }
}
