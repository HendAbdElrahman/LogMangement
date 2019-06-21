using System;
using System.Xml.Serialization;

namespace ViewModels
{
    [Serializable, XmlRoot("Modules")]
    public class Modules
    {
        [XmlElement]
        public Channel channel { get; set; }

        [XmlElement]
        public Image image { get; set; }
    }
}
