using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Workaholic
{
    /// <summary>
    /// Interaction logic for ReadOnlyBar.xaml
    /// </summary>
    public partial class ReadOnlyBar : UserControl
    {
        public ReadOnlyBar()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // I have no clue what this does but the tutorial made it sooooo.....
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        private int _StampType;
        public int StampType
        {
            get { return _StampType; }
            set
            {
                _StampType = value;
                if (_StampType == 1)
                {
                    //Animations for buttons background color to transforme it from transparrent to red
                    SolidColorBrush myBrush = new SolidColorBrush();
                    ColorAnimation myColorAnimation = new ColorAnimation();
                    myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("BorderColor").ToString());
                    myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("BorderColor").ToString());
                    myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
                    myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
                    Work.Background = myBrush;
                }
                else if (_StampType == 2)
                {
                    //Animations for buttons background color to transforme it from transparrent to red
                    SolidColorBrush myBrush = new SolidColorBrush();
                    ColorAnimation myColorAnimation = new ColorAnimation();
                    myColorAnimation.From = (Color)ColorConverter.ConvertFromString(FindResource("BreakColor").ToString());
                    myColorAnimation.To = (Color)ColorConverter.ConvertFromString(FindResource("BreakColor").ToString());
                    myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
                    myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
                    Work.Background = myBrush;
                }
            }
        }

        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }

        // For height of the "break" bar
        private double _workheight;
        public double WorkHeight
        {
            get { return _workheight; }
            set
            {
                _workheight = value;
                UpdateWorkHeight();
                NotifyPropertyChanged("WorkHeight");
            }
        }

        // For the bottom margin of the "break" bar
        private double _workmargin;
        public double WorkMargin
        {
            get { return _workmargin; }
            set
            {
                _workmargin = value;
                UpdateWorkMargin();
                NotifyPropertyChanged("WorkMargin");
            }
        }

        // For the calculations of the heights of the bars
        private double maxValue;
        public double MaxValue
        {
            get { return maxValue; }
            set
            {
                maxValue = value;
                UpdateWorkHeight();
                NotifyPropertyChanged("MaxValue");
            }
        }

        private string _thisvalue;
        public string ThisValue
        {
            get { return _thisvalue; }
            set
            {
                _thisvalue = value;
                //UpdateBreakHeight();
                UpdateWorkHeight();
                NotifyPropertyChanged("Value");
            }
        }

        // Update bar height
        private void UpdateWorkHeight()
        {
            if (maxValue > 0)
            {
                var percent = (_workheight * 100) / maxValue;

                Work.Height = ((percent) * BarSize.ActualHeight) / 100;
            }
        }

        // Update bottom margin of the bar
        private void UpdateWorkMargin()
        {
            if (maxValue > 0)
            {
                var percent = _workmargin / maxValue;

                Work.Margin = new Thickness(0, 0, 0, ((percent) * BarSize.ActualHeight));
            }
        }

        // Update the bar height every time the bar is loaded
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateWorkHeight();
            UpdateWorkMargin();
        }

        // Update the bar height every time the grid is resized
        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateWorkHeight();
            UpdateWorkMargin();
        }
    }
}
