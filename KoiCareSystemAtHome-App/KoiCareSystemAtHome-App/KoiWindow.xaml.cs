using Business_Object.Models;
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
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace KoiCareSystemAtHome_App
{
    /// <summary>
    /// Interaction logic for KoiWindow.xaml
    /// </summary>
    public partial class KoiWindow : Window, INotifyPropertyChanged
    {
        private readonly IKoiRepo koiRepo;
        private readonly KoisTbl kois;
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
        private int _koiId;
        public int KoiId
        {
            get => _koiId;
            set
            {
                if (_koiId != value)
                {
                    _koiId = value;
                }
            }
        }
        private ObservableCollection<KoisTbl> _koi;
        private ICollectionView _filteredMembers;

        public string FilterText { get; set; }


        public KoiWindow()
        {
            InitializeComponent();
            koiRepo = new KoiRepo();
            DataContext = this;
            IsEditMode = false;
            _koi = new ObservableCollection<KoisTbl>();
            _filteredMembers = CollectionViewSource.GetDefaultView(_koi);
        }

        private void FilterMembers()
        {
            if (!string.IsNullOrEmpty(FilterText))
            {
                // Apply filter to show only Koi items whose name contains FilterText (case-insensitive)
                _filteredMembers.Filter = item =>
                {
                    var koi = item as KoisTbl;
                    return koi != null && koi.Name.Contains(FilterText, StringComparison.OrdinalIgnoreCase);
                };
            }
            else
            {
                // Clear filter and show all items
                _filteredMembers.Filter = null;
            }
            KoiDataGrid.ItemsSource = _filteredMembers;
        }

        private void FilterTextChanged(object sender, TextChangedEventArgs e)
        {
            // Call the filter method whenever the TextBox text changes
            FilterMembers();
        }

        private async void Window_Loaded()
        {
            // Fetch koi data asynchronously
            var koisFromDb = koiRepo.GetKois(); // Assuming GetKoisAsync() is asynchronous

            // Project the KoisTbl objects into an anonymous type containing only the desired properties
            var dataGridKois = koisFromDb.Select(k => new
            {
                k.KoiId,
                k.Name,
                k.Age,
                Sex = k.Sex ? "Male" : "Female",  // Example transformation
                k.Breed
            }).ToList();

            // Populate the _koi collection with the fetched data
            _koi.Clear();  // Clear the existing collection before adding new data
            foreach (var koi in koisFromDb)
            {
                _koi.Add(koi); // Add each fetched koi to the ObservableCollection
            }

            // Set the filtered members to the DataGrid
            KoiDataGrid.ItemsSource = dataGridKois;
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

        private void Button_Click(object sender, RoutedEventArgs e)
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
                int koiId = (int)clickedButton.CommandParameter;

                var koi = koiRepo.GetKoiById(koiId);
                if (koi != null)
                {
                    updateKoiToWindow(koi);
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
                int koiId = (int)clickedButton.CommandParameter;

                if (koiRepo.DeleteKoi(koiId))
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

        private void SelectImg_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*",
                Title = "Select a Koi Image"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Set the selected image to the Image control
                KoiImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
            else
            {

                KoiImage.Source = null;
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddKoi_Click(object sender, RoutedEventArgs e)
        {
            var newKoi = saveKoiToDb();
            if (koiRepo.CreateKoi(newKoi))
            {
                CleanAddForm();
                Window_Loaded();
                ChangUI(1);
            }
        }
        private void CancelAddKoi_Click(Object sender, RoutedEventArgs e)
        {
            CleanAddForm();
            KoiImage.Source = null;
        }

        private void NumberOnly(object sender, TextCompositionEventArgs e)
        {
            // Check if the input is a digit
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void EditKoi_Click(object sender, RoutedEventArgs e)
        {
            if (IsEditMode)
            {
                IsEditMode = false;
                btnEdit.Text = "Edit";
                iconEdit.Kind = PackIconMaterialKind.ContentSave;
                //Logic update koi
                var updateKoi = updateKoiToDb();
                if (koiRepo.UpdateKoi(updateKoi))
                {
                    updateKoiToWindow(updateKoi);
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
            updateKoiToWindow(koiRepo.GetKoiById(_koiId));
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
                    KoiDataGrid.Visibility = Visibility.Visible;
                    break;
                case 2:
                    ChangeUIWhenClickButton(btn_detail, btn_add, btn_list);
                    KoiDataGrid.Visibility = Visibility.Collapsed;
                    NewKoiEntryGrid.Visibility = Visibility.Collapsed;
                    KoiDetailGrid.Visibility = Visibility.Visible;
                    break;
                case 3:
                    ChangeUIWhenClickButton(btn_add, btn_detail, btn_list);
                    KoiDetailGrid.Visibility = Visibility.Collapsed;
                    KoiDataGrid.Visibility = Visibility.Collapsed;
                    NewKoiEntryGrid.Visibility = Visibility.Visible;
                    break;

            }
        }

        // End Change UI section
        // Addition Function
        private void updateKoiToWindow(KoisTbl koi)
        {
            _koiId = koi.KoiId;
            koiName.Text = koi.Name;
            koiAge.Text = koi.Age.ToString();
            koiBreed.Text = koi.Breed;
            //if (koi.Image != null) koiImg.Source = new BitmapImage(new Uri(koi.Image));
            koiLenght.Text = koi.Length.ToString();
            koiWeight.Text = koi.Weight.ToString();
            koiSex.SelectedItem = koi.Sex ? koiSex.Items[0] : koiSex.Items[1];
            koiPondId.Text = koi.PondId.ToString();
            koiPhysique.Text = koi.Physique;
        }
        private KoisTbl updateKoiToDb()
        {
            return new KoisTbl
            {
                KoiId = _koiId,
                Name = koiName.Text,
                Age = int.Parse(koiAge.Text),
                Breed = koiBreed.Text,
                Length = decimal.Parse(koiLenght.Text),
                Weight = decimal.Parse(koiWeight.Text),
                Sex = (koiSex.SelectedItem as ComboBoxItem).Content.ToString().Equals("Male") ? true : false,
                PondId = int.Parse(koiPondId.Text),
                Physique = koiPhysique.Text
            };
        }
        
        private KoisTbl saveKoiToDb()
        {
            return new KoisTbl
            {
                Name = name.Text,
                Age = int.Parse(age.Text),
                Breed = breed.Text,
                Length = decimal.Parse(lenght.Text),
                Weight = decimal.Parse(weight.Text),
                Sex = (sex.SelectedItem as ComboBoxItem).Content.ToString().Equals("Male") ? true : false,
                PondId = int.Parse(pondId.Text),
                Physique = physique.Text
            };
        }

        private void CleanAddForm()
        {
                name.Text = string.Empty;        // Clear TextBox
                age.Text = string.Empty;         // Clear TextBox
                breed.Text = string.Empty;       // Clear TextBox
                lenght.Text = string.Empty;      // Clear TextBox
                weight.Text = string.Empty;      // Clear TextBox
                sex.SelectedItem = null;         // Clear ComboBox selection
                pondId.Text = string.Empty;      // Clear TextBox
                physique.Text = string.Empty;    // Clear TextBox
                                                      // koiImg.Source = null;            // Uncomment if you want to clear the 
        }
    }
       
}
