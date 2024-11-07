using Business_Object.Models;
using KoiCare_DAOs;
using KoiCare_Services;
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
using System.Windows;
using System.Collections.ObjectModel;

namespace KoiCareSystemAtHome_App
{
    /// <summary>
    /// Interaction logic for PondListWindow.xaml
    /// </summary>
    public partial class WaterParaWindow : Window, INotifyPropertyChanged
    {
        private readonly IWaterParaService _waterParaService;
        private bool _visibleContent = true;

        private ObservableCollection<WaterParametersTbl> _waterParameters;
        public WaterParaWindow()
        {
            InitializeComponent();
            _waterParaService = new WaterParaService();
            WaterParameters = new ObservableCollection<WaterParametersTbl>(_waterParaService.GetWaterParameter());
            DataContext = this;
        }

        public ObservableCollection<WaterParametersTbl> WaterParameters
        {
            get => _waterParameters;
            set
            {
                _waterParameters = value;
                OnPropertyChanged(nameof(WaterParameters));
            }
        }

        public bool VisibleContent
        {
            get => _contentLoaded;
            set
            {
                _visibleContent = value;
                OnPropertyChanged(nameof(VisibleContent));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Xu ly su kien
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(btn_list))
            {
                _visibleContent = true;
            }
            else if (sender.Equals(btn_detail))
            {
                _visibleContent = true;
            }
        }

        private void FilterTextChanged(object sender, RoutedEventArgs e)
        {
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Kiểm tra xem đó là chuột trái, chuột phải hoặc chuột giữa.
            if (e.ChangedButton == MouseButton.Left)
            {
                // Xử lý khi chuột trái được nhấn
                MessageBox.Show("Chuột trái vừa được nhấn!");
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                // Xử lý khi chuột phải được nhấn
                MessageBox.Show("Chuột phải vừa được nhấn!");
            }
            // Thực hiện các hành động khác khi có sự kiện MouseDown
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Xử lý sự kiện khi chuột trái được nhấn
            MessageBox.Show("Chuột trái vừa được nhấn xuống!");

            // Ví dụ: Di chuyển cửa sổ
            if (e.ClickCount == 2)
            {
                // Xử lý khi nhấn đúp chuột trái
                MessageBox.Show("Đã nhấn đúp chuột trái!");
            }
        }
    }
}
