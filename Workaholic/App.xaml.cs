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

        private void BreakButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button button = (Button)sender;

            //Animations for buttons background color to transforme it from transparrent to red
            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation myColorAnimation = new ColorAnimation();
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("PlateColor").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("BreakColor").ToString());
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
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("BreakColor").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("PlateColor").ToString());
            myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
            button.Background = myBrush;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                if ((sender as MenuItem).Header.ToString() == "Edit")
                {
                    ReadWriteBar _ReadWriteBar = new ReadWriteBar();
                    try
                    {
                        _ReadWriteBar = (ReadWriteBar)((Grid)((ContextMenu)(sender as MenuItem).Parent).PlacementTarget).Parent;
                        DetailWindow detailWindow = new DetailWindow();
                        detailWindow.DetailClose.Style = (Style)this.Resources["GreenButton"];
                        detailWindow.DetailClose.Content = "Save";

                        int row = 0;
                        foreach (DailyHours _dailyHours in Database.GetDailyHoursDetail(configuration.AppSettings.Settings["Username"].Value, _ReadWriteBar.Id))
                        {
                            RowDefinition rowDefinition = new RowDefinition();
                            if (_dailyHours.StampType == 1)
                            {
                                rowDefinition.Height = new GridLength(25);
                                Label label = new Label();
                                label.Content = "WORK";
                                label.Name = $"Id{_dailyHours.Id.ToString()}";
                                detailWindow.DetailGrid.RowDefinitions.Add(rowDefinition);
                                Grid.SetRow(label, row);
                                Grid.SetColumnSpan(label, 2);
                                detailWindow.DetailGrid.Children.Add(label);
                                row++;

                                TextBox textbox = new TextBox();
                                textbox.Style = (Style)this.Resources["TextBoxTime"];
                                textbox.Text = TimeSpan.FromHours(_dailyHours.Start).ToString(@"hh\:mm");
                                textbox.Name = $"Start";
                                rowDefinition = new RowDefinition();
                                rowDefinition.Height = new GridLength(40);
                                detailWindow.DetailGrid.RowDefinitions.Add(rowDefinition);
                                Grid.SetRow(textbox, row);
                                detailWindow.DetailGrid.Children.Add(textbox);

                                textbox = new TextBox();
                                textbox.Style = (Style)this.Resources["TextBoxTime"];
                                textbox.Text = TimeSpan.FromHours(_dailyHours.End).ToString(@"hh\:mm");
                                textbox.Name = $"End";
                                Grid.SetRow(textbox, row);
                                Grid.SetColumn(textbox, 1);
                                detailWindow.DetailGrid.Children.Add(textbox);
                                row++;
                            }
                            rowDefinition = new RowDefinition();
                            if (_dailyHours.StampType == 2)
                            {
                                rowDefinition.Height = new GridLength(25);
                                Label label = new Label();
                                label.Content = "BREAK";
                                label.Name = $"Id{_dailyHours.Id.ToString()}";
                                detailWindow.DetailGrid.RowDefinitions.Add(rowDefinition);
                                Grid.SetRow(label, row);
                                Grid.SetColumnSpan(label, 2);
                                detailWindow.DetailGrid.Children.Add(label);
                                row++;

                                TextBox textbox = new TextBox();
                                textbox.Style = (Style)this.Resources["TextBoxTime"];
                                textbox.Text = TimeSpan.FromHours(_dailyHours.Start).ToString(@"hh\:mm");
                                textbox.Name = $"Start";
                                rowDefinition = new RowDefinition();
                                rowDefinition.Height = new GridLength(30);
                                detailWindow.DetailGrid.RowDefinitions.Add(rowDefinition);
                                Grid.SetRow(textbox, row);
                                detailWindow.DetailGrid.Children.Add(textbox);

                                textbox = new TextBox();
                                textbox.Style = (Style)this.Resources["TextBoxTime"];
                                textbox.Text = TimeSpan.FromHours(_dailyHours.End).ToString(@"hh\:mm");
                                textbox.Name = $"End";
                                Grid.SetRow(textbox, row);
                                Grid.SetColumn(textbox, 1);
                                detailWindow.DetailGrid.Children.Add(textbox);
                                row++;
                            }
                        }

                        detailWindow.ShowDialog();

                        SettingsWindow window = (SettingsWindow)Window.GetWindow(sender as DependencyObject);
                        window.RefreshDailyGrid();


                        //MessageBox.Show(((SettingsWindow)((Border)((Grid)((Grid)((Border)((ScrollViewer)((Grid)((ReadWriteBar)((Grid).Parent).Parent).Parent).Parent).Parent).Parent).Parent).Parent).ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if ((sender as MenuItem).Header.ToString() == "Details")
                {
                    ReadWriteBar _ReadWriteBar = new ReadWriteBar();
                    ReadOnlyBar _ReadOnlyBar = new ReadOnlyBar();
                    try
                    {
                        _ReadWriteBar = (ReadWriteBar)((Grid)((ContextMenu)(sender as MenuItem).Parent).PlacementTarget).Parent;
                        DetailWindow detailWindow = new DetailWindow();
                        detailWindow.DetailClose.Style = (Style)this.Resources["HyperLinkButton"];
                        detailWindow.DetailClose.Content = "Close";

                        int row = 0;
                        foreach (DailyHours _dailyHours in Database.GetDailyHoursDetail(configuration.AppSettings.Settings["Username"].Value, _ReadWriteBar.Id))
                        {
                            RowDefinition rowDefinition = new RowDefinition();
                            if (_dailyHours.StampType == 1)
                            {
                                rowDefinition.Height = new GridLength(25);
                                Label label = new Label();
                                label.Content = "WORK";
                                detailWindow.DetailGrid.RowDefinitions.Add(rowDefinition);
                                Grid.SetRow(label, row);
                                Grid.SetColumnSpan(label, 2);
                                detailWindow.DetailGrid.Children.Add(label);
                                row++;

                                TextBox textbox = new TextBox();
                                textbox.Style = (Style)this.Resources["TextBoxTime"];
                                textbox.Text = TimeSpan.FromHours(_dailyHours.Start).ToString(@"hh\:mm");
                                textbox.Name = $"Id{_dailyHours.Id.ToString()}";
                                textbox.IsReadOnly = true;
                                rowDefinition = new RowDefinition();
                                rowDefinition.Height = new GridLength(40);
                                detailWindow.DetailGrid.RowDefinitions.Add(rowDefinition);
                                Grid.SetRow(textbox, row);
                                detailWindow.DetailGrid.Children.Add(textbox);

                                textbox = new TextBox();
                                textbox.Style = (Style)this.Resources["TextBoxTime"];
                                textbox.Text = TimeSpan.FromHours(_dailyHours.End).ToString(@"hh\:mm");
                                textbox.Name = $"Id{_dailyHours.Id.ToString()}";
                                textbox.IsReadOnly = true;
                                Grid.SetRow(textbox, row);
                                Grid.SetColumn(textbox, 1);
                                detailWindow.DetailGrid.Children.Add(textbox);
                                row++;
                            }
                            rowDefinition = new RowDefinition();
                            if (_dailyHours.StampType == 2)
                            {
                                rowDefinition.Height = new GridLength(25);
                                Label label = new Label();
                                label.Content = "BREAK";
                                detailWindow.DetailGrid.RowDefinitions.Add(rowDefinition);
                                Grid.SetRow(label, row);
                                Grid.SetColumnSpan(label, 2);
                                detailWindow.DetailGrid.Children.Add(label);
                                row++;

                                TextBox textbox = new TextBox();
                                textbox.Style = (Style)this.Resources["TextBoxTime"];
                                textbox.Text = TimeSpan.FromHours(_dailyHours.Start).ToString(@"hh\:mm");
                                textbox.Name = $"Id{_dailyHours.Id.ToString()}";
                                textbox.IsReadOnly = true;
                                rowDefinition = new RowDefinition();
                                rowDefinition.Height = new GridLength(30);
                                detailWindow.DetailGrid.RowDefinitions.Add(rowDefinition);
                                Grid.SetRow(textbox, row);
                                detailWindow.DetailGrid.Children.Add(textbox);

                                textbox = new TextBox();
                                textbox.Style = (Style)this.Resources["TextBoxTime"];
                                textbox.Text = TimeSpan.FromHours(_dailyHours.End).ToString(@"hh\:mm");
                                textbox.Name = $"Id{_dailyHours.Id.ToString()}";
                                textbox.IsReadOnly = true;
                                Grid.SetRow(textbox, row);
                                Grid.SetColumn(textbox, 1);
                                detailWindow.DetailGrid.Children.Add(textbox);
                                row++;
                            }
                        }

                        detailWindow.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
