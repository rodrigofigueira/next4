using Api.Interfaces;
using Api.Models;
using System.Threading.Tasks;

namespace Api.Repository
{
    public class LeadFormService : ILeadFormService
    {

        private readonly ILeadFormRepository _leadFormRepository;

        public LeadFormService(ILeadFormRepository leadFormRepository)
        {
            this._leadFormRepository = leadFormRepository;
        }

        public async Task<bool> Delete(int id)
        {
            bool deletou = await _leadFormRepository.Delete(id);
            return deletou ? true : false;
        }

        public async Task<LeadForm> GetById(int id)
        {
            return await _leadFormRepository.GetById(id);
        }

        public async Task<LeadForm> Post(LeadForm leadForm)
        {
            return await _leadFormRepository.Post(leadForm);
        }

        public async Task<bool> Update(LeadForm leadForm)
        {
            return await _leadFormRepository.Update(leadForm);
        }

    }
}
