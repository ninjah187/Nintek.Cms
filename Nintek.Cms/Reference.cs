using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nintek.Cms
{
    public class Reference<TModel> where TModel : Model
    {
        readonly Func<TModel, bool> _join;

        public Reference(Func<TModel, bool> join)
        {
            _join = join;
        }

        public TModel FirstOrDefault(Bank bank)
        {
            return bank
                .Session
                .Query<TModel>()
                .FirstOrDefault(_join);
        }

        public IReadOnlyList<TModel> ToList(Bank bank)
        {
            return bank
                .Session
                .Query<TModel>()
                .Where(_join)
                .ToList();
        }
    }
}
