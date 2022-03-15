using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Interfaces;
using Api.Models;

namespace Api.Repository
{
    public class LeadRDRepository : ILeadRDRepository
    {

        private DataContext _context;

        public LeadRDRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(int id)
        {
            var _leadRD = await _context.LeadRDs.Where(u => u.Id == id).FirstOrDefaultAsync();
            _context.LeadRDs.Remove(_leadRD);
            int changes = await _context.SaveChangesAsync();
            return changes > 0 ? true : false;
        }

        public async Task<LeadRD> GetById(int id)
        {
            return await _context.LeadRDs.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<LeadRD> Post(LeadRD leadRD)
        {
            _context.LeadRDs.Add(leadRD);
            await _context.SaveChangesAsync();
            return leadRD;

        }

        public async Task<bool> Update(LeadRD leadRD)
        {
            LeadRD _leadRD = await _context.LeadRDs
                                        .Where(u => u.Id == leadRD.Id).FirstOrDefaultAsync();

            _leadRD.LeadForm = leadRD.LeadForm;
            _leadRD.DataEntrada = leadRD.DataEntrada;
            _leadRD.EventType = leadRD.EventType;
            _leadRD.EventFamily = leadRD.EventFamily;
            _leadRD.ConversionIdentifier = leadRD.ConversionIdentifier;
            _leadRD.ClientTrackingId = leadRD.ClientTrackingId;
            _leadRD.TrafficSource = leadRD.TrafficSource;
            _leadRD.TrafficMedium = leadRD.TrafficMedium;
            _leadRD.TrafficCampaign = leadRD.TrafficCampaign;
            _leadRD.TrafficValue = leadRD.TrafficValue;

            _context.LeadRDs.Update(_leadRD);
            int atualizou = await _context.SaveChangesAsync();
            return atualizou > 0 ? true : false;

        }


    }
}
