using Api.Models;
using System.Threading.Tasks;

namespace Api.Interfaces
{
    public interface IHashSecurityAPIService
    {
        public Task<HashSecurityAPI> Post(HashSecurityAPI hashSecurity);

        public Task<bool> Update(HashSecurityAPI hashSecurity);

        public Task<bool> Delete(int id);

        public Task<HashSecurityAPI> GetById(int id);
    }
}
