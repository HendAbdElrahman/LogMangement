using Business.Helpers;
using IBusiness;
using System;
using System.Configuration;
using System.IO;
using ViewModels;

namespace Business
{
    public class FileLogger<T> : ILogger<T>
    {

        private IParser<T> parser;
        private IParserFactory<T> parserFactory;

        public FileLogger(IParser<T> parser, IParserFactory<T> parserFactory)
        {
            this.parser = parser;
            this.parserFactory = parserFactory;
        }

        public void AddWarningLog(string data)
        {
            AddLog(data, LogLevel.Warning.ToString());
        }

        public void AddInfoLog(string data)
        {
            AddLog(data, LogLevel.Info.ToString());
        }

        public void AddFatelLog(string data)
        {
            AddLog(data, LogLevel.Fatel.ToString());
        }


        private void AddLog(string data, string logType)
        {
            if (data == null)
                throw new ArgumentNullException();

            IParser<T> parser = this.parserFactory.Build(data);

            var parsedData = parser.Parse(data);

            var prop = new Helper().PrintTModelPropertyAndValue<T>(parsedData);

            saveToFile(prop, logType);
        }


        private void saveToFile(object data, string logType)
        {
            var filePath = ConfigurationManager.AppSettings["RollingFile"];
            using (StreamWriter sw1 = File.AppendText(filePath))
            {
                var str = $" {DateTime.Now.ToString()} - {logType} -  {data}";
                sw1.WriteLine(str);
                sw1.Close();
            }
        }

        //private string PrintTModelPropertyAndValue<T>(object tmodelObj)
        //{
        //    string str = "";
        //    foreach (var prop in typeof(T).GetFields())
        //    {
        //        str += prop.Name.ToString();
        //        str += " :  ";
        //        str += prop.GetValue(tmodelObj);
        //        str += ",   ";
        //    }
        //    return str;
        //}

    }
}
