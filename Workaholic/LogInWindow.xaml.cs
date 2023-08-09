using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Workaholic;

namespace StartStopWork
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();
        }

        // Click() events for the buttons
        #region Click()
        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool loggedin = false;

                if (Database.LogIn(UsernameInput.Text, PasswordInput.Text))
                {
                    this.Close();

                    MainWindow.isLoggedOut = false;
                }
                else
                {
                    PublicEntitys.ShowError(100);
                }
            }
            catch
            {
                PublicEntitys.ShowError(306);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
        #endregion
    }
}