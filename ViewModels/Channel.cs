using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ViewModels
{
    [Serializable]
    public class Channel
    {
        [XmlArray("resources")]
        [XmlArrayItem("resource")]
        public List<Resources> resources { get; set; }
    }
}
