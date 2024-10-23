using Business_Object.Models;
using KoiCare_Repositories;
using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Drawing2D;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace KoiCareSystemAtHome_App
{
    /// <summary>
    /// Interaction logic for PondWindow.xaml
    /// </summary>
    public partial class PondWindow : Window, INotifyPropertyChanged
    {
        private readonly IPondRepository pondRepo;
        private readonly PondsTbl ponds;
        private  int accId;
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
        private int _pondId;
        public int PondId
        {
            get => _pondId;
            set
            {
                if (_pondId != value)
                {
                    _pondId = value;
                }
            }
        }
        private ObservableCollection<PondsTbl> _pond;
        private ICollectionView _filteredMembers;

        public string FilterText { get; set; }


        public PondWindow(int accId)
        {
            InitializeComponent();
            pondRepo = new PondRepository();
            DataContext = this;
            IsEditMode = false;
            this.accId = accId;
            _pond = new ObservableCollection<PondsTbl>();
            _filteredMembers = CollectionViewSource.GetDefaultView(_pond);
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
            var pondsFromDB = pondRepo.GetPondsByUserId(accId); 
            // Project the KoisTbl objects into an anonymous type containing only the desired properties
            var dataGridKois = pondsFromDB.Select(k => new
            {
                k.PondId,
                k.Name,
                k.Depth,
                k.Volume,
                k.DrainCount,
                k.PumpCapacity
            }).ToList();

            // Populate the _koi collection with the fetched data
            _pond.Clear();  // Clear the existing collection before adding new data
            foreach (var koi in pondsFromDB)
            {
                _pond.Add(koi); // Add each fetched koi to the ObservableCollection
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
                int pondId = (int)clickedButton.CommandParameter;

                var pond = pondRepo.GetPondById(pondId);
                if (pond != null)
                {
                    updateKoiToWindow(pond);
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
                int pondId = (int)clickedButton.CommandParameter;

                if (pondRepo.DeletePond(pondId))
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

        private void KoiManagement_Click(object sender, RoutedEventArgs e)
        {
            KoiWindow koiWindow = new KoiWindow(accId);
            koiWindow.Show();
            this.Close();
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
            var newPond = saveKoiToDb();
            if (pondRepo.CreatePond(newPond))
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
                if (pondRepo.UpdatePond(updateKoi))
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
            updateKoiToWindow(pondRepo.GetPondById(_pondId));
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
        private void updateKoiToWindow(PondsTbl pond)
        {
            _pondId = pond.PondId;
            pondName.Text = pond.Name;
            pondDepth.Text = pond.Depth.ToString();
            pondDrainCount.Text = pond.DrainCount.ToString();
            pondVolumn.Text = pond.Volume.ToString();
            pondPumpCapacity.Text = pond.PumpCapacity.ToString();
            
        }
        private PondsTbl updateKoiToDb()
        {
            return new PondsTbl
            {
                PondId = _pondId,
                Name = pondName.Text,
                Depth = decimal.Parse(pondDepth.Text),
                Volume = int.Parse(pondVolumn.Text),
                DrainCount = int.Parse(pondDrainCount.Text),
                PumpCapacity = int.Parse(pondPumpCapacity.Text),
                AccId = accId
            };
        }

        private PondsTbl saveKoiToDb()
        {
            return new PondsTbl
            {
                Name = name.Text,
                Depth = decimal.Parse(depth.Text),
                DrainCount = int.Parse(drainCount.Text),
                Volume = int.Parse(volumn.Text),
                PumpCapacity = int.Parse(pumpCapacity.Text),
                AccId = accId
            };
        }

        private void CleanAddForm()
        {
            name.Text = string.Empty;        // Clear TextBox
            volumn.Text = string.Empty;         // Clear TextBox
            depth.Text = string.Empty;       // Clear TextBox
            drainCount.Text = string.Empty;      // Clear TextBox
            pumpCapacity.Text = string.Empty;      // Clear TextBox
        }
    }

}

