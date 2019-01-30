using System.Xml.Serialization;

namespace TravelHelper.Model.SerializeObjects.IrishRail
{
    [XmlRoot(ElementName = "ArrayOfObjStationData", Namespace = "http://api.irishrail.ie/realtime/")]
    public class IrishRailStationInfo
    {
        [XmlElement(ElementName = "objStationData")]
        public IrishRailTrainData[] StationData { get; set; }
    }

    public class IrishRailTrainData
    {
        [XmlElement(ElementName = "Traincode")]
        public string TrainCode { get; set; }

        [XmlElement(ElementName = "Stationfullname")]
        public string StationFullname { get; set; }

        [XmlElement(ElementName = "Stationcode")]
        public string StationCode { get; set; }

        [XmlElement(ElementName = "Traindate")]
        public string Traindate { get; set; }

        [XmlElement(ElementName = "Origin")]
        public string Origin { get; set; }

        [XmlElement(ElementName = "Origintime")]
        public string OriginTime { get; set; }

        [XmlElement(ElementName = "Destination")]
        public string Destination { get; set; }

        [XmlElement(ElementName = "Lastlocation")]
        public string LastLocation { get; set; }

        [XmlElement(ElementName = "Locationtype")]
        public string LocationType { get; set; }
    }
}