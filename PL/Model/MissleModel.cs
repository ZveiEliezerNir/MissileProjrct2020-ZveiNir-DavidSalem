using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Device.Location;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BE;
using BLL;  
using GoogleMapsApi;
using GoogleMapsApi.Entities.Common;
using GoogleMapsApi.Entities.PlaceAutocomplete.Request;

namespace PL.Model
{
    /// <summary>
    /// the Model
    /// </summary>
    public class MissleModel: INotifyPropertyChanged
    {
        private string googleMapsKey = ConfigurationSettings.AppSettings.Get("GoogleMapsKey");
        private List<string> _locations;
        private BLL_imp currentBll;
        public List<string> locations
        {
            get
            {
                return _locations;
            }
            set
            {
                _locations = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public MissleModel()
        {
            currentBll = new BLL_imp();
            locations = new List<string>();
        }

        public Report incomingReport
        {
            get { return incomingReport; }
            set
            {
               // incomingReport = (Report)value;
                currentBll.addReport(value);
            }
        }

        public Hit incomingHit
        {
            get { return incomingHit; }
            set
            {
                //incomingHit = value;
                currentBll.addHit(value);
            }
        }

        public bool checkLocations(string leters)
        {
            bool flag = false;
            AddressLocationGoogleMaps(leters);
            flag = true;
            return flag;
        }

        private async void AddressLocationGoogleMaps(string leters)
        {
            List<string> tempResult = new List<string>();
            if (leters == "")
            {
                return;
            }

            else
            {
                try
                {
                    PlaceAutocompleteRequest request = new PlaceAutocompleteRequest();
                    request.ApiKey = googleMapsKey;
                    request.Input = leters;
                    var response = await GoogleMaps.PlaceAutocomplete.QueryAsync(request);
                    foreach (var item in response.Results)
                    {
                        tempResult.Add(item.Description);
                    }
                    locations.Clear();
                    locations = tempResult;
                }
                catch (Exception) { throw; }
            }
        }

        public async Task<List<Report>> getReportsByDates(DateTime startDate, DateTime endDate)
        {
            if (String.IsNullOrEmpty(startDate.ToString())|| String.IsNullOrEmpty(endDate.ToString())||startDate>endDate)
            {
                return null;
            }
            var reports =await allReports();
            List<Report> result = new List<Report>();
            foreach (var item in reports)
            {
                if (item.timeOfReport>=startDate&&item.timeOfReport<=endDate)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public async Task<List<Hit>> getHitsByDates(DateTime startDate, DateTime endDate)
        {
            if (String.IsNullOrEmpty(startDate.ToString()) || String.IsNullOrEmpty(endDate.ToString()) || startDate > endDate)
            {
                return null;
            }
            var hits = await allHits();
            List<Hit> result = new List<Hit>();
            foreach (var item in hits)
            {
                if (item.timeOfHit >= startDate && item.timeOfHit <= endDate)
                {
                    result.Add(item);
                }
            }
            return result;
        }


        public Microsoft.Maps.MapControl.WPF.Location Center (List<Report> listOfReport)
        {
            double latitude_Min;
            double latitude_Max;
            double longitude_Min;
            double longitude_Max;
            double latitude_Center;
            double longitude_Center;
            Microsoft.Maps.MapControl.WPF.Location center_location = new Microsoft.Maps.MapControl.WPF.Location();

            if (listOfReport.Count == 0)
            {
                return null;
            }
            latitude_Min = listOfReport.Min(r => r.Latitude);
            latitude_Max = listOfReport.Max(r => r.Latitude);
            longitude_Min = listOfReport.Min(r => r.Longitude);
            longitude_Max = listOfReport.Max(r => r.Longitude);
            latitude_Center = (latitude_Min + latitude_Max) / 2;
            longitude_Center = (longitude_Min + longitude_Max) / 2;
            center_location.Latitude = latitude_Center;
            center_location.Longitude = longitude_Center;
            return center_location;
        }

        public List<GeoCoordinate> HitByK_Means(List<Report> check_list, int k)
        {
            return currentBll.k_Means(check_list, k);
        }

        public async Task<List<Hit>> allHits()
        {
            return await currentBll.getAllHits();
        }

        public async Task<List<Report>> allReports()
        {            
            return await currentBll.getAllReports();
        }
    }
}
