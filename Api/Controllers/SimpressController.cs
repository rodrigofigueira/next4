using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Api.Interfaces;
using Api.Models;
using Api.Models.DTO.Simpress;
using System.IO;
using System;

namespace Api.Controllers
{
    public class SimpressController : BaseApiController
    {
        private ISimpressService _simpressService;

        public SimpressController(ISimpressService simpressService)
        {
            _simpressService = simpressService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<SimpressAccountValue>> GetById([FromRoute] string id)
        {
            if (id.Count() < 36) return BadRequest("Id deve ter o seguinte formato: 00000000-0000-0000-0000-000000000000");

            var contato = await _simpressService.GetById(id);
            return Ok(contato);
        }

        [HttpGet]
        [Route("emails/{email}")]
        public async Task<ActionResult<SimpressAccountValue>> GetByEmail([FromRoute] string email)
        {
            var contato = await _simpressService.GetByEmail(email);
            return Ok(contato);
        }

        [HttpPost]
        public async Task<ActionResult> Post(SimpressAccountPost simpressAccount)
        {
            if (await _simpressService.Post(simpressAccount)) return NoContent();
            return BadRequest("Não foi possível realizar o post");
        }

        [HttpPatch]
        [Route("{accountId}")]
        public async Task<ActionResult> Patch([FromRoute] string accountId, SimpressAccountPatch simpressAccountPatch)
        {
            if (await _simpressService.Patch(accountId, simpressAccountPatch)) return NoContent();
            return BadRequest("Não foi possível realizar o patch");
        }

        [HttpPost]
        [Route("simpress/webhook")]
        public async Task<ActionResult> WebhookRecebidoDaAPISimpress(dynamic value)
        {
            await GravarDadosNoTxt($"Objeto recebido da API do Simpress em {DateTime.Now.ToString("dd/MM/yyyy")} \n {value}\n");
            return NoContent();
        }

        private static async Task GravarDadosNoTxt(string value)
        {
            StreamWriter streamWriter = new StreamWriter("LogSimpress.txt", true);
            await streamWriter.WriteLineAsync(value);
            streamWriter.Close();
        }
    }
}
