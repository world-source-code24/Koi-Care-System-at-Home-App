using Business_Object.Models;
using KoiCare_Repositories.IRepositories;
using KoiCare_Repositories;
using MahApps.Metro.IconPacks;
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
using System.Reflection.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KoiCareSystemAtHome_App
{
    /// <summary>
    /// Interaction logic for PondWindow.xaml
    /// </summary>
    public partial class WaterParaWindow : Window, INotifyPropertyChanged
    {
        private readonly IWaterParameterRepo waterRepo;
        private readonly WaterParametersTbl waters;
        private int accId;
        private bool _isEditMode;
        public bool IsEditMode
        {
            get => _isEditMode;
            set
            {
                if (_isEditMode != value)
                {
                    _isEditMode = value;
                    OnPropertyChanged(nameof(IsEditMode));  // Notify the UI about the change
                }
            }
        }
        private int _waterId;
        public int WaterId
        {
            get => _waterId;
            set
            {
                if (_waterId != value)
                {
                    _waterId = value;
                }
            }
        }
        private ObservableCollection<WaterParametersTbl> _water;
        private ICollectionView _filteredMembers;
        private readonly IPondRepository pondRepository;
        public string FilterText { get; set; }
        

        public WaterParaWindow(int accId)
        {
            InitializeComponent();
            waterRepo = new WaterParameterRepo();
            DataContext = this;
            IsEditMode = false;
            this.accId = accId;
            _water = new ObservableCollection<WaterParametersTbl>();
            _filteredMembers = CollectionViewSource.GetDefaultView(_water);
            pondRepository = new PondRepository();
        }

        private void FilterMembers()
        {
            if (!string.IsNullOrEmpty(FilterText))
            {
                // Apply filter to show only Koi items whose name contains FilterText (case-insensitive)
                _filteredMembers.Filter = item =>
                {
                    var pond = item as PondsTbl;
                    return pond != null && pond.Name.Contains(FilterText, StringComparison.OrdinalIgnoreCase);
                };
            }
            else
            {
                // Clear filter and show all items
                _filteredMembers.Filter = null;
            }
            WaterDataGrid.ItemsSource = _filteredMembers;
        }

        private void FilterTextChanged(object sender, TextChangedEventArgs e)
        {
            // Call the filter method whenever the TextBox text changes
            FilterMembers();
        }

        private async void Window_Loaded()
        {
            // Fetch koi data asynchronously
            var watersFromDB = waterRepo.GetParametersByUserId(accId);
            // Project the KoisTbl objects into an anonymous type containing only the desired properties
            var dataGridKois = watersFromDB.Select(w => new
            {
                w.ParameterId,
                w.Salt,
                w.Temperature,
                w.Po4Level,
                w.No3Level,
                w.O2Level,
                w.No2Level,
                w.Date,
                w.TotalChlorines,
                w.PhLevel,
            }).ToList();

            // Populate the _koi collection with the fetched data
            _water.Clear();  // Clear the existing collection before adding new data
            foreach (var koi in watersFromDB)
            {
                _water.Add(koi); // Add each fetched koi to the ObservableCollection
            }
            var ponds = pondRepository.GetPondsByUserId(accId);
            // Set the filtered members to the DataGrid
            WaterDataGrid.ItemsSource = dataGridKois;
            cbPond.ItemsSource = ponds;
            cbPond.DisplayMemberPath = "Name";
            cbPond.SelectedValuePath = "PondId";
        }


        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Window_Loaded();
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

        private void Button_ClickPond(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton == btn_add || clickedButton == btn_add1)
            {
                ChangUI(3);
            }
            else if (clickedButton == btn_detail)
            {
                ChangUI(2);
            }
            else if (clickedButton == btn_list)
            {
                ChangUI(1);
            }
        }

        private void DetailButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                // Access the DataContext of the button (which should be the Koi object)
                int parameterId = (int)clickedButton.CommandParameter;

                var water = waterRepo.GetParameterById(parameterId);
                if (water != null)
                {
                    txtPondName.Text = water.Pond.Name;
                    _waterId = parameterId;
                    updateWaterToWindow(water);
                    ChangUI(2);
                }
                else
                {
                    MessageBox.Show("An error occured!");
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the button that was clicked
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                // Access the DataContext of the button (which should be the Koi object)
                int waterId = (int)clickedButton.CommandParameter;

                if (waterRepo.DeleteParameter(waterId))
                {
                    Window_Loaded();
                    ChangUI(1);

                }
                else
                {
                    MessageBox.Show("An error occured!");
                }
            }
        }

        

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow(accId);
            homeWindow.Show();
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
        private void Product_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow product = new ProductWindow(accId);
            product.Show();
            this.Close();
        }
        private void Water_Click(object sender, RoutedEventArgs e)
        {
            WaterParaWindow waterParaWindow = new WaterParaWindow(accId);
            waterParaWindow.Show();
            this.Close();
        }
        

        private void AddKoi_Click(object sender, RoutedEventArgs e)
        {
            var newWater = saveWaterToDb();
            if (waterRepo.AddParameter(newWater))
            {
                CleanAddForm();
                Window_Loaded();
                ChangUI(1);
            }
        }
        private void CancelAddKoi_Click(object sender, RoutedEventArgs e)
        {
            CleanAddForm();
        }



        private void NumberOnly(object sender, TextCompositionEventArgs e)
        {
            // Allow only digits and one decimal point
            e.Handled = !decimal.TryParse(((TextBox)sender).Text + e.Text, out _);
        }

        private void EditKoi_Click(object sender, RoutedEventArgs e)
        {
            if (IsEditMode)
            {
                IsEditMode = false;
                btnEdit.Text = "Edit";
                iconEdit.Kind = PackIconMaterialKind.ContentSave;
                //Logic update koi
                var updateWater = waterRepo.GetParameterById(_waterId);
                updateWaterToDb(updateWater);
                if (waterRepo.UpdateParameter(updateWater))
                {
                    updateWaterToWindow(updateWater);
                    Window_Loaded();
                }
            }
            else
            {
                IsEditMode = true;
                btnEdit.Text = "Save";
                iconEdit.Kind = PackIconMaterialKind.ApplicationEdit;


            }
        }
        private void CancelEditKoi_Click(object sender, RoutedEventArgs e)
        {
            IsEditMode = false;
            btnEdit.Text = "Edit";
            iconEdit.Kind = PackIconMaterialKind.ContentSave;
            updateWaterToWindow(waterRepo.GetParameterById(_waterId));
        }




        //Chang UI section

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ChangeUIWhenClickButton(Button clickButton, Button button, Button button1)
        {
            clickButton.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#db4b2e"));
            button.BorderBrush = Brushes.Transparent;
            button1.BorderBrush = Brushes.Transparent;
        }
        private void ChangUI(int numUI)
        {
            switch (numUI)
            {
                case 1:
                    ChangeUIWhenClickButton(btn_list, btn_detail, btn_add);
                    KoiDetailGrid.Visibility = Visibility.Collapsed;
                    NewKoiEntryGrid.Visibility = Visibility.Collapsed;
                    WaterDataGrid.Visibility = Visibility.Visible;
                    break;
                case 2:
                    ChangeUIWhenClickButton(btn_detail, btn_add, btn_list);
                    WaterDataGrid.Visibility = Visibility.Collapsed;
                    NewKoiEntryGrid.Visibility = Visibility.Collapsed;
                    KoiDetailGrid.Visibility = Visibility.Visible;
                    break;
                case 3:
                    ChangeUIWhenClickButton(btn_add, btn_detail, btn_list);
                    KoiDetailGrid.Visibility = Visibility.Collapsed;
                    WaterDataGrid.Visibility = Visibility.Collapsed;
                    NewKoiEntryGrid.Visibility = Visibility.Visible;
                    break;

            }
        }

        // End Change UI section
        // Addition Function
        private void updateWaterToWindow(WaterParametersTbl water)
        {
            _waterId = water.ParameterId;
            txtTemperature.Text = water.Temperature.ToString();
            txtSalt.Text = water.Salt.ToString();
            txtPHlevel.Text = water.PhLevel.ToString();
            txtO2level.Text = water.O2Level.ToString();
            txtNO2level.Text = water.No2Level.ToString();
            txtNO3level.Text = water.No3Level.ToString();
            txtPO4level.Text = water.Po4Level.ToString();
            txtTotalChlorines.Text = water.TotalChlorines.ToString();
            txtDate.SelectedDate = water.Date;

        }
        private void updateWaterToDb(WaterParametersTbl water)
        {

            water.Temperature = decimal.TryParse(txtTemperature.Text, out var temp) ? temp : (decimal?)null;
            water.Salt = decimal.TryParse(txtSalt.Text, out var salt) ? salt : (decimal?)null;
            water.PhLevel = decimal.TryParse(txtPHlevel.Text, out var ph) ? ph : (decimal?)null;
            water.O2Level = decimal.TryParse(txtO2level.Text, out var o2) ? o2 : (decimal?)null;
            water.No2Level = decimal.TryParse(txtNO2level.Text, out var no2) ? no2 : (decimal?)null;
            water.No3Level = decimal.TryParse(txtNO3level.Text, out var no3) ? no3 : (decimal?)null;
            water.Po4Level = decimal.TryParse(txtPO4level.Text, out var po4) ? po4 : (decimal?)null;
            water.TotalChlorines = decimal.TryParse(txtTotalChlorines.Text, out var chlorine) ? chlorine : (decimal?)null;
            water.Date = DateTime.Now;
        }

        private WaterParametersTbl saveWaterToDb()
        {
            
            return new WaterParametersTbl
            {
                Temperature = decimal.TryParse(Temperature.Text, out var temp) ? temp : (decimal?)null,
                Salt = decimal.TryParse(Salt.Text, out var salt) ? salt : (decimal?)null,
                PhLevel = decimal.TryParse(PHlevel.Text, out var ph) ? ph : (decimal?)null,
                O2Level = decimal.TryParse(O2level.Text, out var o2) ? o2 : (decimal?)null,
                No2Level = decimal.TryParse(NO2level.Text, out var no2) ? no2 : (decimal?)null,
                No3Level = decimal.TryParse(NO3level.Text, out var no3) ? no3 : (decimal?)null,
                Po4Level = decimal.TryParse(PO4level.Text, out var po4) ? po4 : (decimal?)null,
                TotalChlorines = decimal.TryParse(TotalChlorines.Text, out var chlorine) ? chlorine : (decimal?)null,
                Date = DateTime.Now,
                PondId = int.TryParse(cbPond.SelectedValue?.ToString(), out var pondId) ? pondId : 0,
                
            };
        }

        private void CleanAddForm()
        {
            Temperature.Text = string.Empty;
            Salt.Text = string.Empty;
            PHlevel.Text = string.Empty;
            O2level.Text = string.Empty;
            NO2level.Text = string.Empty;
            NO3level.Text = string.Empty;
            PO4level.Text = string.Empty;
            TotalChlorines.Text = string.Empty;
            
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}