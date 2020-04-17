using GoogleMapsApi;
using GoogleMapsApi.Entities.Common;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Geocoding.Response;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Device.Location;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PL.Converters
{
    /// <summary>
    /// This class store the logic for converting the string of an address to its global coordinates, using Google Maps
    /// </summary>
    class StringToGeoCoordinate
    {
        private static string googleMapsKey = ConfigurationSettings.AppSettings.Get("GoogleMapsKey");
        string address;
        bool flag;
        public GeoCoordinate CurrentLocation { set; get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="address"> the address form the View </param>
        public StringToGeoCoordinate(string address)
        {
            this.address = address;
        }


        /*public static GeoCoordinate GetCoordinates(string region)
        {
            using (var client = new WebClient())
            {
                string uri = "https://maps.googleapis.com/maps/api/geocode/json?address=" + region + "&key=" + googleMapsKey;

                string geocodeInfo = client.DownloadString(uri);
                JavaScriptSerializer oJS = new JavaScriptSerializer();
                GoogleGeoCodeResponse latlongdata = oJS.Deserialize<GoogleGeoCodeResponse>(geocodeInfo);
                //Console.WriteLine(double.Parse(latlongdata.results[0].geometry.location.lat.Replace('.', ',')));
                return new GeoCoordinate(Convert.ToDouble(latlongdata.results[0].geometry.location.lat.Replace('.', ',')), Convert.ToDouble(latlongdata.results[0].geometry.location.lng.Replace('.', ',')));
            }
        }*/

        /// <summary>
        /// engine for conversion
        /// </summary>
        /// <returns></returns>
        public GeoCoordinate startConvert()
        {
            CurrentLocation = new GeoCoordinate();
            flag = false;
            stringToGeoCoordinate(address);
            
            //while (!flag) ;
            return CurrentLocation;
        }

        //private async void stringToGeoCoordinate(string address)
        /// <summary>
        /// the converting method
        /// </summary>
        /// <param name="address">address to convert</param>
        private async void stringToGeoCoordinate(string address)
        {
            try
            {
                GeocodingRequest geocodeRequest = new GeocodingRequest();
                geocodeRequest.Address = address;
                geocodeRequest.ApiKey = googleMapsKey;
                GeocodingResponse geocode = await GoogleMaps.Geocode.QueryAsync(geocodeRequest);
                //GeocodingResponse geocode = GoogleMaps.Geocode.Query(geocodeRequest);
                if (geocode.Status == Status.OK)
                {
                    IEnumerator<Result> iter = geocode.Results.GetEnumerator();
                    iter.MoveNext();
                    Location tempLocation = iter.Current.Geometry.Location;
                    CurrentLocation.Latitude = tempLocation.Latitude;
                    CurrentLocation.Longitude = tempLocation.Longitude;
                    flag = true;
                }
            }
            catch (Exception) { }
        }

    }
}
