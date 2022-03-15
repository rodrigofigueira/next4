using Api.Models;
using System.Threading.Tasks;

namespace Api.Interfaces
{
    public interface ILeadRDService
    {
        public Task<LeadRD> Post(LeadRD leadRD);

        public Task<bool> Update(LeadRD leadRD);

        public Task<bool> Delete(int id);

        public Task<LeadRD> GetById(int id);
    }
}
