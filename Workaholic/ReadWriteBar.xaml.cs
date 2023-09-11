using StartStopWork;
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
    /// Interaction logic for ReadWriteBar.xaml
    /// </summary>
    public partial class ReadWriteBar : UserControl
    {
        public ReadWriteBar()
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
                switch (_StampType)
                {
                    case -1:
                        Work.Background = Brushes.Transparent;
                        break;
                    case 0:
                        Work.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("AbsenceBrush").ToString()));
                        break;
                    case 1:
                        Work.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("WorkBrush").ToString()));
                        break;
                    case 2:
                        Work.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("BreakBrush").ToString()));
                        break;
                    case 3:
                        Work.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("WeekendBrush").ToString()));
                        break;
                    case 4:
                        Work.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("FreeBrush").ToString()));
                        break;
                }
            }
        }

        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                try
                {
                    _id = value;
                }
                catch
                {
                    PublicEntitys.ShowError(500);
                }
            }
        }

        // For height of the "break" bar
        private double _workheight;
        public double WorkHeight
        {
            get { return _workheight; }
            set
            {
                try
                {
                    _workheight = value;
                    UpdateWorkHeight();
                    NotifyPropertyChanged("WorkHeight");
                }
                catch
                {
                    PublicEntitys.ShowError(500);
                }
            }
        }

        // For the bottom margin of the "break" bar
        private double _workmargin;
        public double WorkMargin
        {
            get { return _workmargin; }
            set
            {
                try
                {
                    _workmargin = value;
                    UpdateWorkMargin();
                    NotifyPropertyChanged("WorkMargin");
                }
                catch
                {
                    PublicEntitys.ShowError(500);
                }
            }
        }

        // For the calculations of the heights of the bars
        private double maxValue;
        public double MaxValue
        {
            get { return maxValue; }
            set
            {
                try
                {
                    maxValue = value;
                    UpdateWorkHeight();
                    NotifyPropertyChanged("MaxValue");
                }
                catch
                {
                    PublicEntitys.ShowError(500);
                }
            }
        }

        private string _thisvalue;
        public string ThisValue
        {
            get { return _thisvalue; }
            set
            {
                try
                {
                    _thisvalue = value;
                    UpdateWorkHeight();
                    NotifyPropertyChanged("Value");
                }
                catch
                {
                    PublicEntitys.ShowError(500);
                }
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
