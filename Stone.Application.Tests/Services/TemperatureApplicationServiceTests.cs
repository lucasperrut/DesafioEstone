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
    public class TemperatureApplicationServiceTests
    {
        private Mock<IRepository> _repository;
        private TemperatureApplicationService _service;
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _repository = new Mock<IRepository>();
            _service = new TemperatureApplicationService(_repository.Object);
            _fixture = new Fixture();
        }

        [TestMethod]
        public async Task ShouldBePossibleGetAll()
        {
            _repository.Setup(r => r.GetAll<Temperature>()).ReturnsAsync(new List<Temperature> { new Temperature() });

            var nameCity = _fixture.Create<string>();

            var result = await _service.GetAll();

            _repository.Verify(r => r.GetAll<Temperature>());

            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public async Task ShouldBePossibleFind()
        {
            var nameCity = _fixture.Create<string>();

            _repository.Setup(r => r.FindWhere<Temperature>(It.IsAny<Expression<Func<Temperature, bool>>>())).ReturnsAsync(new List<Temperature> { new Temperature
            {
                Date = DateTime.Now
            } });

            var result = await _service.Find(nameCity);

            _repository.Verify(r => r.FindWhere(It.IsAny<Expression<Func<Temperature, bool>>>()));

            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public async Task ShouldBePossibleDelete()
        {
            await _service.Delete(It.IsAny<Expression<Func<Temperature, bool>>>());

            _repository.Verify(r => r.Delete(It.IsAny<Expression<Func<Temperature, bool>>>()));
        }
    }
}
