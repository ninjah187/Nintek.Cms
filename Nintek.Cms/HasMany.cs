using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintek.Cms
{
    public class HasMany<T> where T : Model
    {
        readonly Func<T, bool> _join;

        public HasMany(Func<T, bool> join)
        {
            _join = join;
        }

        public IEnumerable<T> Load(Bank bank)
        {
            return bank.Session.Query<T>().Where(_join);
        }
    }
}
