using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Interfaces;
using Api.Models;
using Api.Models.DTO.Simpress;

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
        [Route("GetById/{id}")]
        public async Task<ActionResult<SimpressAccountValue>> GetById([FromRoute] string id)
        {
            if (id.Count() < 36) return BadRequest("Id deve ter o seguinte formato: 00000000-0000-0000-0000-000000000000");

            var contato = await _simpressService.GetById(id);
            return Ok(contato);
        }

        [HttpGet]
        [Route("GetByEmail/{email}")]
        public async Task<ActionResult<SimpressAccountValue>> GetByEmail([FromRoute] string email)
        {            
            var contato = await _simpressService.GetByEmail(email);
            return Ok(contato);
        }


        [HttpPost]
        public async Task<ActionResult> Post(SimpressAccountPost simpressAccount)
        {
            if(await _simpressService.Post(simpressAccount)) return NoContent();
            return BadRequest("Não foi possível realizar o post");
        }

    }
}
