using BE;
using Microsoft.Maps.MapControl.WPF;
using PL.Model;
using System;
using MColor = System.Windows.Media.Color;
using DColor = System.Drawing.Color;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Device.Location;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media;
using System.Windows.Controls;

namespace PL.ViewModel
{
    /// <summary>
    /// ViewModel for the clutering window
    /// </summary>
    class DataAnalysisViewModel : INotifyPropertyChanged
    {
        public MissleModel currentModel { set; get; }
        public ObservableCollection<Report> Reports { get; set; }
        public ObservableCollection<Hit> Hits { get; set; }
        public ObservableCollection<Pushpin> pushpins { get; set; }
        public ObservableCollection<GeoCoordinate> K_means_collection { get; set; }
        private double _ZoomLevel;
        public double ZoomLevel
        {
            set
            {
                _ZoomLevel = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ZoomLevel"));
            }
            get { return _ZoomLevel; }
        }
        public DateTime start { set; get; }
        public DateTime end { set; get; }
        public string eventNum { set; get; } = "Numbers only";
        private ComboBoxItem _newItem = new ComboBoxItem();
        public ComboBoxItem newItem
        {
            get { return _newItem; }
            set
            {
                _newItem = value;
                K_Means = _newItem.Content;
            }
        }

        public object K_Means { set; get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public DataAnalysisViewModel()
        {
            ZoomLevel = 10.0;
            CenterLocation = new Location(31.5, 35);
            K_means_collection = new ObservableCollection<GeoCoordinate>();
            Hits = new ObservableCollection<Hit>();
            Reports = new ObservableCollection<Report>();
            currentModel = new MissleModel();
            CenterLocation = new Location();
            pushpins = new ObservableCollection<Pushpin>();
            start = DateTime.Now;
            end = DateTime.Now;
        }

        /// <summary>
        /// display all current Report records
        /// </summary>
        public async void allCurrentReport()
        {
            pushpins.Clear();
            Reports.Clear();
            List<Report> result = new List<Report>();
            if (end != null)
            {
                //var allreport = await currentModel.getReportsByDates(start, end);
                var allreport = await currentModel.allReports();
                foreach (var item in allreport)
                {
                    updateReportsAndPushPins(item);
                    result.Add(item);
                }
                CenterLocation = currentModel.Center(result);
                ZoomLevel = 7.2;
            }
            else
            {
                DateTime tempend = start;
                tempend.AddMinutes(10 * Convert.ToInt32(eventNum));
                var allreport = await currentModel.getReportsByDates(start, tempend);
                foreach (var item in allreport)
                {
                    updateReportsAndPushPins(item);
                    result.Add(item);
                }
                CenterLocation = currentModel.Center(result);
            }
        }

        /// <summary>
        /// update the list of Report record and push the graphic "pins" for them in the map
        /// </summary>
        /// <param name="item"></param>
        public void updateReportsAndPushPins(Report item)
        {
            Pushpin pin = new Pushpin();
            Location tempLocation = new Location();
            Reports.Add(item);
            tempLocation.Latitude = item.Latitude;
            tempLocation.Longitude = item.Longitude;
            pin.Location = tempLocation;
            pin.Background = new SolidColorBrush(ToMediaColor(System.Drawing.Color.FromName("Blue")));
            pushpins.Add(pin);
            //pushpins.CollectionChanged += PushpinCollectionChange;

        }

        /// <summary>
        /// send the list of Report to the Model BL for K-means analysis
        /// </summary>
        public void HitByK_Means()
        {
            List<Report> result = new List<Report>();
            foreach (var item in Reports)
            {
                result.Add(item);
            }
            List<GeoCoordinate> byKMeans = currentModel.HitByK_Means(result, Convert.ToInt32(K_Means));
            foreach (var item in byKMeans)
            {
                updateKMeansCollectionAndPushPins(item);
            }
        }

        /// <summary>
        /// update the result of the K-means analysis and push the graphical "pins" on the map
        /// </summary>
        /// <param name="item"></param>
        public void updateKMeansCollectionAndPushPins(GeoCoordinate item)
        {
            Pushpin pin = new Pushpin();
            Location tempLocation = new Location();
            K_means_collection.Add(item);
            tempLocation.Latitude = item.Latitude;
            tempLocation.Longitude = item.Longitude;
            pin.Location = tempLocation;
            pin.Background = new SolidColorBrush(ToMediaColor(System.Drawing.Color.FromName("Green")));
            pushpins.Add(pin);
        }

        /// <summary>
        /// recieve all hits within the last 10 minutes
        /// </summary>
        public async void allCurrentHits()
        {
            Hits.Clear();
            if (end != null)
            {
                var allhits = await currentModel.getHitsByDates(start, end);
                foreach (var item in allhits)
                {
                    updateHitsAndPushPins(item);
                }
            }
            else
            {
                DateTime tempend = start;
                tempend.AddMinutes(10 * Convert.ToInt32(eventNum));
                var allhits = await currentModel.getHitsByDates(start, end);
                foreach (var item in allhits)
                {
                    updateHitsAndPushPins(item);
                }
            }
        }

        /// <summary>
        /// update the list of Hit records and push the graphical "pins" on the map
        /// </summary>
        /// <param name="item"></param>
        public void updateHitsAndPushPins(Hit item)
        {
            Pushpin pin = new Pushpin();
            Location tempLocation = new Location();
            Hits.Add(item);
            tempLocation.Latitude = item.Latitude;
            tempLocation.Longitude = item.Longitude;
            pin.Location = tempLocation;
            pin.Background = new SolidColorBrush(ToMediaColor(System.Drawing.Color.FromName("Red")));
            pushpins.Add(pin);
        }

        /// <summary>
        /// unify all colors of the map pins
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public MColor ToMediaColor(DColor color)
        {
            return MColor.FromArgb(color.A, color.R, color.G, color.B);
        }

        private Location _centerlocation;
        public Location CenterLocation
        {
            set
            {
                _centerlocation = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("CenterLocation"));
            }
            get { return _centerlocation; }

        }
    }
}
