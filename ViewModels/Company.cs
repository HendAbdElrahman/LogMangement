using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        //[XmlAttribute(AttributeName = "Company1")]
        [XmlArray("Company1")]
        [XmlArrayItem("Company1")]
        public List<Company1> Company1 { get; set; }

      //  public Company1 Company1;
    }
    [Serializable]
    public class Company1
    {
        [XmlAttribute(AttributeName = "Id")]
        public int Id;
        [XmlAttribute(AttributeName = "Name")]
        public string Name;
    }
    [Serializable]
    [XmlRoot("response")]
    public class IdentifyData
    {
        [XmlElement("modules")]
        public Modules modules { get; set; }
    }

    [Serializable, XmlRoot("Modules")]
    public class Modules
    {
        [XmlElement]
        public Channel channel { get; set; }
        [XmlElement]
        public Image image { get; set; }
    }
    [Serializable]
    public class Image
    {
        [XmlArray("resources")]
        [XmlArrayItem("resource")]
        public List<Resources> resources { get; set; }

    }

    [Serializable]
    public class Channel
    {
        [XmlArray("resources")]
        [XmlArrayItem("resource")]
        public List<Resources> resources { get; set; }
    }

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
