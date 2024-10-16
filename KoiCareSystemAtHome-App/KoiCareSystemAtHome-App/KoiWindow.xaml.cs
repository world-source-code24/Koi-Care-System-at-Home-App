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
using static System.Net.Mime.MediaTypeNames;

namespace KoiCareSystemAtHome_App
{
    /// <summary>
    /// Interaction logic for KoiWindow.xaml
    /// </summary>
    public partial class KoiWindow : Window
    {
        public KoiWindow()
        {
            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
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
        private void ChangeUIWhenClickButton(Button clickButton, Button button, Button button1)
        {
            clickButton.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#db4b2e"));
            button.BorderBrush = Brushes.Transparent;
            button1.BorderBrush = Brushes.Transparent;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton == btn_add || clickedButton == btn_add1)
            {
                ChangeUIWhenClickButton(btn_add, btn_detail, btn_list);
                KoiDetailGrid.Visibility = Visibility.Collapsed;
                KoiDataGrid.Visibility = Visibility.Collapsed;
                NewKoiEntryGrid.Visibility = Visibility.Visible;
            }
            else if (clickedButton == btn_detail)
            {
                ChangeUIWhenClickButton(btn_detail, btn_add, btn_list);
                KoiDataGrid.Visibility = Visibility.Collapsed;
                NewKoiEntryGrid.Visibility = Visibility.Collapsed;
                KoiDetailGrid.Visibility = Visibility.Visible;
            }
            else if (clickedButton == btn_list)
            {
                ChangeUIWhenClickButton(btn_list, btn_detail, btn_add);
                KoiDetailGrid.Visibility = Visibility.Collapsed;
                NewKoiEntryGrid.Visibility = Visibility.Collapsed;
                KoiDataGrid.Visibility = Visibility.Visible;
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

        }
        private void CancelAddKoi_Click(Object sender, RoutedEventArgs e)
        {

            KoiImage.Source = null;
        }

        private void NumberOnly(object sender, TextCompositionEventArgs e)
        {
            // Check if the input is a digit
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void EditKoi_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
