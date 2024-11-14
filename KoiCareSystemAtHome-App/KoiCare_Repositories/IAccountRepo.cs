using Business_Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_Repositories
{
    public interface IAccountRepo
    {
        public List<AccountTbl> GetAccounts();
        public AccountTbl GetAccount(int id);
        public bool UpdateAccount(AccountTbl account);
        public bool CreateAccount(AccountTbl accountTbl);
    }
}
