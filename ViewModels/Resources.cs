using System;
using System.Xml.Serialization;

namespace ViewModels
{
    [Serializable]
    public class Resources
    {
        [XmlAttribute]
        public string name { get; set; }

        [XmlAttribute]
        public string url { get; set; }

        [XmlAttribute]
        public string refresh_interval { get; set; }

        [XmlText]
        public string someText { get; set; }
    }
}
