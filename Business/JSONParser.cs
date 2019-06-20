using IBusiness;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class JSONParser<T> : IParser<T>
    {
        private readonly string parserType = "JSON";
        public bool IsMatch(string parserType)
        {
            return parserType.ToUpper() == this.parserType;
        }

        public T Parse(string data)
        {

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);

            return result;
        }
    }
}
