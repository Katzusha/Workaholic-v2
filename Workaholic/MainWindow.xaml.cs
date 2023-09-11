using StartStopWork;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Workaholic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Stopwatch WorkStopWatch = new Stopwatch();
        public Stopwatch BreakStopWatch = new Stopwatch();
        public bool isBreakTime;
        public bool isFirstBreakTime;
        public static bool isLoggedOut;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;
        }

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                if (Database.TryConnection() == false)
                {
                    PublicEntitys.ShowError(400);
                }

                isBreakTime = false;
                isFirstBreakTime = true;

                SetPosition();

                //Declare new clock
                DispatcherTimer timer = new DispatcherTimer();

                //Update clock every 5 seconds
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += tickevent;
                timer.Start();

                StartStop.Width = 175;

                isLoggedOut = false;

                CheckLogIn();
            }
            catch
            {
                PublicEntitys.ShowError(306);
            }
        }

        public void CheckLogIn()
        {
            if (PublicEntitys.configuration.AppSettings.Settings["Username"].Value == "")
            {
                this.Hide();

                LogInWindow window = new LogInWindow();

                isLoggedOut = true;

                window.ShowDialog();

                this.Show();
            }
        }

        public void SetPosition()
        {
            try
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                //Set all the variables from app.config file
                string align = (configuration.AppSettings.Settings["Align"].Value);
                var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
                switch (align)
                {
                    case "TopLeft":
                        this.Left = 10;
                        this.Top = 10;
                        break;
                    case "TopMiddle":
                        this.Left = (desktopWorkingArea.Right / 2) - (this.Width / 2);
                        this.Top = 10;
                        break;
                    case "TopRight":
                        this.Left = desktopWorkingArea.Right - this.Width - 10;
                        this.Top = 10;
                        break;
                    case "MiddleLeft":
                        this.Left = 10;
                        this.Top = (desktopWorkingArea.Bottom / 2) - (this.Height / 2);
                        break;
                    case "MiddleMiddle":
                        this.Top = (desktopWorkingArea.Bottom / 2) - (this.Height / 2);
                        this.Left = (desktopWorkingArea.Right / 2) - (this.Width / 2);
                        break;
                    case "MiddleRight":
                        this.Top = (desktopWorkingArea.Bottom / 2) - (this.Height / 2);
                        this.Left = desktopWorkingArea.Right - this.Width - 10;
                        break;
                    case "BottomLeft":
                        this.Top = desktopWorkingArea.Bottom - this.Height - 10;
                        this.Left = 10;
                        break;
                    case "BottomMiddle":
                        this.Top = desktopWorkingArea.Bottom - this.Height - 10;
                        this.Left = (desktopWorkingArea.Right / 2) - (this.Width / 2);
                        break;
                    case "BottomRight":
                        this.Top = desktopWorkingArea.Bottom - this.Height - 10;
                        this.Left = desktopWorkingArea.Right - this.Width - 10;
                        break;
                }
            }
            catch
            {
                PublicEntitys.ShowError(306);
            }
        }

        private void tickevent(object sender, EventArgs e)
        {
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = WorkStopWatch.Elapsed;

            // Format and display the TimeSpan value. 
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
            WorkTime.Content = (elapsedTime);

            TimeSpan breakts = BreakStopWatch.Elapsed;

            if (isBreakTime)
            {
                string BreakElapsedTime = String.Format("{0:00}:{1:00}:{2:00}", breakts.Hours, breakts.Minutes, breakts.Seconds);
                BreakTime.Content = (BreakElapsedTime);
            }
        }

        private void StartStop_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isStart)
            {
                //Animations for buttons background color to transforme it from transparrent to red
                SolidColorBrush myBrush = new SolidColorBrush();
                ColorAnimation myColorAnimation = new ColorAnimation();
                myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundBrush").ToString());
                myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("RedBrush").ToString());
                myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
                myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
                StartStop.Background = myBrush;
            }
            else if (isStart == false)
            {
                //Animations for buttons background color to transforme it from transparrent to red
                SolidColorBrush myBrush = new SolidColorBrush();
                ColorAnimation myColorAnimation = new ColorAnimation();
                myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundBrush").ToString());
                myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("GreenBrush").ToString());
                myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
                myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
                StartStop.Background = myBrush;
            }
        }
        private void StartStop_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isStart)
            {
                //Animations for buttons background color to transforme it from transparrent to red
                SolidColorBrush myBrush = new SolidColorBrush();
                ColorAnimation myColorAnimation = new ColorAnimation();
                myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("RedBrush").ToString());
                myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundBrush").ToString());
                myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
                myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
                StartStop.Background = myBrush;
            }
            else if (isStart == false)
            {
                //Animations for buttons background color to transforme it from transparrent to red
                SolidColorBrush myBrush = new SolidColorBrush();
                ColorAnimation myColorAnimation = new ColorAnimation();
                myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("GreenBrush").ToString());
                myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundBrush").ToString());
                myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
                myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
                StartStop.Background = myBrush;
            }
        }

        private void Break_MouseLeave(object sender, MouseEventArgs e)
        {
            //Animations for buttons background color to transforme it from transparrent to red
            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation myColorAnimation = new ColorAnimation();
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("BreakBrush").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundBrush").ToString());
            myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
            Break.Background = myBrush;
        }
        private void Break_MouseEnter(object sender, MouseEventArgs e)
        {
            //Animations for buttons background color to transforme it from transparrent to red
            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation myColorAnimation = new ColorAnimation();
            myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("BackgroundBrush").ToString());
            myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("BreakBrush").ToString());
            myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
            Break.Background = myBrush;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (isStart)
                {
                    WorkStopWatch.Stop();
                    // Get the elapsed time as a TimeSpan value.
                    TimeSpan ts = WorkStopWatch.Elapsed;

                    // Format and display the TimeSpan value. 
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
                    WorkTime.Content = (elapsedTime);
                    WorkStopWatch.Stop();

                    Database.PostStamp(1, null, null, null, PublicEntitys.configuration.AppSettings.Settings["Username"].Value);

                    string exualtime = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
                    WorkTime.Content = (exualtime);
                }
            }
            catch
            {
                PublicEntitys.ShowError(306);
            }
        }

        bool isStart = false;
        private void StartStop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isStart)
                {
                    Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    if (Database.PostStamp(4, null, null, null, configuration.AppSettings.Settings["Username"].Value))
                    {
                        //Animations for buttons background color to transforme it from transparrent to red
                        SolidColorBrush myBrush = new SolidColorBrush();
                        ColorAnimation myColorAnimation = new ColorAnimation();
                        myColorAnimation.From = Color.FromArgb(255, 255, 86, 86);
                        myColorAnimation.To = Color.FromArgb(255, 86, 200, 86);
                        myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
                        myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
                        StartStop.Background = myBrush;

                        DoubleAnimation WidthAnimation = new DoubleAnimation();
                        WidthAnimation.From = 50;
                        WidthAnimation.To = 175;
                        WidthAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
                        StartStop.BeginAnimation(WidthProperty, WidthAnimation);

                        StartStop.Content = "▶";
                        isStart = false;
                        WorkStopWatch.Stop();

                        Break.Content = "BREAK";
                        isFirstBreakTime = true;
                        isBreakTime = false;
                        BreakStopWatch.Stop();
                    }
                }
                else if (isStart == false)
                {
                    Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    if ((configuration.AppSettings.Settings["Username"].Value) == "")
                    {
                        this.Hide();

                        LogInWindow window = new LogInWindow();
                        window.ShowDialog();

                        this.Show();
                        return;
                    }

                    if (Database.PostStamp(1, null, null, null, configuration.AppSettings.Settings["Username"].Value))
                    {
                        //Animations for buttons background color to transforme it from transparrent to red
                        SolidColorBrush myBrush = new SolidColorBrush();
                        ColorAnimation myColorAnimation = new ColorAnimation();
                        myColorAnimation.From = Color.FromArgb(255, 86, 200, 86);
                        myColorAnimation.To = Color.FromArgb(255, 255, 86, 86);
                        myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
                        myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
                        StartStop.Background = myBrush;

                        DoubleAnimation WidthAnimation = new DoubleAnimation();
                        WidthAnimation.From = 175;
                        WidthAnimation.To = 50;
                        WidthAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
                        StartStop.BeginAnimation(WidthProperty, WidthAnimation);

                        WorkStopWatch.Restart();
                        WorkStopWatch.Start();

                        StartStop.Content = "■";
                        isStart = true;

                        BreakTime.Content = "00:00:00";
                    }
                }
            }
            catch
            {
                PublicEntitys.ShowError(306);
            }
        }

        private void Break_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isFirstBreakTime)
                {
                    if (Database.PostStamp(2, null, null, null, PublicEntitys.configuration.AppSettings.Settings["Username"].Value))
                    {
                        isBreakTime = true;
                        isFirstBreakTime = false;

                        BreakStopWatch.Restart();
                        BreakStopWatch.Start();

                        Break.Content = "WORK";
                    }
                }
                else
                {
                    if (isBreakTime)
                    {
                        if (Database.PostStamp(3, null, null, null, PublicEntitys.configuration.AppSettings.Settings["Username"].Value))
                        {
                            BreakStopWatch.Stop();
                            isBreakTime = false;

                            Break.Content = "BREAK";
                        }
                    }
                    else
                    {
                        if (Database.PostStamp(2, null, null, null, PublicEntitys.configuration.AppSettings.Settings["Username"].Value))
                        {
                            BreakStopWatch.Start();
                            isBreakTime = true;

                            Break.Content = "WORK";
                        }
                    }
                }
            }
            catch
            {
                PublicEntitys.ShowError(306);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SettingsWindow settingswindow = new SettingsWindow();

                settingswindow.ShowDialog();

                SetPosition();

                CheckLogIn();
            }
            catch
            {
                PublicEntitys.ShowError(306);
            }
        }
    }
}
