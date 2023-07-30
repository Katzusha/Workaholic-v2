using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace StartStopWork
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        // Animations for the non global buttons
        #region Animations
        // Animation for the buttons Enter() event for the align placement of the main window
        private void Align_MouseEnter(object sender, MouseEventArgs e)
        {
            //Animations for buttons background color to transforme it from transparrent to red
            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation myColorAnimation = new ColorAnimation();
            myColorAnimation.From = Color.FromArgb(255, 62, 62, 78);
            myColorAnimation.To = Color.FromArgb(255, 95, 95, 115);
            myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
            ((Button)sender).Background = myBrush;
        }
        // Animation for the buttons Leave() event for the align placement of the main window
        private void Align_MouseLeave(object sender, MouseEventArgs e)
        {
            //Animations for buttons background color to transforme it from transparrent to red
            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation myColorAnimation = new ColorAnimation();
            myColorAnimation.From = Color.FromArgb(255, 95, 95, 115);
            myColorAnimation.To = Color.FromArgb(255, 62, 62, 78);
            myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
            ((Button)sender).Background = myBrush;
        }
        #endregion

        // Variables
        #region Variables
        // Variable for the labels under neath the time lines
        #endregion

        public SettingsWindow()
        {
            InitializeComponent();

            // Open configuration file
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            string btnname = configuration.AppSettings.Settings["Align"].Value;
            SurnameInput.Text = configuration.AppSettings.Settings["Surname"].Value;
            FirstnameInput.Text = configuration.AppSettings.Settings["Firstname"].Value;
            UsernameInput.Text = configuration.AppSettings.Settings["Username"].Value;
            PasswordInput.Text = configuration.AppSettings.Settings["Password"].Value;

            foreach (Button button in Align.Children)
            {
                if (button.Name == (("Align" + btnname)))
                {
                    //Animations for buttons background color to transforme it from transparrent to red
                    SolidColorBrush myBrush = new SolidColorBrush();
                    ColorAnimation myColorAnimation = new ColorAnimation();
                    myColorAnimation.From = Color.FromArgb(0, 86, 86, 255);
                    myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("BorderColor").ToString());
                    myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
                    myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
                    button.BorderBrush = myBrush;
                }
            }

            try
            {
                //cmd = new MySqlCommand("SELECT * FROM MonthlyHours WHERE Username = '" + PublicEntitys.Encryption(configuration.AppSettings.Settings["Username"].Value) + "' ORDER BY Date ASC LIMIT 12", MainWindow.conn);

                //using (MySqlDataReader reader = cmd.ExecuteReader())
                //{
                //    int column = 0;
                //    while (reader.Read())
                //    {
                //        ColumnDefinition col = new ColumnDefinition();
                //        col.Width = new GridLength(80);
                //        MonthlyHistory.ColumnDefinitions.Add(col);

                //        Bar bar = new Bar();
                //        bar.MaxValue = Math.Round(reader.GetDouble(0), 2);
                //        bar.Value = Math.Round(reader.GetDouble(1), 2);
                //        Grid.SetColumn(bar, column);

                //        Label date = new Label();
                //        int day = reader.GetInt32(3);
                //        date.Content = Months[day - 1];
                //        Grid.SetColumn(date, column);
                //        Grid.SetRow(date, 1);

                //        MonthlyHistory.Children.Add(bar);
                //        MonthlyHistory.Children.Add(date);
                //        column++;
                //    }
                //}

                //MainWindow.conn.Close();
            }
            catch (Exception ex)
            {
                //ErrorWindow window = new ErrorWindow("Something happend that was not supposed to. Please check internet connection and if that is not the problem than contact system support!");
                //window.ShowDialog();

                //MainWindow.conn.Close();
            }
        }

        // Click() events for the buttons
        #region Click()
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["Firstname"].Value = FirstnameInput.Text;
            configuration.AppSettings.Settings["Surname"].Value = SurnameInput.Text;
            configuration.AppSettings.Settings["Username"].Value = UsernameInput.Text;
            configuration.AppSettings.Settings["Password"].Value = PasswordInput.Text;
            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void Align_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            foreach (Button child in Align.Children)
            {
                child.BorderBrush = Brushes.Transparent;
            }

            //Animations for buttons background color to transforme it from transparrent to red
            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation myColorAnimation = new ColorAnimation();
            myColorAnimation.From = Color.FromArgb(0, 86, 86, 255);
            myColorAnimation.To = Color.FromArgb(255, 86, 86, 255);
            myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
            btn.BorderBrush = myBrush;

            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["Align"].Value = btn.Name.ToString().Replace("Align", "");
            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["Firstname"].Value = "";
            configuration.AppSettings.Settings["Surname"].Value = "";
            configuration.AppSettings.Settings["Username"].Value = "";
            configuration.AppSettings.Settings["Password"].Value = "";
            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");

            Workaholic.MainWindow.isLoggedOut = true;

            this.Close();
        }
        #endregion

        // Other events
        #region Other events
            private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        #endregion

    }
}
