using Api.Interfaces;
using Api.Models;
using Api.Models.DTO.Simpress;
using Api.Models.Util;
using Api.Extensions;
using System.Threading.Tasks;

namespace Api.Repository
{
    public class LeadFormService : ILeadFormService
    {

        private readonly ILeadFormRepository _leadFormRepository;
        private readonly ISimpressService _simpressService;

        public LeadFormService(ILeadFormRepository leadFormRepository, ISimpressService simpressService)
        {
            this._leadFormRepository = leadFormRepository;
            this._simpressService = simpressService;
        }

        public async Task<bool> Delete(int id)
        {
            bool deletou = await _leadFormRepository.Delete(id);
            return deletou ? true : false;
        }

        public async Task<ResumoIntegracaoSimpress> IntegrateWithSimpress()
        {
            var leadsParaIntegracao = await _leadFormRepository.ToIntegrate();
            ResumoIntegracaoSimpress resumo = new ResumoIntegracaoSimpress();

            foreach (var lead in leadsParaIntegracao)
            {

                SimpressAccountPost simpress = new SimpressAccountPost();
                simpress.name = lead.Nome;
                simpress.emailaddress1 = lead.Email;

                if (await _simpressService.Post(simpress))
                {
                    lead.DataIntegracao = System.DateTime.Now;
                    resumo.UUIDIntegradas.Add(lead.Id.ToString());
                }
                else
                {
                    resumo.UUIDNaoIntegradas.Add(lead.Id.ToString());
                }

                lead.QuantidadeTentativas++;
                await _leadFormRepository.Update(lead);

            }

            return resumo;

        }

        public async Task<ResumoIntegracaoSimpress> SendLeadToPost()
        {
            var leadsParaIntegracao = await _leadFormRepository.ToIntegrate();
            ResumoIntegracaoSimpress resumo = new ResumoIntegracaoSimpress();

            foreach (var lead in leadsParaIntegracao)
            {

                SimpressLeadPost simpress = new SimpressLeadPost();
                simpress.firstname = lead.Nome;
                simpress.lastname = lead.Sobrenome;
                simpress.companyname = lead.Empresa;
                simpress.stg_cnpj = lead.CNPJ;
                simpress.telephone1 = lead.TelefoneContato;
                simpress.emailaddress1 = lead.Email;
                simpress.sim_pilar_negocio_interesse = lead.PilarNegocio.ConverterCodigoPilarDeNegocios();
                simpress.sim_qtd_impressora_multifuncionais = lead.QuantidadeEquipamentos.ConvertCodigoQtdImpressorasMultiFuncionais();
                simpress.sim_volume_impressao = lead.VolumeImpressao.ConverterCodigoVolumeImpressao();
                simpress.qualificationcomments = lead.Mensagem;


                if (await _simpressService.PostLead(simpress))
                {
                    lead.DataIntegracao = System.DateTime.Now;
                    resumo.UUIDIntegradas.Add(lead.Id.ToString());
                }
                else
                {
                    resumo.UUIDNaoIntegradas.Add(lead.Id.ToString());
                }

                lead.QuantidadeTentativas++;
                await _leadFormRepository.Update(lead);

            }

            return resumo;

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
