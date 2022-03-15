using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Api.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Api.Interfaces;
using System;

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


    }
}
