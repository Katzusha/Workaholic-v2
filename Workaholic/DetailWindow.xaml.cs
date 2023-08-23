using MySqlX.XDevAPI.Relational;
using StartStopWork;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
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
using Workaholic;
using System.Windows.Media.Animation;

namespace Workaholic
{
    /// <summary>
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {
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

        public int row = 0;
        RowDefinition rowDefinition = new RowDefinition();
        public DetailWindow(int type, object sender)
        {
            InitializeComponent();

            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            switch (type)
            {
                case 1:
                    ReadWriteBar _ReadWriteBar = (ReadWriteBar)((Grid)((ContextMenu)(sender as MenuItem).Parent).PlacementTarget).Parent;
                    DetailClose.Style = (Style)Resources["GreenButton"];
                    DetailClose.Content = "Save";
                    foreach (DailyHours _dailyHours in Database.GetDailyHoursDetail(configuration.AppSettings.Settings["Username"].Value, _ReadWriteBar.Id))
                    {
                        try
                        {
                            rowDefinition = new RowDefinition();
                            if (_dailyHours.StampType == 1)
                            {
                                rowDefinition.Height = new GridLength(25);
                                Label label = new Label();
                                label.Content = "WORK";
                                label.Name = $"Id{_dailyHours.Id.ToString()}";
                                DetailGrid.RowDefinitions.Add(rowDefinition);
                                Grid.SetRow(label, row);
                                Grid.SetColumnSpan(label, 2);
                                DetailGrid.Children.Add(label);
                                row++;

                                TextBox textbox = new TextBox();
                                textbox.Text = TimeSpan.FromHours(_dailyHours.Start).ToString(@"hh\:mm");
                                textbox.Name = $"Start";
                                rowDefinition = new RowDefinition();
                                rowDefinition.Height = new GridLength(40);
                                DetailGrid.RowDefinitions.Add(rowDefinition);
                                Grid.SetRow(textbox, row);
                                DetailGrid.Children.Add(textbox);

                                textbox = new TextBox();
                                textbox.Text = TimeSpan.FromHours(_dailyHours.End).ToString(@"hh\:mm");
                                textbox.Name = $"End";
                                Grid.SetRow(textbox, row);
                                Grid.SetColumn(textbox, 1);
                                DetailGrid.Children.Add(textbox);
                                row++;
                            }
                            rowDefinition = new RowDefinition();
                            if (_dailyHours.StampType == 2)
                            {
                                rowDefinition.Height = new GridLength(25);
                                Label label = new Label();
                                label.Content = "BREAK";
                                label.Name = $"Id{_dailyHours.Id.ToString()}";
                                DetailGrid.RowDefinitions.Add(rowDefinition);
                                Grid.SetRow(label, row);
                                Grid.SetColumnSpan(label, 2);
                                DetailGrid.Children.Add(label);
                                row++;

                                TextBox textbox = new TextBox();
                                textbox.Text = TimeSpan.FromHours(_dailyHours.Start).ToString(@"hh\:mm");
                                textbox.Name = $"Start";
                                rowDefinition = new RowDefinition();
                                rowDefinition.Height = new GridLength(30);
                                DetailGrid.RowDefinitions.Add(rowDefinition);
                                Grid.SetRow(textbox, row);
                                DetailGrid.Children.Add(textbox);

                                textbox = new TextBox();
                                textbox.Text = TimeSpan.FromHours(_dailyHours.End).ToString(@"hh\:mm");
                                textbox.Name = $"End";
                                Grid.SetRow(textbox, row);
                                Grid.SetColumn(textbox, 1);
                                DetailGrid.Children.Add(textbox);
                                row++;
                            }
                        }
                        catch
                        {
                            PublicEntitys.ShowError(500);
                        }
                    }

                    rowDefinition = new RowDefinition();
                    rowDefinition.Height = new GridLength(25);
                    DetailGrid.RowDefinitions.Add(rowDefinition);
                    Button btn = new Button();
                    btn.Style = (Style)this.Resources["HyperLinkButton"];
                    btn.Content = "New stamp";
                    btn.Margin = new Thickness(0, 0, 0, -15);
                    btn.Click += new RoutedEventHandler(CreateNewStamp);
                    Grid.SetColumnSpan(btn, 2);
                    Grid.SetRow(btn, row);
                    DetailGrid.Children.Add(btn);

                    break;
                case 2:
                    try
                    {
                        _ReadWriteBar = (ReadWriteBar)((Grid)((ContextMenu)(sender as MenuItem).Parent).PlacementTarget).Parent;
                        DetailClose.Style = (Style)this.Resources["HyperLinkButton"];
                        DetailClose.Content = "Close";
                        foreach (DailyHours _dailyHours in Database.GetDailyHoursDetail(configuration.AppSettings.Settings["Username"].Value, _ReadWriteBar.Id))
                        {
                            try
                            {
                                rowDefinition = new RowDefinition();
                                if (_dailyHours.StampType == 1)
                                {
                                    rowDefinition.Height = new GridLength(25);
                                    Label label = new Label();
                                    label.Content = "WORK";
                                    DetailGrid.RowDefinitions.Add(rowDefinition);
                                    Grid.SetRow(label, row);
                                    Grid.SetColumnSpan(label, 2);
                                    DetailGrid.Children.Add(label);
                                    row++;

                                    TextBox textbox = new TextBox();
                                    textbox.Text = TimeSpan.FromHours(_dailyHours.Start).ToString(@"hh\:mm");
                                    textbox.Name = $"Id{_dailyHours.Id.ToString()}";
                                    textbox.IsReadOnly = true;
                                    rowDefinition = new RowDefinition();
                                    rowDefinition.Height = new GridLength(40);
                                    DetailGrid.RowDefinitions.Add(rowDefinition);
                                    Grid.SetRow(textbox, row);
                                    DetailGrid.Children.Add(textbox);

                                    textbox = new TextBox();
                                    textbox.Text = TimeSpan.FromHours(_dailyHours.End).ToString(@"hh\:mm");
                                    textbox.Name = $"Id{_dailyHours.Id.ToString()}";
                                    textbox.IsReadOnly = true;
                                    Grid.SetRow(textbox, row);
                                    Grid.SetColumn(textbox, 1);
                                    DetailGrid.Children.Add(textbox);
                                    row++;
                                }
                                rowDefinition = new RowDefinition();
                                if (_dailyHours.StampType == 2)
                                {
                                    rowDefinition.Height = new GridLength(30);
                                    Label label = new Label();
                                    label.Content = "BREAK";
                                    DetailGrid.RowDefinitions.Add(rowDefinition);
                                    Grid.SetRow(label, row);
                                    Grid.SetColumnSpan(label, 2);
                                    DetailGrid.Children.Add(label);
                                    row++;

                                    TextBox textbox = new TextBox();
                                    textbox.Text = TimeSpan.FromHours(_dailyHours.Start).ToString(@"hh\:mm");
                                    textbox.Name = $"Id{_dailyHours.Id.ToString()}";
                                    textbox.IsReadOnly = true;
                                    rowDefinition = new RowDefinition();
                                    rowDefinition.Height = new GridLength(30);
                                    DetailGrid.RowDefinitions.Add(rowDefinition);
                                    Grid.SetRow(textbox, row);
                                    DetailGrid.Children.Add(textbox);

                                    textbox = new TextBox();
                                    textbox.Text = TimeSpan.FromHours(_dailyHours.End).ToString(@"hh\:mm");
                                    textbox.Name = $"Id{_dailyHours.Id.ToString()}";
                                    textbox.IsReadOnly = true;
                                    Grid.SetRow(textbox, row);
                                    Grid.SetColumn(textbox, 1);
                                    DetailGrid.Children.Add(textbox);
                                }
                            }
                            catch
                            {
                                PublicEntitys.ShowError(500);
                            }
                        }
                    }
                    catch
                    {
                        ReadOnlyBar _ReadOnlyBar = (ReadOnlyBar)((Grid)((ContextMenu)(sender as MenuItem).Parent).PlacementTarget).Parent;
                        DetailClose.Style = (Style)this.Resources["HyperLinkButton"];
                        DetailClose.Content = "Close";
                        foreach (DailyHours _dailyHours in Database.GetDailyHoursDetail(configuration.AppSettings.Settings["Username"].Value, _ReadOnlyBar.Id))
                        {
                            try
                            {
                                rowDefinition = new RowDefinition();
                                if (_dailyHours.StampType == 1)
                                {
                                    rowDefinition.Height = new GridLength(25);
                                    Label label = new Label();
                                    label.Content = "WORK";
                                    DetailGrid.RowDefinitions.Add(rowDefinition);
                                    Grid.SetRow(label, row);
                                    Grid.SetColumnSpan(label, 2);
                                    DetailGrid.Children.Add(label);
                                    row++;

                                    TextBox textbox = new TextBox();
                                    textbox.Text = TimeSpan.FromHours(_dailyHours.Start).ToString(@"hh\:mm");
                                    textbox.Name = $"Id{_dailyHours.Id.ToString()}";
                                    textbox.IsReadOnly = true;
                                    rowDefinition = new RowDefinition();
                                    rowDefinition.Height = new GridLength(40);
                                    DetailGrid.RowDefinitions.Add(rowDefinition);
                                    Grid.SetRow(textbox, row);
                                    DetailGrid.Children.Add(textbox);

                                    textbox = new TextBox();
                                    textbox.Text = TimeSpan.FromHours(_dailyHours.End).ToString(@"hh\:mm");
                                    textbox.Name = $"Id{_dailyHours.Id.ToString()}";
                                    textbox.IsReadOnly = true;
                                    Grid.SetRow(textbox, row);
                                    Grid.SetColumn(textbox, 1);
                                    DetailGrid.Children.Add(textbox);
                                    row++;
                                }
                                rowDefinition = new RowDefinition();
                                if (_dailyHours.StampType == 2)
                                {
                                    rowDefinition.Height = new GridLength(30);
                                    Label label = new Label();
                                    label.Content = "BREAK";
                                    DetailGrid.RowDefinitions.Add(rowDefinition);
                                    Grid.SetRow(label, row);
                                    Grid.SetColumnSpan(label, 2);
                                    DetailGrid.Children.Add(label);
                                    row++;

                                    TextBox textbox = new TextBox();
                                    textbox.Text = TimeSpan.FromHours(_dailyHours.Start).ToString(@"hh\:mm");
                                    textbox.Name = $"Id{_dailyHours.Id.ToString()}";
                                    textbox.IsReadOnly = true;
                                    rowDefinition = new RowDefinition();
                                    rowDefinition.Height = new GridLength(30);
                                    DetailGrid.RowDefinitions.Add(rowDefinition);
                                    Grid.SetRow(textbox, row);
                                    DetailGrid.Children.Add(textbox);

                                    textbox = new TextBox();
                                    textbox.Text = TimeSpan.FromHours(_dailyHours.End).ToString(@"hh\:mm");
                                    textbox.Name = $"Id{_dailyHours.Id.ToString()}";
                                    textbox.IsReadOnly = true;
                                    Grid.SetRow(textbox, row);
                                    Grid.SetColumn(textbox, 1);
                                    DetailGrid.Children.Add(textbox);
                                }
                            }
                            catch
                            {
                                PublicEntitys.ShowError(500);
                            }
                        }
                    }
                    
                    break;
            }
        }

        private void CreateNewStamp(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Grid.SetRow(btn, row + 2);

            rowDefinition = new RowDefinition();
            rowDefinition.Height = new GridLength(30);
            Label label = new Label();
            label.Content = "BREAK";
            label.Name = $"Id0";
            DetailGrid.RowDefinitions.Add(rowDefinition);
            Grid.SetRow(label, row);
            Grid.SetColumnSpan(label, 2);
            DetailGrid.Children.Add(label);
            row++;

            TextBox textbox = new TextBox();
            textbox.Name = $"Start";
            rowDefinition = new RowDefinition();
            rowDefinition.Height = new GridLength(30);
            DetailGrid.RowDefinitions.Add(rowDefinition);
            Grid.SetRow(textbox, row);
            DetailGrid.Children.Add(textbox);

            textbox = new TextBox();
            textbox.Name = $"End";
            Grid.SetRow(textbox, row);
            Grid.SetColumn(textbox, 1);
            DetailGrid.Children.Add(textbox);
            row++;
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
                    int WorkStampId = 0;

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
                                        WorkStampId = _dailyHours.Id;

                                    }
                                    if (minBreak <= _dailyHours.Start && maxBreak >= _dailyHours.End)
                                    {
                                        if (_dailyHours.Id == 0)
                                        {
                                            _dailyHours.Username = WorkStampId.ToString();
                                        }
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

                        if (minBreak <= _dailyHours.Start && maxBreak >= _dailyHours.End)
                        {
                            if (_dailyHours.Id == 0)
                            {
                                _dailyHours.Username = WorkStampId.ToString();
                            }
                            dailyHours.Add(_dailyHours);
                            _dailyHours = new DailyHours();
                        }
                        else
                        {
                            throw new Exception("");
                        }
                    }
                    catch
                    {
                        PublicEntitys.ShowError(406);
                        isCorrectSyntax = false;
                    }
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
