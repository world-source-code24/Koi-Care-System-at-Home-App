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
using System.Xml.Linq;

namespace KoiCareSystemAtHome_App
{
    /// <summary>
    /// Interaction logic for UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Window
    {
        private int accId = 0;
        private readonly IAccountRepo accountRepo;
        //private readonly IJobPostingService jobService;
        public UserProfile(int accId)
        {
            InitializeComponent();
            accountRepo = new AccountRepo();
            this.accId = accId;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Load user profile when the window loads
            var account = accountRepo.GetAccount(accId);
            if (account != null)
            {
                // Assuming there are TextBox controls in your XAML to display profile information
                txtFullName.Text = account.Name;
                txtEmail.Text = account.Email;
                txtPhone.Text = account.Phone;
                // Populate other profile fields as needed
            }
            else
            {
                MessageBox.Show("Failed to load user profile.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        private void WaterParameter_Click(object sender, RoutedEventArgs e)
        {
            WaterParaWindow waterParaWindow = new WaterParaWindow(accId);
            waterParaWindow.Show();
            this.Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Edit Profile
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Update profile logic
            var account = accountRepo.GetAccount(accId);
            if (account != null)
            {
                // Update the account properties based on input fields
                account.Name = txtFullName.Text;
                account.Email = txtEmail.Text;
                account.Phone = txtPhone.Text;

                try
                {
                    accountRepo.UpdateAccount(account);
                    MessageBox.Show("Profile updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to update profile. Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Profile not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Edit Profile Page
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
