using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Device.Location;
using System.ComponentModel.DataAnnotations;

namespace BE
{
    /// <summary>
    /// represent a detailed, confiremd Hit
    /// </summary>
    public class Hit
    {
        [Key]
        public int EventID { get; set; }
        public DateTime timeOfHit { set; get; }
        public double Latitude { get; set; }// Location
        public double Longitude { get; set; }
        public string locationImage { set; get; }

        /// <summary>
        /// Get the coordinate the the local tuple of (Latitude, Longitude).
        /// </summary>
        /// <returns></returns>
        public GeoCoordinate GetCoordinate()
        {
            return new GeoCoordinate(Latitude, Longitude);
        }
    }
}
