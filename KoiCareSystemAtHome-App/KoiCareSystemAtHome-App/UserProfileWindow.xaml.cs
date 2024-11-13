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
    /// Interaction logic for UserProfileWindow.xaml
    /// </summary>
    public partial class UserProfileWindow : Window
    {
        private readonly int accId;
        private readonly IAccountRepo accountRepo;
        public UserProfileWindow(int accId)
        {
            InitializeComponent();
            this.accId = accId;
            accountRepo = new AccountRepo();
            SaveDataFromDB(accId);
        }
        
        private void SaveDataFromDB(int accId)
        {
            var account = accountRepo.GetAccount(accId);
            if (account != null )
            {
                txtFullName.Text = account.Name;
                txtAddress.Text = account.Address;
                txtPhone.Text = account.Phone; 
                email.Text = account.Email;

            }
        }

        private void SaveDataToDB(AccountTbl account)
        {
            
                account.Name = txtFullName.Text;
                account.Address = txtAddress.Text;
                account.Phone = txtPhone.Text;
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Handle mouse down event for border
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Allow window drag and move without the title bar
            this.DragMove();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow(accId);
            homeWindow.Show();
            this.Close();
        }

        private void KoiManagement_Click(object sender, RoutedEventArgs e)
        {
            KoiWindow koiWindow = new KoiWindow(accId);
            koiWindow.Show();
            this.Close();
        }

        private void PondMonitor_Click(object sender, RoutedEventArgs e)
        {
            PondWindow pondWindow = new PondWindow(accId);
            pondWindow.Show();
            this.Close();
        }
        private void Water_Click(object sender, RoutedEventArgs e)
        {
            WaterParaWindow waterParaWindow = new WaterParaWindow(accId);
            waterParaWindow.Show();
            this.Close();
        }
        private void Product_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow product = new ProductWindow(accId);
            product.Show();
            this.Close();
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Logic for handling the button click event
        }

        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            CartWindow cartWindow = new CartWindow(accId);
            cartWindow.Show();
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var account = accountRepo.GetAccount(accId);
            if (account != null)
            {
                SaveDataToDB(account);
                bool result = accountRepo.UpdateAccount(account);
                if (result)
                {
                    MessageBox.Show("Update successfully!!");
                }
                else
                {
                    MessageBox.Show("An error occured when processing!");
                }
            }
        }
    }
}
  