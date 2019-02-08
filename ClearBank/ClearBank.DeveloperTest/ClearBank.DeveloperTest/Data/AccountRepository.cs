using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearBank.DeveloperTest.Data
{
    public class AccountRepository : IAccountRepository
    {
        public IAccountDataStore GetDataStore()
        {
            var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];

            if (dataStoreType == "Backup")
            {
                return new BackupAccountDataStore();
            }
            else
            {
                return  new AccountDataStore();
            }
        }
    }
}
