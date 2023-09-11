using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Printing;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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

        #endregion

        // Variables
        #region Variables
        // Variable for the labels under neath the time lines
        private static string[] Months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Okt", "Nov", "Dec" };
        private static int Year = DateTime.Now.Year;
        private static int Month = DateTime.Now.Month;
        #endregion

        public SettingsWindow()
        {
            InitializeComponent();

            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight - 10;
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth - 10;

            try
            {
                string btnname = PublicEntitys.configuration.AppSettings.Settings["Align"].Value;
                SurnameInput.Text = PublicEntitys.configuration.AppSettings.Settings["Surname"].Value;
                FirstnameInput.Text = PublicEntitys.configuration.AppSettings.Settings["Firstname"].Value;
                UsernameInput.Text = PublicEntitys.configuration.AppSettings.Settings["Username"].Value;
                PasswordInput.Text = PublicEntitys.configuration.AppSettings.Settings["Password"].Value;

                foreach (Button button in Align.Children)
                {
                    if (button.Name == (("Align" + btnname)))
                    {
                        SolidColorBrush myBrush = new SolidColorBrush();
                        ColorAnimation myColorAnimation = new ColorAnimation();
                        myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("PlateBrush").ToString());
                        myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("PrimaryBrush").ToString());
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
                MonthAndYear.Content = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month)}, {Year.ToString()}";

                DailyHistory.ColumnDefinitions.Clear();
                DailyHistory.Children.Clear();

                List<DailyHours> DailyHoursList = Database.GetDailyHours(PublicEntitys.configuration.AppSettings.Settings["Username"].Value, Year, Month);
                List<DaysOff> DaysOffList = Database.GetDaysOff(PublicEntitys.configuration.AppSettings.Settings["Username"].Value);

                int x = 0;
                int col = 0;
                DateOnly endDate = new DateOnly(Year, Month, DateTime.DaysInMonth(Year, Month));
                DateOnly startDate = new DateOnly(Year, Month, 1);
                for (DateOnly lastdate = startDate; lastdate <= endDate; lastdate = lastdate.AddDays(1))
                {
                    ColumnDefinition _columnDefinition = new ColumnDefinition();
                    _columnDefinition.MinWidth = 80;
                    DailyHistory.ColumnDefinitions.Add(_columnDefinition);
                    Label _date = new Label();
                    _date.HorizontalContentAlignment = HorizontalAlignment.Center;
                    _date.Content = $"{lastdate.ToString("dddd")}" +
                        $"\n{lastdate.Day.ToString()}. {Months[lastdate.Month - 1]}";
                    Grid.SetRow(_date, 1);
                    Grid.SetColumn(_date, col);
                    DailyHistory.Children.Add(_date);

                    List<DailyHours> _dailyHoursList = DailyHoursList.FindAll(x => DateOnly.Parse(x.Date.ToString("yyyy-MM-dd")) == lastdate);

                    if (_dailyHoursList.Count > 0)
                    {
                        if (PublicEntitys.configuration.AppSettings.Settings["AuthLevel"].Value == "1" || PublicEntitys.configuration.AppSettings.Settings["AuthLevel"].Value == "2")
                        {
                            if (lastdate.ToString("dddd") == "Saturday" || lastdate.ToString("dddd") == "Sunday")
                            {
                                ReadOnlyBar _readOnlyBar = new ReadOnlyBar();
                                _readOnlyBar.WorkMargin = 0;
                                _readOnlyBar.WorkHeight = 24;
                                _readOnlyBar.MaxValue = 24;
                                _readOnlyBar.StampType = 3;
                                _readOnlyBar.ToolTip = $"{lastdate.ToString("dddd")}" +
                                    $"\nWeekend";
                                Grid.SetColumn(_readOnlyBar, col);
                                DailyHistory.Children.Add(_readOnlyBar);
                            }
                            else if (DaysOffList.Any(x => DateOnly.Parse(x.Start.ToString("yyyy-MM-dd")) <= lastdate && lastdate <= DateOnly.Parse(x.End.ToString("yyyy-MM-dd"))))
                            {
                                ReadOnlyBar _readOnlyBar = new ReadOnlyBar();
                                _readOnlyBar.WorkMargin = 0;
                                _readOnlyBar.WorkHeight = 24;
                                _readOnlyBar.MaxValue = 24;
                                _readOnlyBar.StampType = 4;
                                _readOnlyBar.ToolTip = $"{lastdate.ToString("dddd")}" +
                                    $"\nDay off";
                                Grid.SetColumn(_readOnlyBar, col);
                                DailyHistory.Children.Add(_readOnlyBar);
                            }
                            else if (lastdate >= DateOnly.Parse(DateTime.Now.ToString()))
                            {
                                ReadOnlyBar _readOnlyBar = new ReadOnlyBar();
                                _readOnlyBar.WorkMargin = 0;
                                _readOnlyBar.WorkHeight = 24;
                                _readOnlyBar.MaxValue = 24;
                                _readOnlyBar.StampType = -1;
                                Grid.SetColumn(_readOnlyBar, col);
                                DailyHistory.Children.Add(_readOnlyBar);
                            }

                            double CombineWorkDuration = 0;
                            double CombineBreakDuration = 0;
                            foreach (DailyHours _dailyHours in _dailyHoursList)
                            {
                                ReadWriteBar _readWriteBar = new ReadWriteBar();
                                _readWriteBar.Id = _dailyHours.Id;
                                _readWriteBar.WorkMargin = _dailyHours.Start;
                                _readWriteBar.WorkHeight = _dailyHours.Duration;
                                _readWriteBar.MaxValue = 24;
                                _readWriteBar.StampType = _dailyHours.StampType;

                                switch (_dailyHours.StampType)
                                {
                                    case 1:
                                        _readWriteBar.ToolTip = $"{lastdate.ToString("dddd")} - WORK" +
                                            $"\nStart: {TimeSpan.FromHours(_dailyHours.Start).ToString(@"hh\:mm")}" +
                                            $"\nEnd: {TimeSpan.FromHours(_dailyHours.End).ToString(@"hh\:mm")}";
                                        CombineWorkDuration = CombineWorkDuration + _dailyHours.Duration;
                                        break;
                                    case 2:
                                        _readWriteBar.ToolTip = $"{lastdate.ToString("dddd")} - BREAK" +
                                            $"\nStart: {TimeSpan.FromHours(_dailyHours.Start).ToString(@"hh\:mm")}" +
                                            $"\nEnd: {TimeSpan.FromHours(_dailyHours.End).ToString(@"hh\:mm")}";
                                        CombineBreakDuration = CombineBreakDuration + _dailyHours.Duration;
                                        break;
                                }

                                Grid.SetColumn(_readWriteBar, col);
                                DailyHistory.Children.Add(_readWriteBar);
                            }

                            Label _time = new Label();
                            _time.VerticalAlignment = VerticalAlignment.Center;
                            _time.FontSize = 20;
                            _time.IsHitTestVisible = false;

                            if ((CombineWorkDuration * 0.0625) < CombineBreakDuration)
                            {
                                _time.Content = TimeSpan.FromHours(CombineWorkDuration - (CombineBreakDuration - (CombineWorkDuration * 0.0625))).ToString(@"hh\:mm");
                            }
                            else
                            {
                                _time.Content = TimeSpan.FromHours(CombineWorkDuration).ToString(@"hh\:mm");
                            }

                            Grid.SetColumn(_time, col);
                            DailyHistory.Children.Add(_time);
                        }
                        else if (PublicEntitys.configuration.AppSettings.Settings["AuthLevel"].Value == "3")
                        {
                            if (lastdate.ToString("dddd") == "Saturday" || lastdate.ToString("dddd") == "Sunday")
                            {
                                ReadOnlyBar _readOnlyBar = new ReadOnlyBar();
                                _readOnlyBar.WorkMargin = 0;
                                _readOnlyBar.WorkHeight = 24;
                                _readOnlyBar.MaxValue = 24;
                                _readOnlyBar.StampType = 3;
                                _readOnlyBar.ToolTip = $"{lastdate.ToString("dddd")}" +
                                    $"\nWeekend";
                                Grid.SetColumn(_readOnlyBar, col);
                                DailyHistory.Children.Add(_readOnlyBar);
                            }
                            else if (DaysOffList.Any(x => DateOnly.Parse(x.Start.ToString("yyyy-MM-dd")) <= lastdate && lastdate <= DateOnly.Parse(x.End.ToString("yyyy-MM-dd"))))
                            {
                                ReadOnlyBar _readOnlyBar = new ReadOnlyBar();
                                _readOnlyBar.WorkMargin = 0;
                                _readOnlyBar.WorkHeight = 24;
                                _readOnlyBar.MaxValue = 24;
                                _readOnlyBar.StampType = 4;
                                _readOnlyBar.ToolTip = $"{lastdate.ToString("dddd")}" +
                                    $"\nDay off";
                                Grid.SetColumn(_readOnlyBar, col);
                                DailyHistory.Children.Add(_readOnlyBar);
                            }
                            else if (lastdate >= DateOnly.Parse(DateTime.Now.ToString("yyyy-MM-dd")))
                            {
                                ReadOnlyBar _readOnlyBar = new ReadOnlyBar();
                                _readOnlyBar.WorkMargin = 0;
                                _readOnlyBar.WorkHeight = 24;
                                _readOnlyBar.MaxValue = 24;
                                _readOnlyBar.StampType = -1;
                                Grid.SetColumn(_readOnlyBar, col);
                                DailyHistory.Children.Add(_readOnlyBar);
                            }

                            double CombineWorkDuration = 0;
                            double CombineBreakDuration = 0;
                            foreach (DailyHours _dailyHours in _dailyHoursList)
                            {
                                ReadOnlyBar _readOnlyBar = new ReadOnlyBar();
                                _readOnlyBar.Id = _dailyHours.Id;
                                _readOnlyBar.WorkMargin = _dailyHours.Start;
                                _readOnlyBar.WorkHeight = _dailyHours.Duration;
                                _readOnlyBar.MaxValue = 24;
                                _readOnlyBar.StampType = _dailyHours.StampType;

                                switch (_dailyHours.StampType)
                                {
                                    case 1:
                                        _readOnlyBar.ToolTip = $"{lastdate.ToString("dddd")} - WORK" +
                                            $"\nStart: {TimeSpan.FromHours(_dailyHours.Start).ToString(@"hh\:mm")}" +
                                            $"\nEnd: {TimeSpan.FromHours(_dailyHours.End).ToString(@"hh\:mm")}";
                                        CombineWorkDuration = CombineWorkDuration + _dailyHours.Duration;
                                        break;
                                    case 2:
                                        _readOnlyBar.ToolTip = $"{lastdate.ToString("dddd")} - BREAK" +
                                            $"\nStart: {TimeSpan.FromHours(_dailyHours.Start).ToString(@"hh\:mm")}" +
                                            $"\nEnd: {TimeSpan.FromHours(_dailyHours.End).ToString(@"hh\:mm")}";
                                        CombineBreakDuration = CombineBreakDuration + _dailyHours.Duration;
                                        break;
                                }

                                Grid.SetColumn(_readOnlyBar, col);
                                DailyHistory.Children.Add(_readOnlyBar);
                            }

                            Label _time = new Label();
                            _time.VerticalAlignment = VerticalAlignment.Center;
                            _time.FontSize = 20;
                            _time.IsHitTestVisible = false;

                            if ((CombineWorkDuration * 0.0625) < CombineBreakDuration)
                            {
                                _time.Content = TimeSpan.FromHours(CombineWorkDuration - (CombineBreakDuration - (CombineWorkDuration * 0.0625))).ToString(@"hh\:mm");
                            }
                            else
                            {
                                _time.Content = TimeSpan.FromHours(CombineWorkDuration).ToString(@"hh\:mm");
                            }

                            Grid.SetColumn(_time, col);
                            DailyHistory.Children.Add(_time);
                        }
                    }
                    else
                    {
                        ReadOnlyBar _readOnlyBar = new ReadOnlyBar();
                        _readOnlyBar.Id = 0;
                        _readOnlyBar.WorkMargin = 0;
                        _readOnlyBar.WorkHeight = 24;
                        _readOnlyBar.MaxValue = 24;

                        if (lastdate.ToString("dddd") == "Saturday" || lastdate.ToString("dddd") == "Sunday")
                        {
                            _readOnlyBar.StampType = 3;
                            _readOnlyBar.ToolTip = $"{lastdate.ToString("dddd")}" +
                                $"\nWeekend";

                            Grid.SetColumn(_readOnlyBar, col);
                            DailyHistory.Children.Add(_readOnlyBar);
                        }
                        else if (DaysOffList.Any(x => DateOnly.Parse(x.Start.ToString("yyyy-MM-dd")) <= lastdate && lastdate <= DateOnly.Parse(x.End.ToString("yyyy-MM-dd"))))
                        {
                            _readOnlyBar.StampType = 4;
                            _readOnlyBar.ToolTip = $"{lastdate.ToString("dddd")}" +
                                $"\nDay off";

                            Grid.SetColumn(_readOnlyBar, col);
                            DailyHistory.Children.Add(_readOnlyBar);
                        }
                        else if (lastdate >= DateOnly.Parse(DateTime.Now.ToString("yyyy-MM-dd")))
                        {
                            _readOnlyBar.StampType = -1;

                            Grid.SetColumn(_readOnlyBar, col);
                            DailyHistory.Children.Add(_readOnlyBar);
                        }
                        else
                        {
                            _readOnlyBar.StampType = 0;
                            _readOnlyBar.ToolTip = $"{lastdate.ToString("dddd")}" +
                                $"\nUn-planned absence";

                            Grid.SetColumn(_readOnlyBar, col);
                            DailyHistory.Children.Add(_readOnlyBar);

                            Label _time = new Label();
                            _time.VerticalAlignment = VerticalAlignment.Center;
                            _time.FontSize = 20;
                            _time.Content = TimeSpan.FromHours(0).ToString(@"hh\:mm");
                            Grid.SetColumn(_time, col);
                            DailyHistory.Children.Add(_time);
                        }
                    }

                    col++;
                }
            }
            catch 
            {
                PublicEntitys.ShowError(500);
            }

            try
            {
                MonthlyHistory.ColumnDefinitions.Clear();
                MonthlyHistory.Children.Clear();

                List<MonthlyHours> MonthlyHoursList = Database.GetMonthlyHours(PublicEntitys.configuration.AppSettings.Settings["Username"].Value);

                int x = 0;
                int col = 0;
                for (DateOnly lastdate = DateOnly.Parse(DateTime.Now.AddMonths(-23).ToString("yyyy-MM-dd")); lastdate.ToString("yyyy-MM") != DateTime.Now.AddMonths(1).ToString("yyyy-MM"); lastdate = lastdate.AddMonths(1))
                {
                    int NumberOfDays = DateTime.DaysInMonth(lastdate.Year, lastdate.Month);

                    ColumnDefinition _columnDefinition = new ColumnDefinition();
                    _columnDefinition.MinWidth = 100;
                    MonthlyHistory.ColumnDefinitions.Add(_columnDefinition);

                    Label _date = new Label();
                    _date.Content = $"{lastdate.ToString("MMMM")}\n{lastdate.ToString("yyyy")}";
                    Grid.SetRow(_date, 1);
                    Grid.SetColumn(_date, col);
                    MonthlyHistory.Children.Add(_date);

                    List<MonthlyHours> _monthlyHoursList = MonthlyHoursList.FindAll(x => x.Month == lastdate.Month && x.Year == lastdate.Year);

                    if (_monthlyHoursList.Count > 0)
                    {
                        double worktime = 0;
                        double breaktime = 0;
                        foreach (MonthlyHours _monthlyHours in _monthlyHoursList)
                        {
                            ReadOnlyBar _readOnlyBar = new ReadOnlyBar();
                            _readOnlyBar.Id = _monthlyHours.Id;
                            _readOnlyBar.WorkHeight = _monthlyHours.Duration;
                            _readOnlyBar.MaxValue = NumberOfDays * 8;
                            _readOnlyBar.StampType = _monthlyHours.StampType;

                            switch (_monthlyHours.StampType)
                            {
                                case 1:
                                    _readOnlyBar.ToolTip = $"{lastdate.ToString("MMMM")} - WORK" +
                                        $"\nWorking hours: {TimeSpan.FromHours(_monthlyHours.Duration).TotalHours.ToString()}:{(TimeSpan.FromHours(_monthlyHours.Duration).TotalMinutes - (TimeSpan.FromHours(_monthlyHours.Duration).TotalHours * 60)).ToString("00")}";
                                    worktime = worktime + _monthlyHours.Duration;
                                    break;
                                case 2:
                                    _readOnlyBar.ToolTip = $"{lastdate.ToString("MMMM")} - BREAK" +
                                        $"\nOf that breaks: {TimeSpan.FromHours(_monthlyHours.Duration).ToString(@"hh\:mm")}";
                                    breaktime = breaktime + _monthlyHours.Duration;

                                    if ((worktime * 0.0625) < breaktime)
                                    {
                                        int breakhours = (Int32)TimeSpan.FromHours(breaktime).TotalHours;
                                        int breakminutes = (Int32)TimeSpan.FromHours(breaktime).Minutes;
                                        int Overdobreakhours = (Int32)TimeSpan.FromHours((breaktime - (worktime * 0.0625))).TotalHours;
                                        int Overdobreakminutes = (Int32)TimeSpan.FromHours((breaktime - (worktime * 0.0625))).Minutes;

                                        _readOnlyBar.ToolTip = $"Of that breaks:" +
                                        $"\n{breakhours.ToString("00")}:{breakminutes.ToString("00")}" +
                                        $"\n\nOf that overdo breaks:" +
                                        $"\n{Overdobreakhours.ToString("00")}:{Overdobreakminutes.ToString("00")}";
                                    }
                                    else
                                    {
                                        int breakhours = (Int32)TimeSpan.FromHours(breaktime).TotalHours;
                                        int breakminutes = (Int32)TimeSpan.FromHours(breaktime).Minutes;
                                        _readOnlyBar.ToolTip = $"Of that breaks:" +
                                        $"\n{breakhours.ToString()}:{breakminutes.ToString()}";
                                    }
                                    break;
                            }

                            Grid.SetColumn(_readOnlyBar, col);
                            MonthlyHistory.Children.Add(_readOnlyBar);
                        }

                        Label _time = new Label();
                        _time.VerticalAlignment = VerticalAlignment.Center;
                        _time.FontSize = 20;
                        _time.IsHitTestVisible = false;

                        if ((worktime * 0.0625) < breaktime)
                        {
                            int hours = (Int32)TimeSpan.FromHours(worktime - (breaktime - (worktime * 0.0625))).TotalHours;
                            int minutes = (Int32)TimeSpan.FromHours(worktime - (breaktime - (worktime * 0.0625))).Minutes;
                            _time.Content = $"{hours.ToString()}:{minutes.ToString("00")}";
                        }
                        else
                        {
                            _time.Content = $"{TimeSpan.FromHours(worktime).TotalHours.ToString()}:{(TimeSpan.FromHours(worktime).TotalMinutes - (TimeSpan.FromHours(worktime).TotalHours * 60)).ToString("00")}";
                        }

                        Grid.SetColumn(_time, col);
                        MonthlyHistory.Children.Add(_time);
                    }
                    else
                    {
                        ReadOnlyBar _readOnlyBar = new ReadOnlyBar();
                        _readOnlyBar.Id = 0;
                        _readOnlyBar.WorkMargin = 0;
                        _readOnlyBar.WorkHeight = NumberOfDays * 8;
                        _readOnlyBar.MaxValue = 160;

                        _readOnlyBar.StampType = 0;
                        _readOnlyBar.ToolTip = $"{lastdate.ToString("MMMM")}" +
                            $"\nUn-planned absence";

                        Grid.SetColumn(_readOnlyBar, col);
                        MonthlyHistory.Children.Add(_readOnlyBar);

                        Label _time = new Label();
                        _time.VerticalAlignment = VerticalAlignment.Center;
                        _time.FontSize = 20;
                        _time.Content = TimeSpan.FromHours(0).ToString(@"hh\:mm");
                        Grid.SetColumn(_time, col);
                        MonthlyHistory.Children.Add(_time);
                    }

                    col++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("PlateBrush").ToString());
                myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("PrimaryBrush").ToString());
                myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
                myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
                btn.BorderBrush = myBrush;

                PublicEntitys.configuration.AppSettings.Settings["Align"].Value = btn.Name.ToString().Replace("Align", "");
                PublicEntitys.configuration.Save(ConfigurationSaveMode.Full, true);
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
                PublicEntitys.configuration.AppSettings.Settings["Firstname"].Value = "";
                PublicEntitys.configuration.AppSettings.Settings["Surname"].Value = "";
                PublicEntitys.configuration.AppSettings.Settings["Username"].Value = "";
                PublicEntitys.configuration.AppSettings.Settings["Password"].Value = "";
                PublicEntitys.configuration.Save(ConfigurationSaveMode.Full, true);
                ConfigurationManager.RefreshSection("appSettings");

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

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void PreviusMonth_Click(object sender, RoutedEventArgs e)
        {
            Month--;
            if (Month == 0)
            {
                Year--;
                Month = 12;
            }

            RefreshDailyGrid();
        }

        private void NextMonth_Click(object sender, RoutedEventArgs e)
        {
            Month++;
            if (Month == 13)
            {
                Year++;
                Month = 1;
            }

            RefreshDailyGrid();
        }
    }
}
