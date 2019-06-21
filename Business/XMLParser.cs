﻿using IBusiness;
using System.IO;
using System.Xml.Serialization;

namespace Business
{
    public class XMLParser<T> : IParser<T>
    {

        public T Parse(string data)
        {
            var result = DeserializeObject(data);
            return result;
        }
        private static T DeserializeObject(string xml)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var tr = new StringReader(xml))
            {
                return (T)serializer.Deserialize(tr);
            }
        }
    }
}
