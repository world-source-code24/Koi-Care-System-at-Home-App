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
    /// Interaction logic for UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Window
    {
        private int accId;
        public UserProfile()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Add initialization logic here
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

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Logic for handling the button click event
        }

    }
}
