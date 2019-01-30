using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TravelHelper.Controller.Location;
using TravelHelper.Model.Math;
using TravelHelper.Model.SerializeObjects.IrishRail;

namespace TravelHelper.Controller
{
    public static class IrishRailController
    {
        public static async Task<string> GetClosestTrain(string destination)
        {
            using (HttpClient client = new HttpClient())
            {
                string stringRes = await client.GetStringAsync("http://api.irishrail.ie/realtime/realtime.asmx/getCurrentTrainsXML");

                using (TextReader textReader = new StringReader(stringRes))
                {
                    try
                    {
                        XmlSerializer serial = new XmlSerializer(typeof(IrishRailTrainPositions));

                        IrishRailTrainPositions stationInfo = serial.Deserialize(textReader) as IrishRailTrainPositions;

                        Vector2[] stationLocations = new Vector2[stationInfo.TrainPositions.Length];

                        for (int i = 0; i < stationInfo.TrainPositions.Length; i++)
                        {
                            stationLocations[i] = new Vector2()
                            {
                                X = float.Parse(stationInfo.TrainPositions[i].Longitude),
                                Y = float.Parse(stationInfo.TrainPositions[i].Latitude)
                            };
                        }

                        Vector2 usersLocation = await LocationFinder.GetLocation();

                        List<TrainCodeLocation> trainNames = new List<TrainCodeLocation>();

                        //The distance between each degree of longitude/ latitude
                        float coordinationalOffset = 111.0f;

                        for (int i = 0; i < stationLocations.Length; i++)
                        {
                            TrainCodeLocation loc = new TrainCodeLocation()
                            {
                                Distance = usersLocation.Distance(stationLocations[i]) * coordinationalOffset,
                                TrainCode = stationInfo.TrainPositions[i].TrainCode
                            };

                            trainNames.Add(loc);
                        }

                        trainNames = trainNames.OrderBy(t => t.Distance).ToList();

                        for (int i = 0; i < trainNames.Count; i++)
                        {
                            string stationName = await TrainGoesTo(trainNames[i].TrainCode, destination);

                            if (!string.IsNullOrEmpty(stationName))
                                return "Get the: " + trainNames[i].TrainCode + " it's " +
                                    trainNames[i].Distance + "km away from you. It's currently at " + stationName; 
                        }

                        return "Unable to find a train running to " + destination;
                    }
                    catch (Exception e)
                    {
                        return e.Message;
                    }
                }
            }
        }

        static async Task<string> TrainGoesTo(string trainCode, string destination)
        {
            using (HttpClient client = new HttpClient())
            {
                string stringRes = await client.GetStringAsync(
                    "http://api.irishrail.ie/realtime/realtime.asmx/getTrainMovementsXML?TrainId=" +
                    trainCode + "&TrainDate=" + DateTime.Now.ToShortDateString());

                using (TextReader textReader = new StringReader(stringRes))
                {
                    XmlSerializer serial = new XmlSerializer(typeof(IrishRailTrainMovements));

                    IrishRailTrainMovements stationInfo = serial.Deserialize(textReader) as IrishRailTrainMovements;

                    string currentTimeString = DateTime.Now.TimeOfDay.ToString();
                    currentTimeString = currentTimeString.Replace(":", string.Empty);
                    currentTimeString = currentTimeString.Split(new char[] { '.' })[0];

                    int currentTime = int.Parse(currentTimeString);

                    string arrivalTime = string.Empty;

                    //Getting the train line that is yet to stop at our destination
                    for (int i = 0; i < stationInfo.TrainMovements.Length; i++)
                    {
                        if (stationInfo.TrainMovements[i].LocationFullName.StartsWith(destination))
                        {
                            int time = int.Parse(stationInfo.TrainMovements[i].ExpectedArrival.Replace(":", string.Empty));

                            if (currentTime < time)
                            {
                                arrivalTime = ", and is due to arrive in " +
                                    stationInfo.TrainMovements[i].LocationFullName +
                                    " at: " + stationInfo.TrainMovements[i].ExpectedArrival;

                                break;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(arrivalTime))
                    {
                        //Finding out where the train is currently at
                        for (int i = 0; i < stationInfo.TrainMovements.Length; i++)
                        {
                            int time = int.Parse(stationInfo.TrainMovements[i].ExpectedArrival.Replace(":", string.Empty));

                            if (time > currentTime)
                                return stationInfo.TrainMovements[i].LocationFullName + arrivalTime;
                        }
                    }
                }
            }

            return string.Empty;
        }
    }
}