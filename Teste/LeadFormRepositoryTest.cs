using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data;
using Api.Models.DTO.User;
using System.Linq;
using Api.Models;
using Api.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using Api.Interfaces;
using Xunit;
using AutoBogus;

namespace Teste
{
    public class LeadFormRepositoryTest
    {
        private LeadFormRepository _leadFormRepository;

        public LeadFormRepositoryTest()
        {
            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()
                           .UseInMemoryDatabase("next4_leadForm_repository")
                           .Options;

            DataContext dataContext = new DataContext(options);

            this._leadFormRepository = new LeadFormRepository(dataContext);
        }

        [Fact]
        public async Task ComRegistrosElegiveisParaIntegracao()
        {

            const int totalDeRegistrosParaInserir = 10;

            var autoFakerLeadRD = new AutoFaker<LeadRD>()
                                .RuleFor(o => o.Id, f => 0)
                                .RuleFor(o => o.LeadForm, f => null);

            var autoFakerLeadForm = new AutoFaker<LeadForm>()
                                .RuleFor(o => o.Id, f => 0)
                                .RuleFor(o => o.DataIntegracao, f => null)
                                .RuleFor(o => o.QuantidadeTentativas, f => 0)
                                .RuleFor(o => o.LeadRDs, f => {

                                    List<LeadRD> leadRDs = new List<LeadRD>();
                                    for (int i = 0; i < 3; i++)
                                    {
                                        leadRDs.Add(autoFakerLeadRD.Generate());
                                    }

                                    return leadRDs;
                                });
            
            for (int i = 0; i < totalDeRegistrosParaInserir; i++)
            {
                LeadForm leadForm = autoFakerLeadForm.Generate();
                await _leadFormRepository.Post(leadForm);
            }

            var leadsElegiveisParaIntegracao = await _leadFormRepository.ToIntegrate();
            Assert.True(leadsElegiveisParaIntegracao.Count == totalDeRegistrosParaInserir);

        }

        [Fact]
        public async Task SemRegistrosElegiveisParaIntegracao()
        {
            const int totalDeRegistrosParaInserir = 10;

            var autoFakerLeadRD = new AutoFaker<LeadRD>()
                                .RuleFor(o => o.Id, f => 0)
                                .RuleFor(o => o.LeadForm, f => null);

            var autoFakerLeadForm = new AutoFaker<LeadForm>()
                                .RuleFor(o => o.Id, f => 0)
                                .RuleFor(o => o.DataIntegracao, f => DateTime.Now)
                                .RuleFor(o => o.QuantidadeTentativas, f => 10)
                                .RuleFor(o => o.LeadRDs, f => {

                                    List<LeadRD> leadRDs = new List<LeadRD>();
                                    for (int i = 0; i < 3; i++)
                                    {
                                        leadRDs.Add(autoFakerLeadRD.Generate());
                                    }

                                    return leadRDs;
                                });

            for (int i = 0; i < totalDeRegistrosParaInserir; i++)
            {
                LeadForm leadForm = autoFakerLeadForm.Generate();
                await _leadFormRepository.Post(leadForm);
            }

            var leadsElegiveisParaIntegracao = await _leadFormRepository.ToIntegrate();
            Assert.True(leadsElegiveisParaIntegracao.Count == 0);

        }

    }
}
