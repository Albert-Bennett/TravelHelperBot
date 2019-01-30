using System.Xml.Serialization;

namespace TravelHelper.Model.SerializeObjects.IrishRail
{
    [XmlRoot(ElementName = "ArrayOfObjTrainMovements", Namespace = "http://api.irishrail.ie/realtime/")]
    public class IrishRailTrainMovements
    {
        [XmlElement(ElementName = "objTrainMovements")]
        public IrishRailTrainMovement[] TrainMovements { get; set; }
    }

    public class IrishRailTrainMovement
    {
        [XmlElement(ElementName = "TrainCode")]
        public string TrainCode { get; set; }

        [XmlElement(ElementName = "TrainDate")]
        public string TrainDate { get; set; }

        [XmlElement(ElementName = "LocationFullName")]
        public string LocationFullName { get; set; }

        [XmlElement(ElementName = "TrainOrigin")]
        public string TrainOrigin { get; set; }

        [XmlElement(ElementName = "TrainDestination")]
        public string TrainDestination { get; set; }

        [XmlElement(ElementName = "ScheduledArrival")]
        public string ScheduledArrival { get; set; }

        [XmlElement(ElementName = "ScheduledDeparture")]
        public string ScheduledDeparture { get; set; }

        [XmlElement(ElementName = "ExpectedArrival")]
        public string ExpectedArrival { get; set; }

        [XmlElement(ElementName = "ExpectedDeparture")]
        public string ExpectedDeparture { get; set; }

        [XmlElement(ElementName = "Arrival")]
        public string Arrival { get; set; }
    }
}
