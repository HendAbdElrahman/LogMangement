using IBusiness;
using IDataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using ViewModels;

namespace Business.Tests
{

    [TestClass]
    public class DBLoggerTest
    {
        private ILogger<Modules> dbLogger;

        Mock<IRepository<DomainModels.Logger>> repository;
        Mock<IParser<Modules>> parser;
        Mock<IParserFactory<Modules>> parserFactory;

        string xmlData = "<?xml version='1.0' encoding='UTF-8'?><Modules xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'><channel><resources> <resource name='x' refresh_interval='180'>text1</resource><resource name='y' refresh_interval='181'>text2</resource><resource name='z' refresh_interval='182'>text3</resource></resources></channel></Modules>";
        private Modules module;

        DomainModels.Logger log = new DomainModels.Logger()
        {
            Id = 1,
            LogLevel = "Warnning",
            Message = "<Modules xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'><channel><resources> <resource name='x' refresh_interval='180'>text1</resource><resource name='y' refresh_interval='181'>text2</resource><resource name='z' refresh_interval='182'>text3</resource></resources></channel></Modules>"
        };

        [TestInitialize]
        public void TestInitialize()
        {
            module = new Modules()
            {
                channel = new Channel()
                {
                    resources = new System.Collections.Generic.List<Resources>() { new Resources()
                    {
                        name = "x",
                        refresh_interval = "180",
                        someText = "text1"
                    },
                    new Resources()
                    {
                        name = "y",
                        refresh_interval = "181",
                        someText = "text2"
                     },
                    new Resources()
                    {
                        name = "z",
                        refresh_interval = "182",
                        someText = "text3"
                    }
                    }
                }
            };

            repository = new Mock<IRepository<DomainModels.Logger>>();

            repository.Setup(o => o.Add(log)).Returns(log);

            parser = new Mock<IParser<Modules>>();

            parser.Setup((IParser<Modules> x) => x.Parse(xmlData)).Returns(module);

            parserFactory = new Mock<IParserFactory<Modules>>();
        }
        
        [TestMethod]
        public async Task AddWarningLog_PassValidXMLDataAsync_WorkCorrectly()
        {
            //arrange
            parserFactory.Setup(O => O.Build(xmlData)).Returns(new XMLParser<Modules>());

            dbLogger = new DBLogger<Modules>(parser.Object, parserFactory.Object, repository.Object);

            //act
            await dbLogger.AddWarningLogAsync(xmlData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Build_PassNullXMLDataAsync_ThrowArgumentNullException()
        {
            //arrange
            xmlData = null;

            parserFactory.Setup(O => O.Build(xmlData)).Returns(new XMLParser<Modules>());

            dbLogger = new DBLogger<Modules>(parser.Object, parserFactory.Object, repository.Object);

            //act
            await dbLogger.AddWarningLogAsync(xmlData);
        }

    }
}
