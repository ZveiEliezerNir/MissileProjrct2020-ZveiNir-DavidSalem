using BE;
using PL.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Configuration;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Geocoding.Response;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Common;

namespace PL.Commands
{
    /// <summary>
    /// The "submit" button of the New_Hit form is a user-defined command defined in this class
    /// </summary>
    public class AddHitCommand : ICommand
    {
        private string googleMapsKey = ConfigurationSettings.AppSettings.Get("GoogleMapsKey");
        double longitude;
        double latitude;
        private IHitViewModel CurrentVM;


        /// <summary>
        /// add a Hit
        /// </summary>
        /// <param name="CurrentVM"> the ViewModel that calls the Command</param>
        public AddHitCommand(IHitViewModel CurrentVM)
        {
            this.CurrentVM = CurrentVM;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// speify the conditions for enabling the Command to Execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            bool result = true;
            var values = (object[])parameter;
            if (values != null)
            {
                foreach (var item in values)
                {
                    if (item == null)
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Execute the Command: collect the fields of the Model from the multi-converter in the View and set the values in the ViewModel
        /// </summary>
        /// <param name="parameter">parameters that were collected form the View usin</param>
        public async void Execute(object parameter)
        {
            var values = (object[])parameter;
            Hit hit = new Hit();
            hit.locationImage = values[0].ToString();
            hit.timeOfHit = DateTime.Parse(values[1].ToString());
            string address = values[2].ToString();

            try
            {
                GeocodingRequest geocodeRequest = new GeocodingRequest();
                geocodeRequest.Address = address;
                geocodeRequest.ApiKey = googleMapsKey;
                GeocodingResponse geocode = await GoogleMaps.Geocode.QueryAsync(geocodeRequest);
                if (geocode.Status == Status.OK)
                {
                    IEnumerator<Result> iter = geocode.Results.GetEnumerator();
                    iter.MoveNext();
                    Location tempLocation = iter.Current.Geometry.Location;
                    latitude = tempLocation.Latitude;
                    longitude = tempLocation.Longitude;
                }
            }
            catch (Exception exception) { }

            hit.Latitude = latitude;
            hit.Longitude = longitude;
            CurrentVM.incomingHit = hit;
        }
    }
}
