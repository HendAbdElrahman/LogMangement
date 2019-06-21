﻿using System;
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

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Build_PassNullXMLData_ThrowArgumentNullException()
        {
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
            //repository.Dispose();
        }
    }
}
