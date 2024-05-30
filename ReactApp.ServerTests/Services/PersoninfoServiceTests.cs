using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReactApp.Server.Models;
using ReactApp.Server.Models.Request;
using ReactApp.Server.Services;
using System.Collections.Generic;
using System.Linq;

namespace ReactApp.Server.Services.Tests
{
    [TestClass]
    [TestCategory("UnitTest")]
    public class PersoninfoServiceTests
    {
        private PersoninfoService? _personinfoService;
        private Mock<ExamContext>? _mockContext;

        [TestInitialize]
        public void Init()
        {
            _mockContext = new Mock<ExamContext>();
            // 模擬 Personinfo 的 DbSet
            var mockPersoninfoData = new List<Personinfo>()
            {
                new Personinfo
                {
                    No = 1,
                    Name = "Test1",
                    Phone = "0900123456",
                    Note = "test1"
                },
                new Personinfo
                {
                    No = 2,
                    Name = "Test2",
                    Phone = "0912345678",
                    Note = "test2"
                }
            }.AsQueryable();

            var mockPersoninfosDbSet = new Mock<DbSet<Personinfo>>();
            mockPersoninfosDbSet.As<IQueryable<Personinfo>>().Setup(m => m.Provider).Returns(mockPersoninfoData.Provider);
            mockPersoninfosDbSet.As<IQueryable<Personinfo>>().Setup(m => m.Expression).Returns(mockPersoninfoData.Expression);
            mockPersoninfosDbSet.As<IQueryable<Personinfo>>().Setup(m => m.ElementType).Returns(mockPersoninfoData.ElementType);
            mockPersoninfosDbSet.As<IQueryable<Personinfo>>().Setup(m => m.GetEnumerator()).Returns(mockPersoninfoData.GetEnumerator());

            // 設置 MockContext 的 Personinfos 屬性
            _mockContext.Setup(c => c.Personinfos).Returns(mockPersoninfosDbSet.Object);

            _personinfoService = new PersoninfoService(_mockContext.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _personinfoService = null;
        }

        [TestMethod]
        public void GetPersoninfo_Success()
        {
            // Fake Request
            var query = new GetPersoninfoQueryModel { No = 1 };

            var result = _personinfoService.GetPersoninfo(query);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Test1", result[0].Name);
        }

        [TestMethod]
        public void AddNewPersoninfo_Success()
        {
            // Fake Request
            var query = new AddNewPersoninfoQueryModel()
            {
                Name = "Test3",
                Phone = "0922333444",
                Note = "test3"
            };

            var result = _personinfoService.AddNewPersoninfo(query);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Cmd);
            Assert.AreEqual("Success", result.Message);
        }

        [TestMethod]
        public void DeletePersoninfo_Success()
        {
            // Fake Request
            var query = new DeletePersoninfoQueryModel()
            {
                No = 1,
                Name = "Test1"
            };

            var result = _personinfoService.DeletePersoninfo(query);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Cmd);
            Assert.AreEqual("Success", result.Message);
        }

        [TestMethod]
        public void UpdatePersoninfo_Success()
        {
            // Fake Request
            var query = new UpdatePersoninfoQueryModel()
            {
                No = 1,
                Name = "Test1",
                Phone = "0911222333",
                Note = "updated note"
            };

            var result = _personinfoService.UpdatePersoninfo(query);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Cmd);
            Assert.AreEqual("Success", result.Message);
        }
    }
}
