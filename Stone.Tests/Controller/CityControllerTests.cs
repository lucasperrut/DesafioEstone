using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Stone.Common.Interfaces;
using Stone.Application.Interfaces;
using Stone.Controllers;
using Ploeh.AutoFixture;
using System.Threading.Tasks;
using Stone.Domain.Entities;
using Stone.Models;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Stone.Application.Resources;

namespace Stone.Tests.Controller
{
    [TestClass]
    public class CityControllerTests
    {
        private CitiesController _controller;
        private Mock<ICityApplicationService> _service;
        private Mock<ITemperatureApplicationService> _serviceTemperature;
        private Mock<IHttpAgent> _userAgent;
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _service = new Mock<ICityApplicationService>();
            _serviceTemperature = new Mock<ITemperatureApplicationService>();
            _userAgent = new Mock<IHttpAgent>();
            _controller = new CitiesController(_service.Object, _serviceTemperature.Object, _userAgent.Object);
            _fixture = new Fixture();
        }

        [TestMethod]
        public async Task ShouldBePossibleGet()
        {
            var nameCity = _fixture.Create<string>();

            var result = await _controller.Get(nameCity);
            _serviceTemperature.Verify(s => s.Find(nameCity));

            Assert.IsTrue(result is OkObjectResult);
        }

        [TestMethod]
        public async Task ShouldBePossiblePost()
        {
            var nameCity = _fixture.Create<string>();

            var result = await _controller.Post(nameCity);

            _service.Verify(s => s.Create(nameCity));

            Assert.IsTrue(result is CreatedResult);
        }

        [TestMethod]
        public async Task ShouldBePossiblePostByCep()
        {
            _userAgent.Setup(u => u.GetAsync<ViaCepModel>(It.IsAny<string>())).Returns(Task.FromResult(_fixture.Create<ViaCepModel>()));

            var CEP = 25645230;
            var result = await _controller.PostByCep(CEP);

            _service.Verify(s => s.Create(It.IsAny<string>()));

            Assert.IsTrue(result is CreatedResult);
        }

        [TestMethod]
        public async Task ShouldBePossibleDelete()
        {
            string nameCity = _fixture.Create<string>();

            var result = await _controller.Delete(nameCity);

            _service.Verify(s => s.Delete(It.IsAny<Expression<Func<City, bool>>>()));

            Assert.IsTrue(result is NoContentResult);
        }

        [TestMethod]
        public async Task ShouldBePossibleDeleteTemperatures()
        {
            string nameCity = _fixture.Create<string>();

            var result = await _controller.DeleteTemperatures(nameCity);

            _serviceTemperature.Verify(s => s.Delete(It.IsAny<Expression<Func<Temperature, bool>>>()));

            Assert.IsTrue(result is NoContentResult);
        }

        [TestMethod]
        public async Task ShouldNotBePossiblePostByCep()
        {
            _userAgent.Setup(u => u.GetAsync<ViaCepModel>(It.IsAny<string>())).Returns(Task.FromResult(_fixture.Create<ViaCepModel>()));

            var CEP = 2564523;
            var exception = await Assert.ThrowsExceptionAsync<Exception>(() => _controller.PostByCep(CEP));

            Assert.AreEqual(StoneApplicationResources.InvalidCEP, exception.Message);
        }
    }
}
