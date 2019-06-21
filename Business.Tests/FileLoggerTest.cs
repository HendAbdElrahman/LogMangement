using System;
using System.Threading.Tasks;
using IBusiness;
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

        string jsonData = @"{""Name"":""Integrant Inc"",""Id"":""1""}";
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
                Name = "Integrant Inc"
            };

            parser = new Mock<IParser<Company>>();

            parser.Setup(x => x.Parse(jsonData)).Returns(company);

            parserFactory = new Mock<IParserFactory<Company>>();
        }


        #region SyncMethods

        [TestMethod]
        public void AddWarningLog_PassValidXMLSources_WorkCorrectly()
        {
            //arrange
            parserFactory.Setup(O => O.Build(jsonData)).Returns(new JSONParser<Company>());

            fileLogger = new FileLogger<Company>(parser.Object, parserFactory.Object);

            //act
            fileLogger.AddWarningLogAsync(jsonData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task Build_PassNullXMLData_ThrowArgumentNullExceptionAsync()
        {
            
            //arrange
            jsonData = null;

            parserFactory.Setup(O => O.Build(jsonData)).Returns(new XMLParser<Company>());

            fileLogger = new FileLogger<Company>(parser.Object, parserFactory.Object);

            //act
            await fileLogger.AddWarningLogAsync(jsonData);
        }

        #endregion

    }
}
