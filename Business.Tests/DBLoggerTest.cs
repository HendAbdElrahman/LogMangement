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
    public class DBLoggerTest
    {
        private ILogger<Company> dbLogger;

        Mock<IRepository<DomainModels.Logger>> repository;
        Mock<IParser<Company>> parser;
        Mock<IParserFactory<Company>> parserFactory;

        string xmlData = "<?xml version='1.0' encoding='utf-8' ?><Company Id='2' Name='Friska'></Company>";
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
            repository = new Mock<IRepository<DomainModels.Logger>>();
            repository.Setup(o => o.Add(log)).Returns(log);
            parser = new Mock<IParser<Company>>();
            parser.Setup(x => x.Parse(xmlData)).Returns(company);
            parserFactory = new Mock<IParserFactory<Company>>();
        }


        [TestMethod]
        public void AddWarningLog_PassValidXMLSources_WorkCorrectly()
        {
            //arrange
            parserFactory.Setup(O => O.Build(xmlData)).Returns(new XMLParser<Company>());
            dbLogger = new DBLogger<Company>(parser.Object, parserFactory.Object, repository.Object);

            //act
            dbLogger.AddWarningLog(xmlData);
        }
        //[TestMethod]
        //[ExpectedException(typeof(InvalidOperationException))]
        //public void TestThroEx()
        //{
        //    Mock<IUserEntity> user = new Mock<IUserEntity>();
        //    Mock<IUserRepository<int>> repo = new Mock<IUserRepository<int>>();
        //    UserManagement userManagement = new UserManagement(repo.Object);
        //    repo.Setup(x => x.GetById(It.IsAny<int>())).Throws(new InvalidOperationException());
        //    var res = userManagement.GetById(-1);

        //}
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestThroEx()
        {
            /* //arrange
             xmlData = null;
             //act
             parserFactory.Setup(x => x.Build(xmlData)).Throws(new ArgumentNullException());
             */

            //arrange
            xmlData = null;
            parserFactory.Setup(O => O.Build(xmlData)).Returns(new XMLParser<Company>());
            dbLogger = new DBLogger<Company>(parser.Object, parserFactory.Object, repository.Object);

            //act
            dbLogger.AddWarningLog(xmlData);
        }
        [TestCleanup]
        public void TestCleanUp()
        {
           // repository.re(downloadedFile);
           // repository.Dispose();
        }
    }
}
