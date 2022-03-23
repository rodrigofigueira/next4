using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Api.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Api.Interfaces;
using System;
using Api.Models.Util;

namespace Api.Controllers
{
    public class LeadFormsController : BaseApiController
    {

        private ILeadFormService _leadFormService;

        public LeadFormsController(ILeadFormService leadFormService)
        {
            _leadFormService = leadFormService;
        }

        [HttpPost]
        public async Task<ActionResult<LeadForm>> Post([FromBody] LeadForm leadForm)
        {
            return Ok(await _leadFormService.Post(leadForm));
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] LeadForm leadForm)
        {
            bool inseriu = await _leadFormService.Update(leadForm);
            return inseriu ? Ok("Atualizado") : BadRequest("Ocorreu um erro ao inserir");
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<LeadForm>> GetById([FromRoute] int id)
        {
            return Ok(await _leadFormService.GetById(id));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var deletou = await _leadFormService.Delete(id);
            return deletou ? Ok("Deletado") : BadRequest("Ocorreu um erro ao deletar");
        }

        [HttpPost("integration_simpress")]
        public async Task<ActionResult<string>> IntegrationSimpress()
        {
            ResumoIntegracaoSimpress resumoIntegracao = await _leadFormService.IntegrateWithSimpress();

            if (resumoIntegracao.UUIDIntegradas.Count == 0) return BadRequest("Não houve integração");
            return Ok($@"Total de registros integrados {resumoIntegracao.UUIDIntegradas.Count} \n
                        Total de registros não integrados {resumoIntegracao.UUIDNaoIntegradas.Count}
                      ");
        }


    }
}
