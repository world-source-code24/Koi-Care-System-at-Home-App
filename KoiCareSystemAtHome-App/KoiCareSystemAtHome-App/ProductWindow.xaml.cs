using Business_Object.Models;
using KoiCare_Repositories;
using Microsoft.EntityFrameworkCore;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace KoiCareSystemAtHome_App
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window, INotifyPropertyChanged
    {

        private KoicareathomeContext _context;
        private readonly ProductRepo productRepo;
        private readonly ProductsTbl products;

        private ObservableCollection<ProductsTbl> _product;
        private ICollectionView _filteredMembers;


        public string FilterText { get; set; }

        public ProductWindow()
        {
            InitializeComponent();
            productRepo = new ProductRepo();
            DataContext = this;
            _product = new ObservableCollection<ProductsTbl>();
            _filteredMembers = CollectionViewSource.GetDefaultView(_product);

            //_context = new KoicareathomeContext();
            

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
            
            var productsFromDb = productRepo.GetProducts(); // Assuming GetProducts() is asynchronous

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


        private void Home_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            //var product = clickedButton?.DataContext as ProductsTbl;

            

            if (clickedButton != null)
            {
                // Assume accId is obtained from the logged-in user's information
                int accId = 1;  // Implement this method to get the current account ID
                int productId = (int)clickedButton.CommandParameter;

                CartRepo cartRepo = new CartRepo();
                if (cartRepo.AddToCart(accId, productId))
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
    }
}
