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

namespace Teste
{
    public class HashSecurityApiControllerTest
    {

        private HashSecurityAPIService _hashSecurityService;
        private IHashSecurityAPIRepository _hashSecurityRepository;
        private HashSecurityAPIController _hashSecurityController;

        public HashSecurityApiControllerTest()
        {

            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()
                           .UseInMemoryDatabase("next4_hashsecurity_controller")
                           .Options;

            this._hashSecurityRepository = new HashSecurityAPIRepository(new DataContext(options));
            this._hashSecurityService = new HashSecurityAPIService(this._hashSecurityRepository);
            this._hashSecurityController = new HashSecurityAPIController(this._hashSecurityService);

        }

        [Fact]
        public async Task TestPost()
        {
            var autoFaker = new AutoFaker<HashSecurityAPI>();
            HashSecurityAPI hashSecurityAPI = autoFaker.Generate();

            var okResult = await _hashSecurityController.Post(hashSecurityAPI);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public async Task TestPut()
        {
            var autoFaker = new AutoFaker<HashSecurityAPI>();
            HashSecurityAPI hashSecurityAPI = autoFaker.Generate();

            await _hashSecurityRepository.Post(hashSecurityAPI);

            hashSecurityAPI.Restriction = "alterado";

            var okResult = await _hashSecurityController.Update(hashSecurityAPI);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public async Task TestDelete()
        {
            var autoFaker = new AutoFaker<HashSecurityAPI>();
            HashSecurityAPI hashSecurityAPI = autoFaker.Generate();

            await _hashSecurityRepository.Post(hashSecurityAPI);

            var okResult = await _hashSecurityController.Delete(hashSecurityAPI.Id);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public async Task TestGetById()
        {
            var autoFaker = new AutoFaker<HashSecurityAPI>();
            HashSecurityAPI hashSecurityAPI = autoFaker.Generate();

            await _hashSecurityRepository.Post(hashSecurityAPI);

            var okResult = await _hashSecurityController.GetById(hashSecurityAPI.Id);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);
        }


    }
}
