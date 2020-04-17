using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using BLL;
using PL.ViewModel;
using BE;
namespace PL.Views
{
    /// <summary>
    /// Interaction logic for Browse_Report.xaml
    /// </summary>
    public partial class Browse_Report : Window
    {

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }


        // public BrowseReportsViewModel browseViewModel { set; get; }
        /// <summary>
        /// Constructor, initialize the data for the diagrams
        /// </summary>
        public Browse_Report()
        {
            InitializeComponent();

            // CreatePieChart();


            //  browseViewModel = new BrowseReportsViewModel();
            // DataContext = browseViewModel;
            // browseViewModel.getAllReports();

            PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            DataContext = this;

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "North",
                    Values = new ChartValues<double> { 4, 6, 5, 2 ,4 }
                },
                new LineSeries
                {
                    Title = "Center",
                    Values = new ChartValues<double> { 6, 7, 3, 4 ,6 },
                    PointGeometry = null
                },
                new LineSeries
                {
                    Title = "South",
                    Values = new ChartValues<double> { 4,2,7,2,7 },
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15
                }
            };

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString("N");

            //modifying the series collection will animate and update the chart
            SeriesCollection.Add(new LineSeries
            {
                Title = "Other",
                Values = new ChartValues<double> { 5, 3, 2, 4 },
                LineSmoothness = 0, //0: straight lines, 1: really smooth lines
                                    // PointGeometry = Geometry.Parse("m 25 70.36218 20 -28 -20 22 -8 -6 z"),
                PointGeometrySize = 5,
                PointForeground = Brushes.Gray
            });

            //modifying any series values will also animate and update the chart
            SeriesCollection[3].Values.Add(5d);

            DataContext = this;

        }

        public Func<ChartPoint, string> PointLabel { get; set; }

        /* public void CreatePieChart()
          {
              double north = 0, south = 0, center = 0, other = 0;


              foreach(Report r in browseViewModel.getAllReports)
              {

                  if (r.Latitude > 32.144343 && r.Latitude <= 33.333795 && r.Longitude > 34.217333 && r.Longitude < 35.672361)
                  {
                      north++;
                  }

                  // center starts at herzelia 
                  else if (r.Latitude > 31.571548 && r.Latitude <= 32.144343 && r.Longitude > 34.217333 && r.Longitude < 35.672361)
                  {
                      center++;
                  }

                  // south starts at keriyt gat
                  else if (r.Latitude > 29.497512 && r.Latitude <= 31.571548 && r.Longitude > 34.217333 && r.Longitude < 35.672361)
                  {
                      south++;
                  }
                  else
                  {
                      other++;
                  }

              }


          }*/
        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
            {
                series.PushOut = 0;
            }

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }

    }
}