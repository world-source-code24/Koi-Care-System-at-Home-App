using Business_Object.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_DAOs
{
    public class AccountDAO
    {
        private KoicareathomeContext appContext;
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
            appContext = new KoicareathomeContext();
        }

        public List<AccountTbl> GetAccounts()
        {
            return appContext.AccountTbls.ToList();
        }

        public bool UpdateAccount(AccountTbl accountTbl)
        {
            bool result = false;
            try
            {
                appContext.AccountTbls.Update(accountTbl);
                appContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public AccountTbl GetAccount(int id)
        {
            return appContext.AccountTbls.SingleOrDefault(x => x.AccId == id);
        }
    }
}
