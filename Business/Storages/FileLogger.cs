using Business.Helpers;
using IBusiness;
using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;
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

        public async Task AddWarningLogAsync(string data)
        {
            await AddLogAsync(data, LogLevel.Warning.ToString());
        }

        public async Task AddInfoLogAsync(string data)
        {
            await AddLogAsync(data, LogLevel.Info.ToString());
        }

        public async Task AddFatelLogAsync(string data)
        {
            await AddLogAsync(data, LogLevel.Fatel.ToString());
        }

        private async Task AddLogAsync(string data, string logType)
        {

            if (data == null)
                throw new ArgumentException();

            IParser<T> parser = parserFactory.Build(data);

            var parsedData = parser.Parse(data);

            var prop = new Helper().ConvertTModelPropertyAndValueToString<T>(parsedData);

            await SaveToFileAsync(prop, logType); 
    
        }

        private async Task SaveToFileAsync(object data, string logType)
        {
            var filePath = ConfigurationManager.AppSettings["RollingFile"];

            var str = $" {DateTime.Now.ToString()} - {logType} -  {data} {Environment.NewLine}";

            byte[] encodedText = Encoding.Default.GetBytes(str);

            using (FileStream sourceStream = new FileStream(filePath,
                FileMode.Append, FileAccess.Write, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            };
        }
        

    }
}
