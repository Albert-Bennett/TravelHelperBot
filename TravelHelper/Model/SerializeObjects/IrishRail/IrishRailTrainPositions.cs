using System.Xml.Serialization;

namespace TravelHelper.Model.SerializeObjects.IrishRail
{
    [XmlRoot(ElementName = "ArrayOfObjTrainPositions", Namespace = "http://api.irishrail.ie/realtime/")]
    public class IrishRailTrainPositions
    {
        [XmlElement(ElementName = "objTrainPositions")]
        public IrishRailTrainPosition[] TrainPositions { get; set; }
    }

    public class IrishRailTrainPosition
    {
        [XmlElement(ElementName = "TrainLatitude")]
        public string Latitude { get; set; }

        [XmlElement(ElementName = "TrainLongitude")]
        public  string Longitude { get; set; }

        [XmlElement(ElementName = "TrainCode")]
        public string TrainCode { get; set; }
    }
}
