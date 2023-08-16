using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Numerics;
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
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {

        System.Media.SoundPlayer player;
        

        public ErrorWindow(int errortype)
        {
            InitializeComponent();

            switch (errortype)
            {
                case 100:
                    ErrorHeader.Content = "WARNING";
                    ErrorHeader.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("BreakBrush").ToString()));
                    Border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("BreakBrush").ToString()));
                    ErrorMessageOutput.Text = "Wrong username or password. Please try again.";
                    break;
                case 306:
                    ErrorHeader.Content = "! ERROR !";
                    ErrorHeader.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("RedBrush").ToString()));
                    Border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("RedBrush").ToString()));
                    ErrorMessageOutput.Text = "Internal error. Please contact system support!";
                    break;
                case 400:
                    ErrorHeader.Content = "! ERROR !";
                    ErrorHeader.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("RedBrush").ToString()));
                    Border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("RedBrush").ToString()));
                    ErrorMessageOutput.Text = "Please check your internet connection and try again.";
                    break;
                case 401:
                    ErrorHeader.Content = "! ERROR !";
                    ErrorHeader.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("RedBrush").ToString()));
                    Border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("RedBrush").ToString()));
                    ErrorMessageOutput.Text = "You do not have permissions for access this file.";
                    break;
                case 404:
                    ErrorHeader.Content = "! ERROR !";
                    ErrorHeader.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("RedBrush").ToString()));
                    Border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("RedBrush").ToString()));
                    ErrorMessageOutput.Text = "Request was not found.";
                    break;
                case 405:
                    ErrorHeader.Content = "! ERROR !";
                    ErrorHeader.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("RedBrush").ToString()));
                    Border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("RedBrush").ToString()));
                    ErrorMessageOutput.Text = "You do not have permissions for this request.";
                    break;
                case 406:
                    ErrorHeader.Content = "WARNING";
                    ErrorHeader.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("RedBrush").ToString()));
                    Border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("RedBrush").ToString()));
                    ErrorMessageOutput.Text = "Invalid inputs. Please check them and try again.";
                    break;
                case 407:
                    ErrorHeader.Content = "! ERROR !";
                    ErrorHeader.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("RedBrush").ToString()));
                    Border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("RedBrush").ToString()));
                    ErrorMessageOutput.Text = "Wrong connection to the server. Please contact system support.";
                    break;
                case 408:
                    ErrorHeader.Content = "! ERROR !";
                    ErrorHeader.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("RedBrush").ToString()));
                    Border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("RedBrush").ToString()));
                    ErrorMessageOutput.Text = "Request has timmed out.";
                    break;
                case 409:
                    ErrorHeader.Content = "WARNING";
                    ErrorHeader.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("BreakBrush").ToString()));
                    Border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("BreakBrush").ToString()));
                    ErrorMessageOutput.Text = "Query ended in conflict. Please check your inputs.";
                    break;
                case 500:
                    ErrorHeader.Content = "! ERROR !";
                    ErrorHeader.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("RedBrush").ToString()));
                    Border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("RedBrush").ToString()));
                    ErrorMessageOutput.Text = "Database is currupted. Please contact system support!";
                    break;
            }

            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (configuration.AppSettings.Settings["HaHaFunny"].Value == "true")
            {
                string[] randomerror = {"I have no clue what just went wrong. Just do the basic procedures and check things up like" +
            " like internet and stuf \nHere is some useles error code: C30034", "Something went wrong with the output. Please check all the inserted " +
            "data and try again.\nError code: C40012", "Opsie wipsie something went wrong!\nPlease check some useles internet stuf and go cry in ur room.\n" +
            "Here is the error code becouse I Will Not explain it to you: C42069", "Error.... \n\n Just error"};

                Random rand = new Random();
                int index = rand.Next(randomerror.Length);
                ErrorMessageOutput.Text = randomerror[index];

                //player = new System.Media.SoundPlayer(@"D:\Projects\Kerieri\Kerieri\Sounds\errorsound.wav");
                //player.Play();
            }
        }



        private void BlueBtn_Enter(object sender, MouseEventArgs e)
        {
            //Animations for buttons background color to transforme it from transparrent to red
            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation myColorAnimation = new ColorAnimation();
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundColor").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("BorderColor").ToString());
            myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
            okbutton.Background = myBrush;
        }

        private void BlueBtn_Leave(object sender, MouseEventArgs e)
        {
            //Animations for buttons background color to transforme it from transparrent to red
            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation myColorAnimation = new ColorAnimation();
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("BorderColor").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundColor").ToString());
            myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
            okbutton.Background = myBrush;
        }

        private void okbutton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
