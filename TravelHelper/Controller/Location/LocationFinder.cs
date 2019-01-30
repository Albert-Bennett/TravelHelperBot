using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using TravelHelper.Model.Math;

namespace TravelHelper.Controller.Location
{
    public class LocationFinder
    {
        public static async Task<Vector2> GetLocation()
        {
            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync("https://api.ipdata.co?api-key=077713f1d5948fe9adba47b75b17683abc8653bd547bc11fdf86e00e");

                if (!string.IsNullOrEmpty(response))
                {
                    GeoLocationData locationData = JsonConvert.DeserializeObject<GeoLocationData>(response);

                    return new Vector2()
                    {
                        X = float.Parse(locationData.Longitude),
                        Y = float.Parse(locationData.Latitude)
                    };
                }
            }

            return Vector2.Zero;
        }
    }
}
