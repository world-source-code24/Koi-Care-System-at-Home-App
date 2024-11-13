using Business_Object.Models;
using KoiCare_Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private readonly int accId;

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total => Quantity * Price;

        public decimal TotalPrice { get; set; } = 0;



        public CartWindow(int accId)
        {
            InitializeComponent();
            DataContext = this;
            this.accId = accId;
            cartRepo = new CartRepo();


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
        private void Water_Click(object sender, RoutedEventArgs e)
        {
            WaterParaWindow waterParaWindow = new WaterParaWindow(accId);
            waterParaWindow.Show();
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
        private void Product_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow productWindow = new ProductWindow(accId);
            productWindow.Show();
            this.Close();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow(accId);
            homeWindow.Show();
            this.Close();
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



        private void LoadCartItems()
        {

            var cartsFromDb = cartRepo.GetCartItems(accId);

            if (cartsFromDb == null)
            {
                MessageBox.Show("No cart items found.");
                TotalPrice = 0;
                NotifyPropertyChanged(nameof(TotalPrice));
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



            CartDataGrid.ItemsSource = dataGridCarts;
            TotalPrice = dataGridCarts.Sum(c => c.Total);
            txtTotalPrice.Text = TotalPrice.ToString();
            NotifyPropertyChanged(nameof(TotalPrice));
        }


        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCartItems();
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
                    if (cartRepo.RemoveProductFromCart(accId, productId))
                    {
                        LoadCartItems();
                    }
                    else
                    {
                        MessageBox.Show("Failed delete product to cart!!!");
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void btnCheckout_Click(object sender, RoutedEventArgs e)
        {
            var cartItems = cartRepo.GetCartItems(accId);


            if (cartItems == null || cartItems.Count == 0)
            {
                MessageBox.Show("Your cart is empty. Please add items to your cart before proceeding.", "Empty Cart", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            decimal totalAmount = TotalPrice;


            var confirmationResult = MessageBox.Show(
                $"Your total amount is {totalAmount:C}. Do you want to proceed with the checkout?",
                "Confirm Checkout",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (confirmationResult == MessageBoxResult.Yes)
            {
                bool paymentSuccess = ProcessPayment(totalAmount);

                if (paymentSuccess)
                {

                    cartRepo.ClearCart(accId);
                    LoadCartItems();

                    MessageBox.Show("Payment successful! Thank you for your purchase.", "Payment Success", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    MessageBox.Show("Payment failed. Please try again later.", "Payment Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }


        private bool ProcessPayment(decimal totalAmount)
        {
            try
            {
                if (totalAmount <= 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Payment processing error: {ex.Message}");
                return false;
            }
        }

      
    }
}