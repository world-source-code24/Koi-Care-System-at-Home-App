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
        public List<AccountTbl> GetAccounts()
        {
            return AccountDAO.Instance.GetAccounts();
        }
    }
}
