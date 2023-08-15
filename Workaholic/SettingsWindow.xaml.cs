using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Printing;
using System.Reflection;
using System.Reflection.PortableExecutable;
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
using Workaholic.Models;
using Duration = System.Windows.Duration;

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
        private static string[] Months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Okt", "Nov", "Dec" };
        #endregion

        public SettingsWindow()
        {
            InitializeComponent();

            try
            {


                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                // Open configuration file
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

                RefreshDailyGrid();
            }
            catch
            {
                PublicEntitys.ShowError(306);
            }
        }

        public void RefreshDailyGrid()
        {
            try
            {
                int column = 0;
                DateOnly coldate = DateOnly.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                DateOnly lastdate = DateOnly.Parse(DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd"));
                Label time = new Label();
                time.IsHitTestVisible = false;
                time.VerticalAlignment = VerticalAlignment.Center;
                time.FontSize = 20;
                double worktime = 0;
                double breaktime = 0;
                int Id = 0;

                DailyHistory.ColumnDefinitions.Clear();
                DailyHistory.Children.Clear();

                ColumnDefinition col = new ColumnDefinition();
                col.Width = new GridLength(80);
                DailyHistory.ColumnDefinitions.Add(col);
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                foreach (DailyHours _dailyHours in Database.GetDailyHours(configuration.AppSettings.Settings["Username"].Value))
                {
                    if (DateOnly.Parse(_dailyHours.Date.ToShortDateString()) == coldate.AddDays(1))
                    {
                        if ((worktime * 0.0625) < breaktime)
                        {
                            time.Content = TimeSpan.FromHours(worktime - (breaktime - (worktime * 0.0625))).ToString(@"hh\:mm");
                        }
                        else
                        {
                            time.Content = TimeSpan.FromHours(worktime).ToString(@"hh\:mm");
                        }

                        Grid.SetColumn(time, column);
                        DailyHistory.Children.Add(time);

                        col = new ColumnDefinition();
                        col.Width = new GridLength(80);
                        DailyHistory.ColumnDefinitions.Add(col);

                        column++;

                        time = new Label();
                        time.IsHitTestVisible = false;
                        time.FontSize = 20;
                        time.VerticalAlignment = VerticalAlignment.Center;
                        time.Content = "";
                        worktime = 0;
                        breaktime = 0;
                    }
                    while (lastdate < DateOnly.Parse(_dailyHours.Date.ToShortDateString()))
                    {
                        if (lastdate != coldate)
                        {
                            ReadWriteBar bar = new ReadWriteBar();
                            Label date = new Label();
                            date.Content = lastdate.ToString("dd. ") + Months[Int32.Parse(lastdate.ToString("MM")) - 1];
                            date.IsHitTestVisible = true;

                            bar.MaxValue = 24;
                            bar.WorkMargin = 0;
                            bar.WorkHeight = 0;
                            bar.StampType = 1;

                            bar.Id = 0;

                            Grid.SetColumn(bar, column);
                            Grid.SetColumn(date, column);
                            Grid.SetRow(date, 1);

                            DailyHistory.Children.Add(bar);
                            DailyHistory.Children.Add(date);

                            lastdate = lastdate.AddDays(1);

                            column++;

                            col = new ColumnDefinition();
                            col.Width = new GridLength(80);
                            DailyHistory.ColumnDefinitions.Add(col);
                        }
                        lastdate = lastdate.AddDays(1);
                    }


                    if (_dailyHours.StampType == 1)
                    {
                        if (configuration.AppSettings.Settings["AuthLevel"].Value == "1" || configuration.AppSettings.Settings["AuthLevel"].Value == "2")
                        {
                            ReadWriteBar bar = new ReadWriteBar();
                            Label date = new Label();
                            date.Content = _dailyHours.Date.ToString("dd. ") + Months[Int32.Parse(_dailyHours.Date.ToString("MM")) - 1];
                            date.IsHitTestVisible = true;

                            worktime = worktime + double.Parse(_dailyHours.Duration.ToString());
                            bar.MaxValue = 24;
                            bar.WorkMargin = _dailyHours.Start;
                            bar.WorkHeight = _dailyHours.Duration;
                            bar.StampType = 1;

                            bar.Id = _dailyHours.Id;
                            Id = _dailyHours.Id;

                            bar.ToolTip = $"WORK" +
                                $"\nStart: {TimeSpan.FromHours(_dailyHours.Start).ToString(@"hh\:mm")}" +
                                $"\nEnd: {TimeSpan.FromHours(_dailyHours.End).ToString(@"hh\:mm")}";

                            Grid.SetColumn(bar, column);
                            Grid.SetColumn(date, column);
                            Grid.SetRow(date, 1);

                            DailyHistory.Children.Add(bar);
                            DailyHistory.Children.Add(date);

                            coldate = DateOnly.Parse(_dailyHours.Date.ToShortDateString());
                            lastdate = DateOnly.Parse(_dailyHours.Date.ToShortDateString()).AddDays(2);
                        }
                        else if (configuration.AppSettings.Settings["AuthLevel"].Value == "3")
                        {
                            ReadOnlyBar bar = new ReadOnlyBar();
                            Label date = new Label();
                            date.Content = _dailyHours.Date.ToString("dd. ") + Months[Int32.Parse(_dailyHours.Date.ToString("MM")) - 1];
                            date.IsHitTestVisible = true;

                            worktime = worktime + double.Parse(_dailyHours.Duration.ToString());
                            bar.MaxValue = 24;
                            bar.WorkMargin = _dailyHours.Start;
                            bar.WorkHeight = _dailyHours.Duration;
                            bar.StampType = 1;

                            bar.Id = _dailyHours.Id;
                            Id = _dailyHours.Id;

                            bar.ToolTip = $"WORK" +
                                $"\nStart: {TimeSpan.FromHours(_dailyHours.Start).ToString(@"hh\:mm")}" +
                                $"\nEnd: {TimeSpan.FromHours(_dailyHours.End).ToString(@"hh\:mm")}";

                            Grid.SetColumn(bar, column);
                            Grid.SetColumn(date, column);
                            Grid.SetRow(date, 1);

                            DailyHistory.Children.Add(bar);
                            DailyHistory.Children.Add(date);

                            coldate = DateOnly.Parse(_dailyHours.Date.ToShortDateString());
                        }
                    }
                    else if (_dailyHours.StampType == 2)
                    {
                        ReadWriteBar bar = new ReadWriteBar();
                        Label date = new Label();
                        date.Content = _dailyHours.Date.ToString("dd. ") + Months[Int32.Parse(_dailyHours.Date.ToString("MM")) - 1];
                        date.IsHitTestVisible = true;

                        breaktime = breaktime + double.Parse(_dailyHours.Duration.ToString());
                        bar.MaxValue = 24;
                        bar.WorkMargin = _dailyHours.Start;
                        bar.WorkHeight = _dailyHours.Duration;
                        bar.StampType = 2;

                        bar.Id = Id;

                        bar.ToolTip = $"BREAK" +
                            $"\nStart: {TimeSpan.FromHours(_dailyHours.Start).ToString(@"hh\:mm")}" +
                            $"\nEnd: {TimeSpan.FromHours(_dailyHours.End).ToString(@"hh\:mm")}";

                        Grid.SetColumn(bar, column);
                        Grid.SetColumn(date, column);
                        Grid.SetRow(date, 1);

                        DailyHistory.Children.Add(bar);
                        DailyHistory.Children.Add(date);

                        coldate = DateOnly.Parse(_dailyHours.Date.ToShortDateString());
                        lastdate = coldate;
                    }
                }
                if ((worktime * 0.0625) < breaktime)
                {
                    time.Content = TimeSpan.FromHours(worktime - (breaktime - (worktime * 0.0625))).ToString(@"hh\:mm");
                }
                else
                {
                    time.Content = TimeSpan.FromHours(worktime).ToString(@"hh\:mm");
                }

                Grid.SetColumn(time, column);
                DailyHistory.Children.Add(time);

                time = new Label();
                time.IsHitTestVisible = false;
                time.FontSize = 20;
                time.VerticalAlignment = VerticalAlignment.Center;
                time.Content = "";
                worktime = 0;
                breaktime = 0;

                while (lastdate < DateOnly.Parse(DateTime.Now.AddDays(1).ToShortDateString()))
                {
                    ReadWriteBar bar = new ReadWriteBar();
                    Label date = new Label();
                    date.Content = lastdate.ToString("dd. ") + Months[Int32.Parse(lastdate.ToString("MM")) - 1];
                    date.IsHitTestVisible = true;

                    bar.MaxValue = 24;
                    bar.WorkMargin = 0;
                    bar.WorkHeight = 0;
                    bar.StampType = 1;

                    bar.Id = 0;
                    Id = 0;

                    Grid.SetColumn(bar, column);
                    Grid.SetColumn(date, column);
                    Grid.SetRow(date, 1);

                    DailyHistory.Children.Add(bar);
                    DailyHistory.Children.Add(date);

                    lastdate = lastdate.AddDays(1);

                    if (lastdate != DateOnly.Parse(DateTime.Now.AddDays(1).ToShortDateString()))
                    {
                        col = new ColumnDefinition();
                        col.Width = new GridLength(80);
                        DailyHistory.ColumnDefinitions.Add(col);
                        column++;
                    }
                }
            }
            catch 
            {
                PublicEntitys.ShowError(500);
            }

            try
            {
                int column = 0;
                int coldate = 0;

                Label time = new Label();
                time.VerticalAlignment = VerticalAlignment.Center;
                time.FontSize = 20;
                double worktime = 0;
                double breaktime = 0;

                MonthlyHistory.ColumnDefinitions.Clear();
                MonthlyHistory.Children.Clear();

                foreach (MonthlyHours _monthlyHours in Database.GetMonthlyHours(MainWindow.configuration.AppSettings.Settings["Username"].Value))
                {
                    if (Int16.Parse(_monthlyHours.Month.ToString()) == coldate)
                    {
                        column--;
                    }
                    else
                    {
                        if (column != 0)
                        {
                            if ((worktime * 0.0625) < breaktime)
                            {
                                int hours = (Int32)TimeSpan.FromHours(worktime - (breaktime - (worktime * 0.0625))).TotalHours;
                                int minutes = (Int32)TimeSpan.FromHours(worktime - (breaktime - (worktime * 0.0625))).Minutes;
                                time.Content = $"{hours.ToString()}:{minutes.ToString()}";
                            }
                            else
                            {
                                int hours = (Int32)TimeSpan.FromHours(worktime).TotalHours;
                                int minutes = (Int32)TimeSpan.FromHours(worktime).Minutes;
                                time.Content = $"{hours.ToString()}:{minutes.ToString()}";
                            }

                            Grid.SetColumn(time, column - 1);
                            MonthlyHistory.Children.Add(time);

                            time = new Label();
                            time.FontSize = 20;
                            time.VerticalAlignment = VerticalAlignment.Center;
                            time.Content = "";
                            worktime = 0;
                            breaktime = 0;
                        }
                    }

                    if (_monthlyHours.StampType == 1)
                    {
                        ReadOnlyBar bar = new ReadOnlyBar();
                        ColumnDefinition col = new ColumnDefinition();
                        Label date = new Label();
                        col.Width = new GridLength(80);
                        MonthlyHistory.ColumnDefinitions.Add(col);
                        date.Content = Months[Int32.Parse(_monthlyHours.Month.ToString()) - 1];
                        date.IsHitTestVisible = true;

                        worktime = worktime + double.Parse(_monthlyHours.Duration.ToString());
                        bar.MaxValue = 160;
                        bar.WorkMargin = 0;
                        bar.WorkHeight = _monthlyHours.Duration;
                        bar.StampType = 1;

                        bar.Id = _monthlyHours.Id;

                        int hours = (Int32)TimeSpan.FromHours(worktime).TotalHours;
                        int minutes = (Int32)TimeSpan.FromHours(worktime).Minutes;
                        bar.ToolTip = $"{hours.ToString()}:{minutes.ToString()}";

                        Grid.SetColumn(bar, column);
                        Grid.SetColumn(date, column);
                        Grid.SetRow(date, 1);

                        MonthlyHistory.Children.Add(bar);
                        MonthlyHistory.Children.Add(date);

                        coldate = Int16.Parse(_monthlyHours.Month.ToString());

                        column++;
                    }
                    else if (_monthlyHours.StampType == 2)
                    {
                        ReadOnlyBar bar = new ReadOnlyBar();
                        ColumnDefinition col = new ColumnDefinition();
                        Label date = new Label();
                        col.Width = new GridLength(80);
                        MonthlyHistory.ColumnDefinitions.Add(col);
                        date.Content = Months[Int32.Parse(_monthlyHours.Month.ToString()) - 1];
                        date.IsHitTestVisible = true;

                        breaktime = breaktime + double.Parse(_monthlyHours.Duration.ToString());
                        bar.MaxValue = 160;
                        bar.WorkMargin = 0;
                        bar.WorkHeight = _monthlyHours.Duration;
                        bar.StampType = 2;

                        bar.Id = _monthlyHours.Id;

                        if ((worktime * 0.0625) < breaktime)
                        {
                            int breakhours = (Int32)TimeSpan.FromHours(breaktime).TotalHours;
                            int breakminutes = (Int32)TimeSpan.FromHours(breaktime).Minutes;
                            int Overdobreakhours = (Int32)TimeSpan.FromHours((breaktime - (worktime * 0.0625))).TotalHours;
                            int Overdobreakminutes = (Int32)TimeSpan.FromHours((breaktime - (worktime * 0.0625))).Minutes;

                            bar.ToolTip = $"Of that breaks:" +
                            $"\n{breakhours.ToString("00")}:{breakminutes.ToString("00")}" +
                            $"\n\nOf that overdo breaks:" +
                            $"\n{Overdobreakhours.ToString("00")}:{Overdobreakminutes.ToString("00")}";
                        }
                        else
                        {
                            int breakhours = (Int32)TimeSpan.FromHours(breaktime).TotalHours;
                            int breakminutes = (Int32)TimeSpan.FromHours(breaktime).Minutes;
                            bar.ToolTip = $"Of that breaks:" +
                            $"\n{breakhours.ToString()}:{breakminutes.ToString()}";
                        }

                        Grid.SetColumn(bar, column);
                        Grid.SetColumn(date, column);
                        Grid.SetRow(date, 1);

                        MonthlyHistory.Children.Add(bar);
                        MonthlyHistory.Children.Add(date);

                        coldate = Int16.Parse(_monthlyHours.Month.ToString());

                        column++;
                    }
                }
                if ((worktime * 0.0625) < breaktime)
                {
                    int hours = (Int32)TimeSpan.FromHours(worktime - (breaktime - (worktime * 0.0625))).TotalHours;
                    int minutes = (Int32)TimeSpan.FromHours(worktime - (breaktime - (worktime * 0.0625))).Minutes;
                    time.Content = $"{hours.ToString()}:{minutes.ToString()}";
                }
                else
                {
                    int hours = (Int32)TimeSpan.FromHours(worktime).TotalHours;
                    int minutes = (Int32)TimeSpan.FromHours(worktime).Minutes;
                    time.Content = $"{hours.ToString()}:{minutes.ToString()}";
                }

                Grid.SetColumn(time, column - 1);
                MonthlyHistory.Children.Add(time);
            }
            catch 
            {
                PublicEntitys.ShowError(500);
            }
        }

        // Click() events for the buttons
        #region Click()
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch
            {
                PublicEntitys.ShowError(306);
            }
        }

        private void Align_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch
            {
                PublicEntitys.ShowError(306);
            }
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch
            {
                PublicEntitys.ShowError(306);
            }
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
