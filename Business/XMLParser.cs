using IBusiness;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
