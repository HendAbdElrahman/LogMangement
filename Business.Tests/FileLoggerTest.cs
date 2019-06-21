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
        private ILogger<Modules> fileLogger;
        Mock<IParser<Modules>> parser;
        Mock<IParserFactory<Modules>> parserFactory;

        string jsonData = @"{
   'channel': {
      'resources': [
         {
            'name': 'x',
            'refresh_interval': 180,
            'text': 'text1'
         },
         {
            'name': 'y',
            'refresh_interval': 181,
            'text': 'text2'
         },
         {
            'name': 'z',
            'refresh_interval': 182,
            'text': 'text3'
         }
      ]
   }
}";

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

            parser = new Mock<IParser<Modules>>();

            parser.Setup(x => x.Parse(jsonData)).Returns(module);

            parserFactory = new Mock<IParserFactory<Modules>>();
        }


        #region SyncMethods

        [TestMethod]
        public async Task AddWarningLog_PassValidJsonDataAsync_WorkCorrectly()
        {
            //arrange
            parserFactory.Setup(O => O.Build(jsonData)).Returns(new JSONParser<Modules>());

            fileLogger = new FileLogger<Modules>(parser.Object, parserFactory.Object);

            //act
            await fileLogger.AddWarningLogAsync(jsonData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task Build_PassNullJsonDataAsync_ThrowArgumentNullException()
        {
            
            //arrange
            jsonData = null;

            parserFactory.Setup(O => O.Build(jsonData)).Returns(new XMLParser<Modules>());

            fileLogger = new FileLogger<Modules>(parser.Object, parserFactory.Object);

            //act
            await fileLogger.AddWarningLogAsync(jsonData);
        }

        #endregion

    }
}
