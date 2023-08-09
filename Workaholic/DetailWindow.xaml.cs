using StartStopWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
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
using Workaholic.Models;

namespace Workaholic
{
    /// <summary>
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {
        public DetailWindow()
        {
            InitializeComponent();
        }

        private void DetailClose_Click(object sender, RoutedEventArgs e)
        {
            List<DailyHours> dailyHours = new List<DailyHours>();
            DailyHours _dailyHours = new DailyHours();
            bool isFirst = true;
            foreach(object item in DetailGrid.Children)
            {
                if (item.GetType().ToString() == "System.Windows.Controls.Label")
                {
                    if (isFirst != true)
                    {
                        dailyHours.Add(_dailyHours);
                        _dailyHours = new DailyHours();
                    }
                    Label label = (Label)item;
                    _dailyHours.Id = Int32.Parse(label.Name.Replace("Id", ""));
                    isFirst = false;
                }
                else if (item.GetType().ToString() == "System.Windows.Controls.TextBox")
                {
                    TextBox textbox = (TextBox)item;
                    TimeSpan timeonly = TimeSpan.Parse(textbox.Text);
                    double time = (timeonly.Hours * 60) + timeonly.Minutes;
                    if (textbox.Name == "Start")
                    {
                        _dailyHours.Start = (time / 60);
                    }
                    else if (textbox.Name == "End")
                    {
                        _dailyHours.End = (time / 60);
                    }
                }
            }

            dailyHours.Add(_dailyHours);

            if (Database.UpdateDailyHoursDetail(PublicEntitys.configuration.AppSettings.Settings["Username"].Value, dailyHours))
            {

                this.Close();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
