using Api.Interfaces;
using Api.Models;
using System.Threading.Tasks;

namespace Api.Services
{
    public class HashSecurityAPIService : IHashSecurityAPIService
    {

        private readonly IHashSecurityAPIRepository _hashSecurityAPIRepository;

        public HashSecurityAPIService(IHashSecurityAPIRepository hashSecurityAPI)
        {
            this._hashSecurityAPIRepository = hashSecurityAPI;
        }

        public async Task<bool> Delete(int id)
        {
            bool deletou = await _hashSecurityAPIRepository.Delete(id);
            return deletou ? true : false;
        }

        public async Task<HashSecurityAPI> GetById(int id)
        {
            return await _hashSecurityAPIRepository.GetById(id);
        }

        public async Task<HashSecurityAPI> Post(HashSecurityAPI hashSecurity)
        {
            return await _hashSecurityAPIRepository.Post(hashSecurity);
        }

        public async Task<bool> Update(HashSecurityAPI hashSecurity)
        {
            return await _hashSecurityAPIRepository.Update(hashSecurity);
        }


    }
}
