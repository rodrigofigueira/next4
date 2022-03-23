using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Interfaces;
using Api.Models;
using System.Collections.Generic;

namespace Api.Repository
{
    public class LeadFormRepository : ILeadFormRepository
    {

        private DataContext _context;

        public LeadFormRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(int id)
        {
            var _leadForm = await _context.LeadForms.Where(u => u.Id == id).FirstOrDefaultAsync();
            _context.LeadForms.Remove(_leadForm);
            int changes = await _context.SaveChangesAsync();
            return changes > 0 ? true : false;
        }

        /// <summary>
        /// Retorna os leads recebidos, via Webhook do RD Station, que ainda não foram integrados para a Simpress
        /// </summary>
        /// <returns>Lista de LeadForms</returns>
        public async Task<List<LeadForm>> ToIntegrate()
        {
            return await _context.LeadForms
                                 .Include(l => l.LeadRDs)
                                 .Where(l => l.DataIntegracao == null && l.QuantidadeTentativas < 3)
                                 .ToListAsync();
        }

        public async Task<LeadForm> GetById(int id)
        {
            return await _context.LeadForms.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<LeadForm> Post(LeadForm leadForm)
        {
            _context.LeadForms.Add(leadForm);
            await _context.SaveChangesAsync();
            return leadForm;

        }

        public async Task<bool> Update(LeadForm leadForm)
        {
            LeadForm _leadForm = await _context.LeadForms
                                        .Where(u => u.Id == leadForm.Id).FirstOrDefaultAsync();

            _leadForm.CNPJ = leadForm.CNPJ;
            _leadForm.Email = leadForm.Email;
            _leadForm.Empresa = leadForm.Empresa;
            _leadForm.Mensagem = leadForm.Mensagem;
            _leadForm.Nome = leadForm.Nome;
            _leadForm.PilarNegocio = leadForm.PilarNegocio;
            _leadForm.QuantidadeEquipamentos = leadForm.QuantidadeEquipamentos;
            _leadForm.Sobrenome = leadForm.Sobrenome;
            _leadForm.TelefoneContato = leadForm.TelefoneContato;
            _leadForm.VolumeImpressao = leadForm.VolumeImpressao;

            _context.LeadForms.Update(_leadForm);
            int atualizou = await _context.SaveChangesAsync();
            return atualizou > 0 ? true : false;
        }
    }
}
