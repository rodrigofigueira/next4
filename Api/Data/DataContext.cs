using System;
using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<HashSecurityAPI> HashSecurityAPIs { get; set; }

        public DbSet<LeadCRM> LeadCRMs { get; set; }

        public DbSet<LeadForm> LeadForms { get; set; }

        public DbSet<LeadRD> LeadRDs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region User
            modelBuilder.Entity<User>().HasKey(u => u.Id);

            modelBuilder.Entity<User>().Property<int>(u => u.Id)
                                       .HasColumnName("id");

            modelBuilder.Entity<User>().HasIndex(u => u.Name)
                                       .IsUnique()
                                       .HasDatabaseName("NameIsUnique");

            modelBuilder.Entity<User>().Property<string>(u => u.Name)
                                       .HasColumnName("name_view")
                                       .HasMaxLength(100)
                                       .IsRequired();

            modelBuilder.Entity<User>().HasIndex(u => u.Email)
                                       .IsUnique()
                                       .HasDatabaseName("EmailIsUnique");

            modelBuilder.Entity<User>().Property<string>(u => u.Email)
                                       .HasColumnName("email")
                                       .HasMaxLength(100)
                                       .IsRequired();

            modelBuilder.Entity<User>().Property<string>(u => u.Password)
                                       .HasColumnName("password")
                                       .HasMaxLength(200)
                                       .IsRequired();

            modelBuilder.Entity<User>().Property<DateTime>(u => u.CreatedAt)
                                       .HasColumnName("dt_created");

            modelBuilder.Entity<User>().Property<DateTime>(u => u.UpdatedAt)
                                       .HasColumnName("dt_update")
                                       .HasDefaultValueSql("GETDATE()");


            modelBuilder.Entity<HashSecurityAPI>().HasKey(u => u.Id);

            modelBuilder.Entity<HashSecurityAPI>().Property<string>(u => u.HashSecurity)
                                       .HasMaxLength(250)
                                       .IsRequired();

            modelBuilder.Entity<HashSecurityAPI>().Property<int>(u => u.Status)
                                       .IsRequired();

            modelBuilder.Entity<HashSecurityAPI>().Property<string>(u => u.Restriction)
                                       .HasMaxLength(45)
                                       .IsRequired();
            #endregion

            #region LeadRD
            modelBuilder.Entity<LeadRD>().HasKey(u => u.Id);

            //modelBuilder.Entity<LeadRD>()
            //            .HasOne<LeadForm>(l => l.LeadForm)
            //            .WithMany()
            //            .HasForeignKey(x => x.LeadFormId);

            modelBuilder.Entity<LeadRD>().Property<DateTime>(u => u.DataEntrada)
                                       .IsRequired();

            modelBuilder.Entity<LeadRD>().Property<string>(u => u.EventType)
                                       .HasMaxLength(45);

            modelBuilder.Entity<LeadRD>().Property<string>(u => u.EventFamily)
                                       .HasMaxLength(45);

            modelBuilder.Entity<LeadRD>().Property<string>(u => u.ConversionIdentifier)
                                       .HasMaxLength(200);

            modelBuilder.Entity<LeadRD>().Property<string>(u => u.ClientTrackingId)
                                       .HasMaxLength(60);

            modelBuilder.Entity<LeadRD>().Property<string>(u => u.TrafficSource)
                                       .HasMaxLength(45);

            modelBuilder.Entity<LeadRD>().Property<string>(u => u.TrafficMedium)
                                       .HasMaxLength(15);

            modelBuilder.Entity<LeadRD>().Property<string>(u => u.TrafficCampaign)
                                       .HasMaxLength(60);

            modelBuilder.Entity<LeadRD>().Property<string>(u => u.TrafficValue)
                                       .HasMaxLength(45);
            #endregion

            #region LeadForm

            modelBuilder.Entity<LeadForm>().HasKey(u => u.Id);

            //definir o relacionamento

            modelBuilder.Entity<LeadForm>().Property<string>(u => u.Nome)
                                       .HasMaxLength(70);

            modelBuilder.Entity<LeadForm>().Property<string>(u => u.Sobrenome)
                                       .HasMaxLength(100);

            modelBuilder.Entity<LeadForm>().Property<string>(u => u.Empresa)
                                       .HasMaxLength(70);

            modelBuilder.Entity<LeadForm>().Property<string>(u => u.CNPJ)
                                       .HasMaxLength(30);

            modelBuilder.Entity<LeadForm>().Property<string>(u => u.TelefoneContato)
                                       .HasMaxLength(20);

            modelBuilder.Entity<LeadForm>().Property<string>(u => u.Email)
                                       .HasMaxLength(70);

            modelBuilder.Entity<LeadForm>().Property<string>(u => u.PilarNegocio)
                                       .HasMaxLength(100);

            modelBuilder.Entity<LeadForm>().Property<int>(u => u.QuantidadeEquipamentos);

            modelBuilder.Entity<LeadForm>().Property<int>(u => u.VolumeImpressao);

            modelBuilder.Entity<LeadForm>().Property<string>(u => u.Mensagem);

            #endregion

            #region LeadCRM

            modelBuilder.Entity<LeadCRM>().HasKey(u => u.Id);

            //modelBuilder.Entity<LeadCRM>()
            //            .HasOne<LeadForm>(l => l.LeadForm)
            //            .WithMany()
            //            .HasForeignKey(x => x.LeadFormId);


            modelBuilder.Entity<LeadCRM>().Property<string>(u => u.AccountIdCrm)
                                       .HasMaxLength(200);

            modelBuilder.Entity<LeadCRM>().Property<string>(u => u.StatusLead)
                                       .HasMaxLength(40);

            modelBuilder.Entity<LeadCRM>().Property<string>(u => u.EncLead)
                                       .HasMaxLength(40);

            modelBuilder.Entity<LeadCRM>().Property<string>(u => u.FilialAtendimento)
                                       .HasMaxLength(40);

            modelBuilder.Entity<LeadCRM>().Property<string>(u => u.GC)
                                       .HasMaxLength(70);

            modelBuilder.Entity<LeadCRM>().Property<string>(u => u.EmpresaCRM)
                                       .HasMaxLength(70);

            modelBuilder.Entity<LeadCRM>().Property<string>(u => u.ChaveCRM)
                                                   .HasMaxLength(100);

            modelBuilder.Entity<LeadCRM>().Property<string>(u => u.Vertical)
                                       .HasMaxLength(70);

            modelBuilder.Entity<LeadCRM>().Property<string>(u => u.FeedbackAtend);

            modelBuilder.Entity<LeadCRM>().Property<int>(u => u.Funcionarios);

            modelBuilder.Entity<LeadCRM>().Property<int>(u => u.Filiais);

            modelBuilder.Entity<LeadCRM>().Property<string>(u => u.Faturamento)
                                        .HasMaxLength(70);

            modelBuilder.Entity<LeadCRM>().Property<string>(u => u.StatusContaCRM)
                                        .HasMaxLength(40);

            modelBuilder.Entity<LeadCRM>().Property<string>(u => u.RazaoStatus)
                                        .HasMaxLength(40);

            modelBuilder.Entity<LeadCRM>().Property<string>(u => u.ModalidadeTucunare)
                                        .HasMaxLength(70);

            modelBuilder.Entity<LeadCRM>().Property<string>(u => u.ProbabilidadeFechamento)
                                        .HasMaxLength(20);

            modelBuilder.Entity<LeadCRM>().Property<DateTime>(u => u.DtEstimativaFechamento);

            modelBuilder.Entity<LeadCRM>().Property<string>(u => u.ReceitaMensalEstimada)
                                        .HasMaxLength(40);

            modelBuilder.Entity<LeadCRM>().Property<string>(u => u.ReceitaTucunare)
                                        .HasMaxLength(40);

            modelBuilder.Entity<LeadCRM>().Property<int>(u => u.PrazoContratoEstimado);

            modelBuilder.Entity<LeadCRM>().Property<string>(u => u.TotalContrato)
                                        .HasMaxLength(70);

            modelBuilder.Entity<LeadCRM>().Property<string>(u => u.PilarOpp)
                                        .HasMaxLength(70);

            modelBuilder.Entity<LeadCRM>().Property<int>(u => u.NumeroProjetoTucunare);

            modelBuilder.Entity<LeadCRM>().Property<int>(u => u.ListaTucunare);

            modelBuilder.Entity<LeadCRM>().Property<DateTime>(u => u.DtFechamentoNegocio);

            modelBuilder.Entity<LeadCRM>().Property<DateTime>(u => u.DtPerdaNegocio);

            modelBuilder.Entity<LeadCRM>().Property<DateTime>(u => u.DtCriacaoOpp);

            modelBuilder.Entity<LeadCRM>().Property<string>(u => u.EstagioProcesso)
                                        .HasMaxLength(40);

            modelBuilder.Entity<LeadCRM>().Property<string>(u => u.DescricaoProjeto);

            modelBuilder.Entity<LeadCRM>().Property<string>(u => u.GerenteConta)
                                        .HasMaxLength(70);

            modelBuilder.Entity<LeadCRM>().Property<string>(u => u.UnidadeNegocios)
                                        .HasMaxLength(20);

            modelBuilder.Entity<LeadCRM>().Property<string>(u => u.Gcom)
                                        .HasMaxLength(70);

            modelBuilder.Entity<LeadCRM>().Property<string>(u => u.Oportunidade)
                                        .HasMaxLength(70);

            modelBuilder.Entity<LeadCRM>().Property<string>(u => u.TipoConta)
                                        .HasMaxLength(40);

            #endregion

        }

    }
}