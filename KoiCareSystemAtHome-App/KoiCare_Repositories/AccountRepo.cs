using Business_Object.Models;
using KoiCare_DAOs;
using KoiCare_Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_Repositories
{
    public class AccountRepo : IAccountRepo
    {
        public List<AccountTbl> GetAccounts()
        {
            return AccountDAO.Instance.GetAccounts();
        }
        public AccountTbl GetAccount(int id)
        {
            return AccountDAO.Instance.GetAccount(id);
        }
        public bool UpdateAccount(AccountTbl accountTbl)
        {
            return AccountDAO.Instance.UpdateAccount(accountTbl);
        }
    }
}
