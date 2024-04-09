using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASPcoreWebAPICRUD.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASPcoreWebAPICRUD.Models;
using Moq;
namespace ASPcoreWebAPICRUD.Controllers.Tests
{
    [TestClass()]
    public class StudentAPIControllerTests
    {
        private StudentAPIController controller;
        private Mock<CodeSecondDbContext> mockStudentService;
        [TestMethod()]
        public void GetstudentsTest()
        {
            controller = new StudentAPIController(mockStudentService.Object);
            //mockStudentService.Setup(x=> x.Getstudents()).Returns(new List<Student>());
            //Assert.Fail();
        }
    }
}