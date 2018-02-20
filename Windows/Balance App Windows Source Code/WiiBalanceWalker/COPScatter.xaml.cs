using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Configurations;

namespace WiiBalanceWalker
{
    /// <summary>
    /// Interaction logic for COPScatter.xaml
    /// </summary>
    public partial class COPScatter : UserControl, INotifyPropertyChanged
    {
        public COPScatter()
        {
            InitializeComponent();

            var r = new Random();
            ValuesA = new ChartValues<ObservablePoint>();
            //ValuesB = new ChartValues<ObservablePoint>();
            //ValuesC = new ChartValues<ObservablePoint>();

            ValuesA.Add(new ObservablePoint(Globals.COPx, Globals.COPy));

            XCoordinates = new[] { -200, -150, -100, -50, 0, 50, 100, 150, 200 };

            YCoordinates = new[] { -100, -50, 0, 50, 100 };

            DataContext = this;

            

        }

        public ChartValues<ObservablePoint> ValuesA { get; set; }
        public ChartValues<ObservablePoint> ValuesB { get; set; }
        public ChartValues<ObservablePoint> ValuesC { get; set; }

        public int[] XCoordinates { get; set; }
        public int[] YCoordinates { get; set; }

        const double boardXmin = -216;
        const double boardXmax = 216;
        const double boardYmin = -114;
        const double boardYmax = 114;

        private void RandomizeOnClick(object sender, RoutedEventArgs e)
        {
            ValuesA[0].X = Globals.COPx;
            ValuesA[0].Y = Globals.COPy;
        }

        public bool IsReading { get; set; }

        private void Read()
        {
            var r = new Random();

            while (IsReading)
            {
                Thread.Sleep(150);
                var now = DateTime.Now;

                //_trend += r.Next(-8, 10);

                ValuesA[0].X = Globals.COPx;
                ValuesA[0].Y = Globals.COPy;

                //lets only use the last 150 values
                //if (ChartValues.Count > 150) ChartValues.RemoveAt(0);
            }
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
