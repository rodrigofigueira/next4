using Api.Models;
using System.Threading.Tasks;

namespace Api.Interfaces
{
    public interface ILeadCRMRepository
    {
        public Task<LeadCRM> Post(LeadCRM leadCRM);

        public Task<bool> Update(LeadCRM leadCRM);

        public Task<bool> Delete(int id);

        public Task<LeadCRM> GetById(int id);

    }

}
