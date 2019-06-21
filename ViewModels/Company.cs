using System;
using System.Xml.Serialization;

namespace ViewModels
{
    [Serializable, XmlRoot("Company")]
    public class Company
    {
        [XmlAttribute(AttributeName = "Id")]
        public int Id;
        [XmlAttribute(AttributeName = "Name")]
        public string Name;
    }
}
