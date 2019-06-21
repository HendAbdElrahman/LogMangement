using IBusiness;

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
