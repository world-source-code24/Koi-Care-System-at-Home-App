using Business_Object.Models;
using KoiCare_Repositories;
using KoiCare_Repositories.IRepositories;
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
        private readonly IAccountRepo accountRepo;
        public LoginWindow()
        {
            InitializeComponent();
            accountRepo = new AccountRepo();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;
            List<AccountTbl> accounts = accountRepo.GetAccounts();
            var account = accounts.OrderByDescending(a => a.AccId).FirstOrDefault(a => a.Email == email && a.Status == true);
            if (account != null)
            {
                
                
                    if (account.Password.Equals(password))
                    {
                        HomeWindow homeWindow = new HomeWindow(account.AccId);
                        homeWindow.Show();
                        this.Close();
                    }
                    else MessageBox.Show("You don't have permission to use this function!");
            }
            else MessageBox.Show("Your account not exxist!");
        }



        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private bool IsMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;
                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximized = true;
                }
            }

        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
