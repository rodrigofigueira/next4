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
    public class LeadRDControllerTest
    {

        private LeadRDService _leadRDService;
        private LeadFormService _leadFormService;
        private LeadRDRepository _leadRDRepository;
        private LeadFormRepository _leadFormRepository;
        private LeadRDController _leadRDController;

        public LeadRDControllerTest()
        {

            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()
                           .UseInMemoryDatabase("next4_leadrd_controller")
                           .Options;

            this._leadRDRepository = new LeadRDRepository(new DataContext(options));
            this._leadRDService = new LeadRDService(this._leadRDRepository);
            this._leadFormService = new LeadFormService(this._leadFormRepository, new SimpressFakeFail());
            this._leadRDController = new LeadRDController(this._leadRDService, this._leadFormService);

        }

        [Fact]
        public async Task TestPost()
        {
            var autoFaker = new AutoFaker<LeadRD>();
            LeadRD leadRD = autoFaker.Generate();

            var okResult = await _leadRDController.Post(leadRD);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);
        }


        [Fact]
        public async Task TestPut()
        {
            var autoFaker = new AutoFaker<LeadRD>();
            LeadRD leadRD = autoFaker.Generate();

            await _leadRDRepository.Post(leadRD);

            leadRD.TrafficMedium = "teste";

            var okResult = await _leadRDController.Update(leadRD);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public async Task TestDelete()
        {
            var autoFaker = new AutoFaker<LeadRD>();
            LeadRD leadRD = autoFaker.Generate();

            await _leadRDRepository.Post(leadRD);

            var okResult = await _leadRDController.Delete(leadRD.Id);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public async Task TestGetById()
        {
            var autoFaker = new AutoFaker<LeadRD>();
            LeadRD leadRD = autoFaker.Generate();

            await _leadRDRepository.Post(leadRD);

            var okResult = await _leadRDController.GetById(leadRD.Id);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);
        }


    }
}
