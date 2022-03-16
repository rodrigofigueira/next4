using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Interfaces;
using Api.Models;

namespace Api.Repository
{
    public class LeadCRMRepository : ILeadCRMRepository
    {

        private DataContext _context;

        public LeadCRMRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(int id)
        {
            var _leadCRM = await _context.LeadCRMs.Where(u => u.Id == id).FirstOrDefaultAsync();
            _context.LeadCRMs.Remove(_leadCRM);
            int changes = await _context.SaveChangesAsync();
            return changes > 0 ? true : false;
        }

        public async Task<LeadCRM> GetById(int id)
        {
            return await _context.LeadCRMs.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<LeadCRM> Post(LeadCRM leadCRM)
        {
            _context.LeadCRMs.Add(leadCRM);
            await _context.SaveChangesAsync();
            return leadCRM;

        }

        public async Task<bool> Update(LeadCRM leadCRM)
        {
            LeadCRM _leadCRM = await _context.LeadCRMs
                                        .Where(u => u.Id == leadCRM.Id).FirstOrDefaultAsync();
                        
            _context.Entry(_leadCRM).CurrentValues.SetValues(leadCRM);

            //todo: se o objeto passado for idêntico ao do banco o SaveChanges retorna 0
            //e consequentemente false
            int atualizou = await _context.SaveChangesAsync();
            return atualizou > 0 ? true : false;
        }

    }
}
