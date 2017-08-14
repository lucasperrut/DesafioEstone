using Stone.Domain.Entities;
using Ploeh.AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Stone.Common.Interfaces;
using Stone.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stone.Domain.Tests.Repositories
{
    [TestClass]
    public class RepositoryTests
    {
        private Mock<IDataProvider> _provider;
        private Repository _repository;
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _provider = new Mock<IDataProvider>();
            _repository = new Repository(_provider.Object);
            _fixture = new Fixture();
        }

        [TestMethod]
        public async Task ShouldBePossibleCreate()
        {
            var city = new City();

            await _repository.Create(city);

            _provider.Verify(c => c.Create(city));
        }

        [TestMethod]
        public async Task ShouldBePossibleCreateMany()
        {
            var cities = new List<City> { new City() };

            await _repository.CreateMany(cities);

            _provider.Verify(c => c.CreateMany(cities));
        }

        [TestMethod]
        public async Task ShouldBePossibleDelete()
        {
            var name = _fixture.Create<string>();

            await _repository.Delete<City>(c => c.Name == name);

            _provider.Verify(c => c.Delete<City>(d => d.Name == name));
        }

        [TestMethod]
        public async Task ShouldBePossibleFind()
        {
            var name = _fixture.Create<string>();

            _provider.Setup(p => p.FindWhere<City>(c => c.Name == name)).ReturnsAsync(new List<City> { new City() });

            var result = await _repository.FindWhere<City>(c => c.Name == name);

            _provider.Verify(c => c.FindWhere<City>(d => d.Name == name));

            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public async Task ShouldBePossibleGetAll()
        {
            _provider.Setup(p => p.GetAll<Temperature>()).ReturnsAsync(new List<Temperature> { new Temperature() });

            var result = await _repository.GetAll<Temperature>();
            _provider.Verify(c => c.GetAll<Temperature>());

            Assert.IsTrue(result.Count() > 0);
        }
    }
}
