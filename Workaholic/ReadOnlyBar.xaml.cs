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

        // For height of the "break" bar
        private double _breakheight;
        public double BreakHeight
        {
            get { return _breakheight; }
            set
            {
                _breakheight = value;
                UpdateBreakHeight();
                NotifyPropertyChanged("BreakHeight");
            }
        }

        // For the bottom margin of the "break" bar
        private double _breakmargin;
        public double BreakMargin
        {
            get { return _breakheight; }
            set
            {
                _breakmargin = value;
                UpdateBreakMargin();
                NotifyPropertyChanged("BreakMargin");
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

        // For height of the "break" bar
        private double _absenceheight;
        public double AbsenceHeight
        {
            get { return _absenceheight; }
            set
            {
                _absenceheight = value;
                UpdateAbsenceHeight();
                NotifyPropertyChanged("AbsenceHeight");
            }
        }

        // For the bottom margin of the "break" bar
        private double _absencemargin;
        public double AbsenceMargin
        {
            get { return _absencemargin; }
            set
            {
                _absencemargin = value;
                UpdateAbsenceMargin();
                NotifyPropertyChanged("AbsenceMargin");
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
                UpdateBreakHeight();
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
                UpdateBreakHeight();
                UpdateWorkHeight();
                NotifyPropertyChanged("Value");
            }
        }

        // Update bar height
        private void UpdateBreakHeight()
        {
            if (maxValue > 0)
            {
                var percent = (_breakheight / maxValue) * 100;

                Break.Height = ((percent) * BarSize.ActualHeight) / 100;
            }
        }

        // Update bottom margin of the bar
        private void UpdateBreakMargin()
        {
            if (maxValue > 0)
            {
                var percent = _breakmargin / maxValue;

                Break.Margin = new Thickness(0, 0, 0, ((percent) * BarSize.ActualHeight));
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

        // Update bar height
        private void UpdateAbsenceHeight()
        {
            if (maxValue > 0)
            {
                var percent = (_absenceheight * 100) / maxValue;

                Absence.Height = ((percent) * BarSize.ActualHeight) / 100;
            }
        }

        // Update bottom margin of the bar
        private void UpdateAbsenceMargin()
        {
            if (maxValue > 0)
            {
                var percent = _absencemargin / maxValue;

                Absence.Margin = new Thickness(0, 0, 0, ((percent) * BarSize.ActualHeight));
            }
        }

        // Update the bar height every time the bar is loaded
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateBreakHeight();
            UpdateBreakMargin();
            UpdateWorkHeight();
            UpdateWorkMargin();
            UpdateAbsenceHeight();
            UpdateAbsenceMargin();
        }

        // Update the bar height every time the grid is resized
        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateBreakHeight();
            UpdateBreakMargin();
            UpdateWorkHeight();
            UpdateWorkMargin();
            UpdateAbsenceHeight();
            UpdateAbsenceMargin();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
