using Microsoft.AspNetCore.Mvc;
using Api.Models;
using System.Threading.Tasks;
using Api.Interfaces;
using System.IO;
using System;
using Newtonsoft.Json;
using Api.Models.DTO.RD;
using System.Text.Json;

namespace Api.Controllers
{
    public class LeadRDController : BaseApiController
    {

        private ILeadRDService _leadRDService;
        private ILeadFormService _leadFormService;

        public LeadRDController(ILeadRDService leadRDService, ILeadFormService leadFormService)
        {
            _leadRDService = leadRDService;
            _leadFormService = leadFormService;
        }

        [HttpPost]
        public async Task<ActionResult<LeadRD>> Post([FromBody] LeadRD leadRD)
        {
            return Ok(await _leadRDService.Post(leadRD));
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] LeadRD leadRD)
        {
            bool inseriu = await _leadRDService.Update(leadRD);
            return inseriu ? Ok("Atualizado") : BadRequest("Ocorreu um erro ao inserir");
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<LeadRD>> GetById([FromRoute] int id)
        {
            return Ok(await _leadRDService.GetById(id));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var deletou = await _leadRDService.Delete(id);
            return deletou ? Ok("Deletado") : BadRequest("Ocorreu um erro ao deletar");
        }
      
        [HttpPost]
        [Route("rd")]
        public async Task<ActionResult> PostRD(dynamic value)
        {
            var jsonSerializado = System.Text.Json.JsonSerializer.Serialize(value);
            RdWebhook leadRdRecebido = JsonConvert.DeserializeObject<RdWebhook>(jsonSerializado);

            Content content = leadRdRecebido.leads[0].first_conversion.content;
            ConversionOrigin conversionOrigin = leadRdRecebido.leads[0].first_conversion.conversion_origin;

            LeadForm leadForm = new LeadForm
            {
                Nome = content.nome,
                Sobrenome = content.Sobrenome,
                Empresa = content.empresa,
                CNPJ = content.CNPJ,
                TelefoneContato = content.telefone,
                Email = content.email_lead,
                PilarNegocio = content.QualOPilarDoSeuNegocioQueVoceDesejaFalar,
                QuantidadeEquipamentos = content.QuantidadeDePilares,
                VolumeImpressao = content.Volume,
                Mensagem = content.Mensagem
            };

            LeadRD leadRd = new LeadRD
            {
                DataEntrada = DateTime.Now,
                TrafficSource = conversionOrigin.source,
                TrafficCampaign = conversionOrigin.campaign,
                TrafficMedium = conversionOrigin.medium,
                TrafficValue = conversionOrigin.value,
                LeadForm = leadForm
            };

            await _leadRDService.Post(leadRd);

            return NoContent();

        }

        private void GravarDadosNoTxt(string value)
        {
            StreamWriter streamWriter = new StreamWriter("LogRD.txt", true);
            streamWriter.WriteLine(value);
            streamWriter.Close();
        }

    }
}
