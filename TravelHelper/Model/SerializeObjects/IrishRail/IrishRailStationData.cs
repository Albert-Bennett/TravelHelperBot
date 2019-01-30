using System.Xml.Serialization;

namespace TravelHelper.Model.SerializeObjects.IrishRail
{
    [XmlRoot(ElementName = "ArrayOfObjStation", Namespace = "http://api.irishrail.ie/realtime/")]
    public class IrishRailStationData
    {
        [XmlElement(ElementName = "objStation")]
        public IrishRailStationLocationInfo[] StationInfo { get; set; }
    }

    public class IrishRailStationLocationInfo
    {
        [XmlElement(ElementName = "StationDesc")]
        public string StationName { get; set; }

        [XmlElement(ElementName = "StationLatitude")]
        public string Latitude { get; set; }

        [XmlElement(ElementName = "StationLongitude")]
        public string Longitude { get; set; }

        [XmlElement(ElementName = "StationCode")]
        public string StationCode { get; set; }

        [XmlElement(ElementName = "StationId")]
        public string StationId { get; set; }
    }
}
