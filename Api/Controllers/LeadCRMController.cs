using Microsoft.AspNetCore.Mvc;
using Api.Models;
using System.Threading.Tasks;
using Api.Interfaces;

namespace Api.Controllers
{
    public class LeadCRMController : BaseApiController
    {
        private ILeadCRMService _leadCRMService;

        public LeadCRMController(ILeadCRMService leadCRMService)
        {
            _leadCRMService = leadCRMService;
        }

        [HttpPost]
        public async Task<ActionResult<LeadCRM>> Post([FromBody] LeadCRM leadCRM)
        {
            return Ok(await _leadCRMService.Post(leadCRM));
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] LeadCRM leadCRM)
        {
            bool inseriu = await _leadCRMService.Update(leadCRM);
            return inseriu ? Ok("Atualizado") : BadRequest("Ocorreu um erro ao atualizar");
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<LeadCRM>> GetById([FromRoute] int id)
        {
            return Ok(await _leadCRMService.GetById(id));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var deletou = await _leadCRMService.Delete(id);
            return deletou ? Ok("Deletado") : BadRequest("Ocorreu um erro ao deletar");
        }


    }
}
