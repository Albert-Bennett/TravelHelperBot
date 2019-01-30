using Newtonsoft.Json;

namespace TravelHelper.Controller.Location
{
    public class GeoLocationData
    {
        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }
    }
}
