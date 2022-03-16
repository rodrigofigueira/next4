using Api.Models;
using System.Threading.Tasks;

namespace Api.Interfaces
{
    public interface IHashSecurityAPIRepository
    {
        public Task<HashSecurityAPI> Post(HashSecurityAPI hashSecurityAPI);

        public Task<bool> Update(HashSecurityAPI hashSecurityAPI);

        public Task<bool> Delete(int id);

        public Task<HashSecurityAPI> GetById(int id);
    }
}
