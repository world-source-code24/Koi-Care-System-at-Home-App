using Business_Object;
using KoiCare_DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_Repositories
{
    public class AccountRepo : IAccountRepo
    {
        public bool CreateAccount(AccountTbl accountTbl)
        {
            return AccountDAO.Instance.CreateAccount(accountTbl);
        }

        public AccountTbl GetAccount(int id)
        {
            return AccountDAO.Instance.GetAccount(id);
        }

        public List<AccountTbl> GetAccounts()
        {
            return AccountDAO.Instance.GetAccounts();
        }

        public bool UpdateAccount(AccountTbl account)
        {
            return AccountDAO.Instance.UpdateAccount(account);
        }
    }
}
