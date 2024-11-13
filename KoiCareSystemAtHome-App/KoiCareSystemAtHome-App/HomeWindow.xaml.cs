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
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private readonly int accId;
        private readonly IProductRepository productRepository;
        public HomeWindow(int accId)
        {
            InitializeComponent();
            RoyalIntro.Text = "Is a web-app that helps users proactively manage and provide the best assessments so that users can take good care of Koi fish.\r\n\r\nIn addition, we also provide products that help users take the best care of Koi fish from fish care accessories from leading countries in the world such as the US, Japan,... and foods that help increase color and size come from the US, Vietnam,... with the aim of supporting users in managing Koi fish ponds at home.";
            this.accId = accId;
            productRepository = new ProductRepository();
            Window_Loaded();
        }

        private  void Window_Loaded()
        {
            var products = productRepository.GetAllProducts().Select(p => new 
            {
               p.ProductId,
               p.Name,
               p.Price,
               p.Category,
               p.Stock
            });

            ProductGrid.ItemsSource = products;
            
        }

        private void DetailButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            UserProfileWindow userProfileWindow = new UserProfileWindow(accId);
            userProfileWindow.Show();
            this.Close();
        }

        private void PondMonitor_Click(object sender, RoutedEventArgs e)
        {
            PondWindow pondWindow = new PondWindow(accId);
            pondWindow.Show();
            this.Close();
        }

        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            CartWindow cartWindow = new CartWindow(accId);
            cartWindow.Show();
            this.Close();
        }
        private void KoiManagement_Click(object sender, RoutedEventArgs e)
        {
            KoiWindow koiWindow = new KoiWindow(accId);
            koiWindow.Show();
            this.Close();
        }


        private void Home_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow(accId);
            homeWindow.Show();
            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
           
            if (clickedButton == btn_detail)
            {
                ChangUI(2);
            }
            else if (clickedButton == btn_list)
            {
                ChangUI(1);
            }
        }
        private void ChangUI(int numUI)
        {
            switch (numUI)
            {
                case 1:
                    ChangeUIWhenClickButton(btn_list, btn_detail);
                    
                    gridHome.Visibility = Visibility.Visible;
                    break;
                case 2:
                    ChangeUIWhenClickButton(btn_detail,btn_list);
                    gridHome.Visibility = Visibility.Collapsed;
                    
                    break;
            }
        }

        private void ChangeUIWhenClickButton(Button clickButton, Button button)
        {
            clickButton.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#db4b2e"));
            button.BorderBrush = Brushes.Transparent;
            
        }
        
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

    }
}
