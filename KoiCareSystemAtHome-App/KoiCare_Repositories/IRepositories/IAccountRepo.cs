using Business_Object.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_Repositories.IRepositories
{
    public interface IAccountRepo
    {
        public List<AccountTbl> GetAccounts();
        public AccountTbl GetAccount(int id);
        public bool UpdateAccount(AccountTbl accountTbl);
    }
}
