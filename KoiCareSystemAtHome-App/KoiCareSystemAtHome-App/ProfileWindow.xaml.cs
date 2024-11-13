using KoiCare_Repositories;
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

namespace KoiCareSystemAtHome_App
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        private int accountID;
        private IAccountRepo accountRepo;
        public ProfileWindow(int accountID)
        {
            InitializeComponent();
            this.accountID = accountID;
            accountRepo = new AccountRepo();
        }

        private void loadProfileData()
        {
            var account = accountRepo.GetAccount(accountID);

        }
    }
}
