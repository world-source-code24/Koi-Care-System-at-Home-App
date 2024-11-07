using Business_Object.Models;
using KoiCare_Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {

        private KoicareathomeContext _context;
        private readonly CartRepo cartRepo;
        private readonly CartTbl cart;
        private readonly int _currentAccId;

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total => Quantity * Price;



        public CartWindow(int accId)
        {
            InitializeComponent();
            DataContext = this;
            _currentAccId = accId;
            cartRepo = new CartRepo();


        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            
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



        private async void Window_Loaded()
        {

            var cartsFromDb = cartRepo.GetCartItems(_currentAccId);

            if (cartsFromDb == null)
            {
                MessageBox.Show("No cart items found.");
                return;
            }

            var dataGridCarts = cartsFromDb.Select(c => new
            {
                c.ProductId,
                c.Product?.Name,  
                c.Quantity,
                Price = c.Product?.Price ?? 0,  
                Total = c.Quantity * (c.Product?.Price ?? 0),

            }).ToList();


            

            // Set the filtered members to the DataGrid
            CartDataGrid.ItemsSource = dataGridCarts;
        }


        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Window_Loaded();
        }



        private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {

                MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to remove this product from your cart?", 
                "Confirm deletion",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    int accId = 1;
                    int productId = (int)clickedButton.CommandParameter;
                    if (cartRepo.RemoveFromCart(accId, productId))
                    {
                        Window_Loaded();
                    }
                    else
                    {
                        MessageBox.Show("Failed delete product to cart!!!");
                    }
                }
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FilterTextChanged(object sender, TextChangedEventArgs e)
        {

        }



    }
}
