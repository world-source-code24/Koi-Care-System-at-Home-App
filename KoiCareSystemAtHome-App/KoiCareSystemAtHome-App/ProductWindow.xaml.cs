using Business_Object.Models;
using KoiCare_Repositories;
using KoiCare_Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private KoicareathomeContext _context;
        private readonly IProductRepository productRepo;
        private readonly ProductsTbl products;

        private ObservableCollection<ProductsTbl> _product;
        private ICollectionView _filteredMembers;
        private readonly int accId;


        public string FilterText { get; set; }

        public ProductWindow(int accId)
        {
            InitializeComponent();
            productRepo = new ProductRepository();
            DataContext = this;
            _product = new ObservableCollection<ProductsTbl>();
            _filteredMembers = CollectionViewSource.GetDefaultView(_product);
            this.accId = accId;
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private bool IsMaximized = false;

        public event PropertyChangedEventHandler? PropertyChanged;

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

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private async void Window_Loaded()
        {

            var productsFromDb = productRepo.GetAllProducts(); // Assuming GetProducts() is asynchronous

            var dataGridProducts = productsFromDb.Select(p => new
            {
                p.ProductId,
                p.Name,
                p.Price,
                p.ProductInfo,

            }).ToList();


            // Populate the _product collection with the fetched data
            _product.Clear();  // Clear the existing collection before adding new data
            foreach (var product in productsFromDb)
            {
                _product.Add(product); // Add each fetched product to the ObservableCollection
            }

            // Set the filtered members to the DataGrid
            ProductDataGrid.ItemsSource = dataGridProducts;
        }


        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Window_Loaded();
        }


        private void FilterMembers()
        {
            if (!string.IsNullOrEmpty(FilterText))
            {
                // Apply filter to show only Koi items whose name contains FilterText (case-insensitive)
                _filteredMembers.Filter = item =>
                {
                    var product = item as ProductsTbl;
                    return product != null && product.Name.Contains(FilterText, StringComparison.OrdinalIgnoreCase);
                };
            }
            else
            {
                // Clear filter and show all items
                _filteredMembers.Filter = null;
            }
            ProductDataGrid.ItemsSource = _filteredMembers;
        }

        private void FilterTextChanged(object sender, TextChangedEventArgs e)
        {
            // Call the filter method whenever the TextBox text changes
            FilterMembers();
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            //var product = clickedButton?.DataContext as ProductsTbl;



            if (clickedButton != null)
            {    
                int productId = (int)clickedButton.CommandParameter;

                CartRepo cartRepo = new CartRepo();
                if (cartRepo.AddProductToCart(accId, productId,1))
                {
                    MessageBox.Show("Product added to cart!");

                }
                else
                {
                    MessageBox.Show("Failed to add product to cart.");
                }
            }
        }




        private void YourCart_Click(object sender, RoutedEventArgs e)
        {
            int accId = 1;
            CartWindow cartWindow = new CartWindow(accId);
            cartWindow.Show();
            this.Close();
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

        private void Water_Click(object sender, RoutedEventArgs e)
        {
            WaterParaWindow waterParaWindow = new WaterParaWindow(accId);
            waterParaWindow.Show();
            this.Close();
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow(accId);
            homeWindow.Show();
            this.Close();
        }
        private void Product_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow product = new ProductWindow(accId);
            product.Show();
            this.Close();
        }
    }
}
