using Business_Object;
using Microsoft.EntityFrameworkCore;
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

        public AccountTbl GetAccount(int id)
        {
            return appContext.AccountTbls.FirstOrDefault(a => a.AccId.Equals(id));
        }

        public bool UpdateAccount(AccountTbl account)
        {
            try
            {
                var KoiAccount = GetAccount(account.AccId);
                if (KoiAccount != null)
                {
                    appContext.Entry<AccountTbl>(KoiAccount).State = EntityState.Modified;
                    appContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool CreateAccount(AccountTbl accountTbl)
        {
            bool result = false;
            try
            {
                appContext.AccountTbls.Add(accountTbl);
                appContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating account: {ex.Message}");
                result = false;
            }
            return result;
        }
    }
}
