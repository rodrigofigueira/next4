using Api.Interfaces;
using Api.Models;
using System.Threading.Tasks;

namespace Api.Services
{
    public class LeadRDService : ILeadRDService
    {

        private readonly ILeadRDRepository _leadRDRepository;

        public LeadRDService(ILeadRDRepository leadRDRepository)
        {
            this._leadRDRepository = leadRDRepository;
        }

        public async Task<bool> Delete(int id)
        {
            bool deletou = await _leadRDRepository.Delete(id);
            return deletou ? true : false;
        }

        public async Task<LeadRD> GetById(int id)
        {
            return await _leadRDRepository.GetById(id);
        }

        public async Task<LeadRD> Post(LeadRD leadRD)
        {
            return await _leadRDRepository.Post(leadRD);
        }

        public async Task<bool> Update(LeadRD leadRD)
        {
            return await _leadRDRepository.Update(leadRD);
        }


    }
}
