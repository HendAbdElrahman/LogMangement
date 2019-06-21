using System;
using System.Xml.Serialization;

namespace ViewModels
{
    [Serializable]
    [XmlRoot("response")]
    public class IdentifyData
    {
        [XmlElement("modules")]
        public Modules modules { get; set; }
    }
}
