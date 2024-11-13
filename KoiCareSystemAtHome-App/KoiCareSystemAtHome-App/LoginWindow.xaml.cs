using Business_Object;
using KoiCare_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KoiCareSystemAtHome_App
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private IAccountRepo accountRepo;
        public LoginWindow()
        {
            InitializeComponent();
            accountRepo = new AccountRepo();
        }

        private void btn_Login(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;
            List<AccountTbl> accounts = accountRepo.GetAccounts();
            var account = accounts.FirstOrDefault(a => a.Email.Equals(email));
            if (account != null)
            {
                if (!account.Status)
                {
                    if (account.Password.Equals(password))
                    {
                        string role = account.Role;
                        if (role.Equals("guest", StringComparison.OrdinalIgnoreCase) || role.Equals("member", StringComparison.OrdinalIgnoreCase))
                        {
                            ProfileWindow profileWindow = new ProfileWindow(account.AccId);
                            profileWindow.Show();
                            Close();
                        }
                        else if (role.Equals("admin", StringComparison.OrdinalIgnoreCase))
                        {

                        }
                        else MessageBox.Show("You don't have permission to use this function!");
                    }
                    else MessageBox.Show("Wrong password!");
                }
                else MessageBox.Show("Wrong email!");
            }
            else MessageBox.Show("Your account not exxist!");
        }
    }
}
