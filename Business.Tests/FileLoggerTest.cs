using System;
using DataAccess;
using IBusiness;
using IDataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModels;

namespace Business.Tests
{

    [TestClass]
    public class FileLoggerTest
    {
        private ILogger<Company> fileLogger;

        Mock<IParser<Company>> parser;
        Mock<IParserFactory<Company>> parserFactory;

        string jsonData = @"{""Name"":""Rick"",""Id"":""1""}";
        private string data = "Data";
        private Company company;

        DomainModels.Logger log = new DomainModels.Logger()
        {
            Id = 0,
            LogLevel = "Warnning",
            Message = "Message"

        };

        [TestInitialize]
        public void TestInitialize()
        {
            company = new Company()
            {
                Id = 3,
                Name = "Friska4433"
            };
            
            parser = new Mock<IParser<Company>>();
            parser.Setup(x => x.Parse(jsonData)).Returns(company);
            parserFactory = new Mock<IParserFactory<Company>>();
        }


        [TestMethod]
        public void AddWarningLog_PassValidXMLSources_WorkCorrectly()
        {
            //arrange
            parserFactory.Setup(O => O.Build(jsonData)).Returns(new JSONParser<Company>());

            fileLogger = new FileLogger<Company>(parser.Object, parserFactory.Object);

            //act
            fileLogger.AddWarningLog(jsonData);
        }

        [TestCleanup]
        public void TestCleanUp()
        {
           // repository.re(downloadedFile);
           // repository.Dispose();
        }
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
