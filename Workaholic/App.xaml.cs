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
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundColor").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("BorderColor").ToString());
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
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("BorderColor").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundColor").ToString());
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
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundColor").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("RedColor").ToString());
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
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("RedColor").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundColor").ToString());
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
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundColor").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("GreenColor").ToString());
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
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("GreenColor").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundColor").ToString());
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
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("PlateColor").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundColor").ToString());
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
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundColor").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("PlateColor").ToString());
            myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
            button.Background = myBrush;
        }
    }
}
