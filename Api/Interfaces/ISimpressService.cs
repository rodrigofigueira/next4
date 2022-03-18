using Api.Models;
using System.Threading.Tasks;

namespace Api.Interfaces
{
    public interface ISimpressService
    {
        public Task<dynamic> Post(dynamic payload);

        public Task<dynamic> Update(dynamic payload);

        public Task<SimpressAccountValue> GetById(string id);
        
        public Task<dynamic> GetByEmail(string email);

    }

}
