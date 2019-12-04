using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nintek.Cms
{
    public interface IRepository<T> where T : Model
    {
        Task<T> Get(int id);
        Task<T[]> GetAll();
    }
}
