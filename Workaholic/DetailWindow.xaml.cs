using StartStopWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.RegularExpressions;
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
            try
            {
                if(DetailClose.Content == "Save")
                {
                    List<DailyHours> dailyHours = new List<DailyHours>();
                    DailyHours _dailyHours = new DailyHours();
                    Regex regex = new Regex(@"\d\d:\d\d");
                    bool isFirst = true;
                    bool isWork = true;
                    bool isCorrectSyntax = true;

                    double minBreak = 0;
                    double maxBreak = 0;
                    try
                    {
                        foreach (object item in DetailGrid.Children)
                        {
                            if (item.GetType().ToString() == "System.Windows.Controls.Label")
                            {
                                if (isFirst != true)
                                {
                                    if (isWork)
                                    {
                                        minBreak = _dailyHours.Start;
                                        maxBreak = _dailyHours.End;
                                    }
                                    if (minBreak <= _dailyHours.Start && maxBreak >= _dailyHours.End)
                                    {
                                        dailyHours.Add(_dailyHours);
                                        _dailyHours = new DailyHours();
                                    }
                                    else
                                    {
                                        throw new Exception("");
                                    }
                                }
                                Label label = (Label)item;
                                _dailyHours.Id = Int32.Parse(label.Name.Replace("Id", ""));
                                if (label.Content == "WORK")
                                {
                                    isWork = true;
                                }
                                else
                                {
                                    isWork = false;
                                }

                                isFirst = false;
                            }
                            else if (item.GetType().ToString() == "System.Windows.Controls.TextBox")
                            {
                                TextBox textbox = (TextBox)item;
                                TimeSpan timeonly = TimeSpan.Parse(textbox.Text);
                                if (textbox.Name == "Start")
                                {
                                    _dailyHours.Start = (timeonly.TotalMinutes / 60);
                                }
                                else if (textbox.Name == "End")
                                {
                                    _dailyHours.End = (timeonly.TotalMinutes / 60);
                                    if (_dailyHours.Start > _dailyHours.End)
                                    {
                                        throw new Exception("");
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        PublicEntitys.ShowError(406);
                        isCorrectSyntax = false;
                    }

                    dailyHours.Add(_dailyHours);
                    if (isCorrectSyntax)
                    {
                        if (Database.UpdateDailyHoursDetail(PublicEntitys.configuration.AppSettings.Settings["Username"].Value, dailyHours))
                        {
                            this.Close();
                        }
                    }
                }
                else if (DetailClose.Content == "Close")
                {
                    this.Close();
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
    }
}
