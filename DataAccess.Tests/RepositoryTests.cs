using System;
using IDataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccess.Tests
{
    [TestClass]
    public class RepositoryTests
    {
        private Logger logger;
        IRepository<Logger> repo;

        [TestInitialize]
        public void TestInitialize()
        {
            repo = new Repository<Logger>(new UnitOfWork());

            logger = new Logger
            {
                LogLevel = "Warnning",
                LogTime = DateTime.Now,
                Message = "Warnning Message"
            };

        }

        [TestCleanup]
        public void TestCleanUp()
        {
            repo.Delete(logger);

            repo.Dispose();
        }


        [TestMethod]
        public void Add_ValidEntity_SaveSucess()
        {
            //act
            repo.Add(logger);

            repo.SaveChanges();

            //assert
            Assert.IsTrue(logger.Id > 0);
        }

        [TestMethod]
        public void Find_SearchForEntity_ReturnedSucess()
        {
            //arrange
            repo.Add(logger);

            repo.SaveChanges();

            //act
            var result = repo.Find(logger.Id);

            //assert
            Assert.AreEqual(logger.Id, result.Id);

        }

        [TestMethod]
        public void Update_UpdateProcessingStatus_UpdateSucess()
        {
            //arrange
            repo.Add(logger);

            repo.SaveChanges();

            //act
            var logLevel = "Info";

            var TempFile = repo.Find(logger.Id);

            TempFile.LogLevel = logLevel;

            repo.Update(TempFile);

            //assert
            Assert.AreEqual(logger.LogLevel, logLevel);

        }


    }
}
