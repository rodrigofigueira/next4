using Microsoft.AspNetCore.Mvc;
using Api.Models;
using System.Threading.Tasks;
using Api.Interfaces;
using System.IO;
using System;

namespace Api.Controllers
{
    public class LeadRDController : BaseApiController
    {

        private ILeadRDService _leadRDService;

        public LeadRDController(ILeadRDService leadRDService)
        {
            _leadRDService = leadRDService;
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

        [HttpGet]
        [Route("rd/{value}")]
        public ActionResult GetRD([FromRoute] string value)
        {
            GravarDadosNoTxt($"GetRD feito em {DateTime.Now.ToString("dd/MM/yyyy")} \n {value}\n");
            return Ok();
        }

        [HttpPost]
        [Route("rd")]
        public ActionResult PostRD(string value)
        {
            GravarDadosNoTxt($"PostRD feito em {DateTime.Now.ToString("dd/MM/yyyy")} \n {value}\n");
            return Ok(value);
        }


        private void GravarDadosNoTxt(string value)
        {
            StreamWriter streamWriter = new StreamWriter("LogRD.txt", true);
            streamWriter.WriteLine(value);
            streamWriter.Close();
        }

    }
}
