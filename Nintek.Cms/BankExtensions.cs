using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nintek.Cms
{
    public static class BankExtensions
    {
        public static async Task SaveAndCommit<T>(this Bank bank, params T[] models)
            where T : Model
        {
            bank.Save(models);
            await bank.Commit();
        }
    }
}
