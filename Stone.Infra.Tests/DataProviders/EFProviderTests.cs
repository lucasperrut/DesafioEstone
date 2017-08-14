using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using Stone.Infra.Providers;
using Ploeh.AutoFixture;
using Stone.Domain.Entities;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace Stone.Infra.Tests.DataProviders
{
    [TestClass]
    public class EFProviderTests
    {
        private Mock<DbContext> _context;
        private Mock<DbSet<City>> _dbSet;
        private EFProvider _provider;
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _context = new Mock<DbContext>();
            _dbSet = new Mock<DbSet<City>>();
            _provider = new EFProvider(_context.Object);
            _fixture = new Fixture();
        }

        [TestMethod]
        public async Task ShouldBePossibleCreate()
        {
            var _dbSet = new Mock<DbSet<Temperature>>();
            _context.Setup(c => c.Set<Temperature>()).Returns(_dbSet.Object);

            var temperature = new Temperature();

            await _provider.Create(temperature);

            _context.Verify(c => c.SaveChangesAsync());
        }

        [TestMethod]
        public async Task ShouldBePossibleDelete()
        {
            var name = _fixture.Create<string>();

            var listCity = new List<City>();

            IQueryable<City> queryableList = listCity.AsQueryable();
            _dbSet.As<IQueryable<City>>().Setup(x => x.Provider).Returns(queryableList.Provider);
            _dbSet.As<IQueryable<City>>().Setup(x => x.Expression).Returns(queryableList.Expression);
            _dbSet.As<IQueryable<City>>().Setup(x => x.ElementType).Returns(queryableList.ElementType);
            _dbSet.As<IQueryable<City>>().Setup(x => x.GetEnumerator()).Returns(() => queryableList.GetEnumerator());

            _context.Setup(x => x.Set<City>()).Returns(_dbSet.Object);

            await _provider.Delete<City>(c => c.Name == name);

            _context.Verify(c => c.SaveChangesAsync());
        }

        [TestMethod]
        public async Task ShouldBePosasibleCreate()
        {
            _context.Setup(c => c.Set<City>()).Returns(_dbSet.Object);

            var city = new City();

            await _provider.Create(city);

            _context.Verify(c => c.SaveChangesAsync());
        }

        [TestMethod]
        public async Task ShouldBePosasibleCreateMany()
        {
            var dbSet = new Mock<DbSet<Temperature>>();
            _context.Setup(c => c.Set<Temperature>()).Returns(dbSet.Object);

            var Temperatures = new List<Temperature>
            {
                new Temperature()
            };

            await _provider.CreateMany(Temperatures);

            _context.Verify(c => c.SaveChangesAsync());
        }

        [TestMethod]
        public async Task ShouldBePossibleGetAll()
        {
            var expectedList = new List<City>() { new City() }.AsQueryable();

            var _dbAsyncEnum = new Mock<IDbAsyncEnumerator<City>>();

            _dbSet.As<IQueryable<City>>().Setup(c => c.Provider).Returns(expectedList.Provider);
            _dbSet.As<IQueryable<City>>().Setup(c => c.Expression).Returns(expectedList.Expression);
            _dbSet.As<IQueryable<City>>().Setup(c => c.ElementType).Returns(expectedList.ElementType);
            _dbSet.As<IQueryable<City>>().Setup(c => c.GetEnumerator()).Returns(expectedList.GetEnumerator());
            _dbSet.As<IDbAsyncEnumerable<City>>().Setup(c => c.GetAsyncEnumerator()).Returns(_dbAsyncEnum.Object);

            _context.Setup(c => c.Set<City>()).Returns(_dbSet.Object);

            var resultList = await _provider.GetAll<City>();

            Assert.IsNotNull(resultList);
        }
    }
}
