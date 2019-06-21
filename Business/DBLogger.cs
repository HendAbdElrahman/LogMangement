using Business.Helpers;
using IBusiness;
using IDataAccess;
using System;
using System.Threading.Tasks;
using ViewModels;

namespace Business
{
    public class DBLogger<T> : ILogger<T>
    {
        private IRepository<DomainModels.Logger> repoLogger;
        private IParser<T> parser;
        private IParserFactory<T> parserFactory;

        public DBLogger(IParser<T> parser, IParserFactory<T> parserFactory, IRepository<DomainModels.Logger> repoLogger)
        {
            this.parser = parser;
            this.parserFactory = parserFactory;
            this.repoLogger = repoLogger;

        }

        public void AddWarningLogAsync(string data)
        {
            AddLog(data, LogLevel.Warning.ToString());
        }

        public void AddInfoLogAsync(string data)
        {
            AddLog(data, LogLevel.Info.ToString());
        }

        public void AddFatelLogAsync(string data)
        {
            AddLog(data, LogLevel.Fatel.ToString());
        }

        private void AddLog(string data, string logType)
        {
            if (data == null)
                throw new ArgumentNullException();

            IParser<T> parser = this.parserFactory.Build(data);

            var parsedData = parser.Parse(data);

            var prop = new Helper().ConvertTModelPropertyAndValueToString<T>(parsedData);

            saveToDB(prop, logType);
        }


        private async Task AddLogAsync(string data, string logType)
        {
            if (data == null)
                throw new ArgumentNullException();

            IParser<T> parser = parserFactory.Build(data);

            var parsedData = parser.Parse(data);

            var prop = new Helper().ConvertTModelPropertyAndValueToString<T>(parsedData);

            await saveToDBAsync(prop, logType);
        }
        private void saveToDB(object data, string logType)
        {
            repoLogger.Add(new DomainModels.Logger()
            {
                LogLevel = logType,
                LogTime = DateTime.Now,
                Message = data.ToString()
            });

            repoLogger.SaveChanges();
        }

        private async Task saveToDBAsync(object data, string logType)
        {
            repoLogger.Add(new DomainModels.Logger()
            {
                LogLevel = logType,
                LogTime = DateTime.Now,
                Message = data.ToString()
            });

            await repoLogger.SaveChangesAsync();
        }

        async Task ILogger<T>.AddWarningLogAsync(string data)
        {
            await AddLogAsync(data, LogLevel.Warning.ToString());
        }

        async Task ILogger<T>.AddInfoLogAsync(string data)
        {
            await AddLogAsync(data, LogLevel.Info.ToString());
        }

        async Task ILogger<T>.AddFatelLogAsync(string data)
        {
            await AddLogAsync(data, LogLevel.Fatel.ToString());
        }
    }

}
