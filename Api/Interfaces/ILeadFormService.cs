using Api.Models;
using System.Threading.Tasks;

namespace Api.Interfaces
{
    public interface ILeadFormService
    {
        public Task<LeadForm> Post(LeadForm leadForm);

        public Task<bool> Update(LeadForm leadForm);

        public Task<bool> Delete(int id);

        public Task<LeadForm> GetById(int id);
    }

}
