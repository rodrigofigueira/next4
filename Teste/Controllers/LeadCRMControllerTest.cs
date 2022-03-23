using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Interfaces;
using Api.Repository;
using Api.Controllers;
using Xunit;
using AutoBogus;
using Api.Models;
using Api.Models.Util;
using Teste.Classes;
using Microsoft.AspNetCore.Mvc;
using Api.Services;

namespace Teste.Controllers
{
    public class LeadCRMControllerTest
    {

        private LeadCRMService _leadCRMService;
        private LeadCRMRepository _leadCRMRepository;
        private LeadCRMController _leadCRMController;

        public LeadCRMControllerTest()
        {

            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()
                           .UseInMemoryDatabase("next4_hashsecurity_controller")
                           .Options;

            this._leadCRMRepository = new LeadCRMRepository(new DataContext(options));
            this._leadCRMService = new LeadCRMService(this._leadCRMRepository);
            this._leadCRMController = new LeadCRMController(this._leadCRMService);

        }

        [Fact]
        public async Task TestPost()
        {
            var autoFaker = new AutoFaker<LeadCRM>();
            LeadCRM leadCRM = autoFaker.Generate();

            var okResult = await _leadCRMController.Post(leadCRM);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);
        }


        [Fact]
        public async Task TestPut()
        {
            var autoFaker = new AutoFaker<LeadCRM>();
            LeadCRM leadCRM = autoFaker.Generate();

            await _leadCRMRepository.Post(leadCRM);

            leadCRM.ListaTucunare = 1;

            var okResult = await _leadCRMController.Update(leadCRM);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public async Task TestDelete()
        {
            var autoFaker = new AutoFaker<LeadCRM>();
            LeadCRM leadCRM = autoFaker.Generate();

            await _leadCRMRepository.Post(leadCRM);

            var okResult = await _leadCRMController.Delete(leadCRM.Id);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public async Task TestGetById()
        {
            var autoFaker = new AutoFaker<LeadCRM>();
            LeadCRM leadCRM = autoFaker.Generate();

            await _leadCRMRepository.Post(leadCRM);

            var okResult = await _leadCRMController.GetById(leadCRM.Id);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);
        }



    }
}
