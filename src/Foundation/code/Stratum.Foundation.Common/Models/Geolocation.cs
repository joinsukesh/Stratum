
namespace Stratum.Foundation.Common.Models
{
    using System;

    [Serializable]
    public class Geolocation
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public Geolocation(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public Geolocation(string latitude, string longitude)
        {
            double lat;
            double lng;
            Double.TryParse(latitude, out lat);
            Double.TryParse(longitude, out lng);
            this.Latitude = lat;
            this.Longitude = lng;
        }
    }
}
