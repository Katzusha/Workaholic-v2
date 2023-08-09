using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.PortableExecutable;
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

namespace StartStopWork
{
    /// <summary>
    /// Interaction logic for Bar.xaml
    /// </summary>
    public partial class Bar : UserControl, INotifyPropertyChanged
    {
        // Create a bar
        public Bar()
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
                try
                {
                    _breakheight = value;
                    UpdateBreakHeight();
                    NotifyPropertyChanged("BreakHeight");
                }
                catch
                {
                    PublicEntitys.ShowError(500);
                }
            }
        }

        // For the bottom margin of the "break" bar
        private double _breakmargin;
        public double BreakMargin
        {
            get { return _breakheight; }
            set
            {
                try
                {
                    _breakmargin = value;
                    UpdateBreakMargin();
                    NotifyPropertyChanged("BreakMargin");
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

        // For height of the "break" bar
        private double _absenceheight;
        public double AbsenceHeight
        {
            get { return _absenceheight; }
            set
            {
                try
                {
                    _absenceheight = value;
                    UpdateAbsenceHeight();
                    NotifyPropertyChanged("AbsenceHeight");
                }
                catch
                {
                    PublicEntitys.ShowError(500);
                }
            }
        }

        // For the bottom margin of the "break" bar
        private double _absencemargin;
        public double AbsenceMargin
        {
            get { return _absencemargin; }
            set
            {
                try
                {
                    _absencemargin = value;
                    UpdateAbsenceMargin();
                    NotifyPropertyChanged("AbsenceMargin");
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
                    UpdateBreakHeight();
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
                    UpdateBreakHeight();
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