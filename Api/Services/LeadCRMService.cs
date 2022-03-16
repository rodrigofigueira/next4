using Api.Interfaces;
using Api.Models;
using System.Threading.Tasks;

namespace Api.Services
{
    public class LeadCRMService : ILeadCRMService
    {
        private readonly ILeadCRMRepository _leadCRMRepository;

        public LeadCRMService(ILeadCRMRepository leadCRMRepository)
        {
            this._leadCRMRepository = leadCRMRepository;
        }

        public async Task<bool> Delete(int id)
        {
            bool deletou = await _leadCRMRepository.Delete(id);
            return deletou ? true : false;
        }

        public async Task<LeadCRM> GetById(int id)
        {
            return await _leadCRMRepository.GetById(id);
        }

        public async Task<LeadCRM> Post(LeadCRM leadRD)
        {
            return await _leadCRMRepository.Post(leadRD);
        }

        public async Task<bool> Update(LeadCRM leadRD)
        {
            return await _leadCRMRepository.Update(leadRD);
        }

    }
}
