using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Interfaces;

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
        public async Task<IActionResult> GetById()
        {
            var contato = await _simpressService.GetById("1");
            return Ok(contato);
        }
    }
}
