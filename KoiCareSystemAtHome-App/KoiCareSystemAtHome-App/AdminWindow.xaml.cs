using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using KoiCare_DAOs;
using Business_Object;
using KoiCare_Repositories;

namespace KoiCareSystemAtHome_App
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private IAccountRepo accountRepo;
        public AdminWindow()
        {
            InitializeComponent();
            accountRepo = new AccountRepo();
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            List<AccountTbl> accounts = AccountDAO.Instance.GetAccounts();
            UserList.Items.Clear();
            foreach (var account in accounts)
            {
                UserList.Items.Add(new ListBoxItem { Content = account.Name, Tag = account });
            }
        }

        private void UserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserList.SelectedItem is ListBoxItem selectedItem && selectedItem.Tag is AccountTbl account)
            {
                txtName.Text = account.Name;
                txtEmail.Text = account.Email;
                txtAddress.Text = account.Address;
                txtPhone.Text = account.Phone;
                txtStatus.IsChecked = account.Status;
                txtRoleID.SelectedItem = txtRoleID.Items
                    .Cast<ComboBoxItem>()
                    .FirstOrDefault(item => item.Content.ToString() == account.Role);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserList.SelectedItem is ListBoxItem selectedItem && selectedItem.Tag is AccountTbl account)
            {
                account.Name = txtName.Text;
                account.Email = txtEmail.Text;
                account.Address = txtAddress.Text;
                account.Phone = txtPhone.Text;
                account.Role = (txtRoleID.SelectedItem as ComboBoxItem)?.Content.ToString();

                bool result = accountRepo.UpdateAccount(account);
                if (result)
                {
                    MessageBox.Show("Account updated successfully!");
                    LoadAccounts();
                }
                else
                {
                    MessageBox.Show("Failed to update the account.");
                }
            }
        }
    }
}
