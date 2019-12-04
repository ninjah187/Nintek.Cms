using System;
using System.Threading.Tasks;

namespace Nintek.Cms.Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var bank = new Bank(null, null);
            var product = new Product();
            await bank.Save(product).Commit();
        }
    }
}
