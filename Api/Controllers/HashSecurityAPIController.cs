using Microsoft.AspNetCore.Mvc;
using Api.Models;
using System.Threading.Tasks;
using Api.Interfaces;

namespace Api.Controllers
{
    public class HashSecurityAPIController : BaseApiController
    {
        private IHashSecurityAPIService _hashSecurityAPIService;

        public HashSecurityAPIController(IHashSecurityAPIService hashSecurityAPIService)
        {
            _hashSecurityAPIService = hashSecurityAPIService;
        }

        [HttpPost]
        public async Task<ActionResult<HashSecurityAPI>> Post([FromBody] HashSecurityAPI hashSecurityAPI)
        {
            return Ok(await _hashSecurityAPIService.Post(hashSecurityAPI));
        }

        [HttpPut]
        public async Task<ActionResult<string>> Update([FromBody] HashSecurityAPI hashSecurityAPI)
        {
            bool inseriu = await _hashSecurityAPIService.Update(hashSecurityAPI);
            return inseriu ? Ok("Atualizado") : BadRequest("Ocorreu um erro ao atualizar");
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<HashSecurityAPI>> GetById([FromRoute] int id)
        {
            return Ok(await _hashSecurityAPIService.GetById(id));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<string>> Delete([FromRoute] int id)
        {
            var deletou = await _hashSecurityAPIService.Delete(id);
            return deletou ? Ok("Deletado") : BadRequest("Ocorreu um erro ao deletar");
        }


    }
}
