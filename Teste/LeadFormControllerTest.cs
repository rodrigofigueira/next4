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

namespace Teste
{
    public class LeadFormControllerTest
    {

        private ILeadFormService _leadFormService;
        private ILeadFormRepository _leadFormRepository;
        private LeadFormsController _leadFormController;

        public LeadFormControllerTest()
        {

            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()
                           .UseInMemoryDatabase("next4_leadform_controller")
                           .Options;
            this._leadFormRepository = new LeadFormRepository(new DataContext(options));
            this._leadFormService = new LeadFormService(this._leadFormRepository, new SimpressFakeFail());
            this._leadFormController = new LeadFormsController(this._leadFormService);
        }

        [Fact]
        public async Task IntegrateLeads()
        {
            int totalObjects = 10;

            var autoFakerLeadRD = new AutoFaker<LeadRD>()
                                .RuleFor(o => o.Id, f => 0)
                                .RuleFor(o => o.LeadForm, f => null);

            var autoFakerLeadForm = new AutoFaker<LeadForm>()
                                .RuleFor(o => o.Id, f => 0)
                                .RuleFor(o => o.DataIntegracao, f => null)
                                .RuleFor(o => o.QuantidadeTentativas, f => 1)
                                .RuleFor(o => o.LeadRDs, f => {

                                    List<LeadRD> leadRDs = new List<LeadRD>();
                                    for (int i = 0; i < 3; i++)
                                    {
                                        leadRDs.Add(autoFakerLeadRD.Generate());
                                    }

                                    return leadRDs;
                                });

            for (int i = 0; i < totalObjects; i++)
            {
                LeadForm leadForm = autoFakerLeadForm.Generate();
                await _leadFormRepository.Post(leadForm);
            }

            var response = await _leadFormController.IntegrationSimpress();
            var item = response.Result as OkObjectResult;

        }


    }
}
