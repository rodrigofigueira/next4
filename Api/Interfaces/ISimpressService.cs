using Api.Models;
using Api.Models.DTO.Simpress;
using System.Threading.Tasks;

namespace Api.Interfaces
{
    public interface ISimpressService
    {
        public Task<bool> Post(SimpressAccountPost simpressAccountPost);

        public Task<bool> Patch(string accountId, SimpressAccountPatch simpressAccountPatch);

        public Task<SimpressAccountValue> GetById(string id);
        
        public Task<dynamic> GetByEmail(string email);

    }

}
