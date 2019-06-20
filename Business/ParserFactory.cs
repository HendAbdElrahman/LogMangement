using IBusiness;
using System;
using ViewModels;

namespace Business
{
    public class ParserFactory<T> : IParserFactory<T>
    {
        public ParserTypes StringFormatType(string input)
        {
            input = input.Trim();

            if (input.StartsWith("{") && input.EndsWith("}")
                   || input.StartsWith("[") && input.EndsWith("]"))
                return ParserTypes.Json;

            else if (!string.IsNullOrEmpty(input) && input.TrimStart().StartsWith("<"))
            {
                return ParserTypes.XML;
            }

            else
                return ParserTypes.unKnown;

        }


        public IParser<T> Build(string data)
        {
            var stringType = StringFormatType(data);

            if (stringType == ParserTypes.Json)
                return new JSONParser<T>();

            else if (stringType == ParserTypes.XML)
                return new XMLParser<T>();

            else
            {
                throw new FormatException("Unsuported data format.");
            }
        }
    }
}
