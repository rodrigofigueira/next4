using Api.Interfaces;
using Api.Models;
using Api.Models.DTO.Simpress;
using System.Linq;
using System.Threading.Tasks;

namespace Teste.Classes
{
    public class SimpressFakeFail : ISimpressService
    {
        public Task<dynamic> GetByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public Task<SimpressAccountValue> GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Patch(string accountId, SimpressAccountPatch simpressAccountPatch)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Post(SimpressAccountPost simpressAccountPost)
        {
            await Task.Delay(100);
            if (simpressAccountPost.emailaddress1.Count() % 2 == 0) return true;
            return false;
        }

        public async Task<bool> PostLead(SimpressLeadPost simpressLeadPost)
        {
            await Task.Delay(100);
            return true;
        }
    }

}
