using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stone.Infra.Http;
using System.Threading.Tasks;
using Stone.Infra.Tests.Fakes;

namespace Stone.Infra.Tests.Http
{
    [TestClass]
    public class UserAgentTests
    {
        [TestMethod]
        public async Task ShouldBePossibleGet()
        {
            HttpAgent user = new HttpAgent();
            await user.GetAsync<FakeEntity>("http://viacep.com.br/ws/25645230/json");
        }
    }
}
