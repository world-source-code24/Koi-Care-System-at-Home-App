using Business_Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_DAOs
{
    public class AccountDAO
    {
        private KoiCareSystemAppContext appContext;
        private static AccountDAO instance;
        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance;
            }
        }

        public AccountDAO()
        {
            appContext = new KoiCareSystemAppContext();
        }

        public List<AccountTbl> GetAccounts()
        {
            return appContext.AccountTbls.ToList();
        }
    }
}
