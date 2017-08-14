using System.Threading.Tasks;

namespace Stone.Common.Interfaces
{
    public interface IHttpAgent
    {
        Task<T> GetAsync<T>(string url);
    }
}
