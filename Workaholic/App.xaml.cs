using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using StartStopWork;
using Workaholic.Models;
using System.Windows.Controls.Primitives;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace Workaholic
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void BlueButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button button = (Button)sender;

            //Animations for buttons background color to transforme it from transparrent to red
            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation myColorAnimation = new ColorAnimation();
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundBrush").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("PrimaryBrush").ToString());
            myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
            button.Background = myBrush;
        }
        private void BlueButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button button = (Button)sender;

            //Animations for buttons background color to transforme it from transparrent to red
            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation myColorAnimation = new ColorAnimation();
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("PrimaryBrush").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundBrush").ToString());
            myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
            button.Background = myBrush;
        }

        private void RedButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button button = (Button)sender;

            //Animations for buttons background color to transforme it from transparrent to red
            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation myColorAnimation = new ColorAnimation();
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundBrush").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("RedBrush").ToString());
            myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
            button.Background = myBrush;
        }
        private void RedButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button button = (Button)sender;

            //Animations for buttons background color to transforme it from transparrent to red
            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation myColorAnimation = new ColorAnimation();
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("RedBrush").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundBrush").ToString());
            myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
            button.Background = myBrush;
        }

        private void GreenButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button button = (Button)sender;

            //Animations for buttons background color to transforme it from transparrent to red
            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation myColorAnimation = new ColorAnimation();
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundBrush").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("GreenBrush").ToString());
            myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
            button.Background = myBrush;
        }
        private void GreenButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button button = (Button)sender;

            //Animations for buttons background color to transforme it from transparrent to red
            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation myColorAnimation = new ColorAnimation();
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("GreenBrush").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundBrush").ToString());
            myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
            button.Background = myBrush;
        }

        private void HyperLinkButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button button = (Button)sender;

            //Animations for buttons background color to transforme it from transparrent to red
            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation myColorAnimation = new ColorAnimation();
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("PlateBrush").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundBrush").ToString());
            myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
            button.Background = myBrush;
        }
        private void HyperLinkButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button button = (Button)sender;

            //Animations for buttons background color to transforme it from transparrent to red
            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation myColorAnimation = new ColorAnimation();
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundBrush").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("PlateBrush").ToString());
            myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
            button.Background = myBrush;
        }

        private void BreakButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button button = (Button)sender;

            //Animations for buttons background color to transforme it from transparrent to red
            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation myColorAnimation = new ColorAnimation();
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("PlateBrush").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("BreakBrush").ToString());
            myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
            button.Background = myBrush;
        }
        private void BreakButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button button = (Button)sender;

            //Animations for buttons background color to transforme it from transparrent to red
            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation myColorAnimation = new ColorAnimation();
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("BreakBrush").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("PlateBrush").ToString());
            myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
            button.Background = myBrush;
        }

        private void Align_MouseEnter(object sender, MouseEventArgs e)
        {
            //Animations for buttons background color to transforme it from transparrent to red
            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation myColorAnimation = new ColorAnimation();
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundBrush").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("PrimaryBrush").ToString());
            myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
            ((Button)sender).Background = myBrush;
        }
        private void Align_MouseLeave(object sender, MouseEventArgs e)
        {
            //Animations for buttons background color to transforme it from transparrent to red
            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation myColorAnimation = new ColorAnimation();
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("PrimaryBrush").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundBrush").ToString());
            myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
            ((Button)sender).Background = myBrush;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            

            if ((sender as MenuItem).Header.ToString().Contains("Edit"))
            {
                ReadWriteBar _ReadWriteBar = new ReadWriteBar();
                try
                {
                    
                    DetailWindow detailWindow = new DetailWindow(1, sender);
                    detailWindow.ShowDialog();

                    SettingsWindow window = (SettingsWindow)Window.GetWindow(sender as DependencyObject);
                    window.RefreshDailyGrid();
                }
                catch (Exception ex)
                {
                    PublicEntitys.ShowError(306);
                }
            }
            else if ((sender as MenuItem).Header.ToString().Contains("Details"))
            {
                try
                {
                    DetailWindow detailWindow = new DetailWindow(2, sender);
                    detailWindow.ShowDialog();
                }
                catch (Exception ex)
                {
                    PublicEntitys.ShowError(306);
                }
            }
        }
    }
}
