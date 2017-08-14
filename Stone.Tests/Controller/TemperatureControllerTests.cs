using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Stone.Application.Interfaces;
using Stone.Controllers;
using Ploeh.AutoFixture;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Stone.Tests.Controller
{
    [TestClass]
    public class TemperatureControllerTests
    {
        private TemperaturesController _controller;
        private Mock<ICityApplicationService> _service;
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _service = new Mock<ICityApplicationService>();
            _controller = new TemperaturesController(_service.Object);
            _fixture = new Fixture();
        }

        //[TestMethod]
        //public async Task ShouldBePossibleGet()
        //{
        //    var result = await _controller.Get();
        //    _service.Verify(s => s.GetAll(It.IsAny<int>()));

        //    Assert.IsTrue(result is OkObjectResult);
        //}
    }
}
