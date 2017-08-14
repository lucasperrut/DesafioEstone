using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Stone.Application.Services;
using Stone.Domain.Entities;
using Stone.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Stone.Application.Tests.Services
{
    [TestClass]
    public class CityApplicationServiceTests
    {
        private Mock<IRepository> _repository;
        private CityApplicationService _service;
        private TemperatureApplicationService _serviceTemperature;
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _repository = new Mock<IRepository>();
            _service = new CityApplicationService(_repository.Object);
            _serviceTemperature = new TemperatureApplicationService(_repository.Object);
            _fixture = new Fixture();
        }

        [TestMethod]
        public async Task ShouldBePossibleCreate()
        {
            var nameCity = _fixture.Create<string>();

            var result = await _service.Create(nameCity);

            _repository.Verify(r => r.Create<City>(It.IsAny<City>()));

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task ShouldBePossibleDelete()
        {
            var filter = _fixture.Create<Expression<Func<City, bool>>>();

            await _service.Delete(filter);

            _repository.Verify(r => r.Delete(filter));
        }

        [TestMethod]
        public async Task ShouldBePossibleGetAll()
        {
            _repository.Setup(r => r.GetAll(It.IsAny<Func<City, DateTime>>(), It.IsAny<int>())).ReturnsAsync(new List<City> { new City() });
            var result = await _service.GetAll(It.IsAny<int>());

            _repository.Verify(r => r.GetAll(It.IsAny<Func<City, DateTime>>(), It.IsAny<int>()));

            Assert.IsTrue(result.Count() > 0);
        }
    }
}
