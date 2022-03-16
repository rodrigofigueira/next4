using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HashSecurityAPIs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HashSecurity = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Restriction = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HashSecurityAPIs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeadForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Sobrenome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Empresa = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    CNPJ = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TelefoneContato = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    PilarNegocio = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    QuantidadeEquipamentos = table.Column<int>(type: "int", nullable: false),
                    VolumeImpressao = table.Column<int>(type: "int", nullable: false),
                    Mensagem = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "LeadCRMs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountIdCrm = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StatusLead = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    EncLead = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    FilialAtendimento = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    GC = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    EmpresaCRM = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    ChaveCRM = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Vertical = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    DtRecLeadGc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FeedbackAtend = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Funcionarios = table.Column<int>(type: "int", nullable: false),
                    Filiais = table.Column<int>(type: "int", nullable: false),
                    Faturamento = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    StatusContaCRM = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    RazaoStatus = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ModalidadeTucunare = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    ProbabilidadeFechamento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DtEstimativaFechamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReceitaMensalEstimada = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ReceitaTucunare = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    PrazoContratoEstimado = table.Column<int>(type: "int", nullable: false),
                    TotalContrato = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    PilarOpp = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    NumeroProjetoTucunare = table.Column<int>(type: "int", nullable: false),
                    NumeroPropostaTucunare = table.Column<int>(type: "int", nullable: false),
                    ListaTucunare = table.Column<int>(type: "int", nullable: false),
                    DtFechamentoNegocio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DtPerdaNegocio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DtCriacaoOpp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstagioProcesso = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    DescricaoProjeto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GerenteConta = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    UnidadeNegocios = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Gcom = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Oportunidade = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    TipoConta = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    LeadFormId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadCRMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadCRMs_LeadForms_LeadFormId",
                        column: x => x.LeadFormId,
                        principalTable: "LeadForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeadRDs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventType = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    EventFamily = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    ConversionIdentifier = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ClientTrackingId = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    TrafficSource = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    TrafficMedium = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    TrafficCampaign = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    TrafficValue = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    LeadFormId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadRDs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadRDs_LeadForms_LeadFormId",
                        column: x => x.LeadFormId,
                        principalTable: "LeadForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeadCRMs_LeadFormId",
                table: "LeadCRMs",
                column: "LeadFormId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadRDs_LeadFormId",
                table: "LeadRDs",
                column: "LeadFormId");

            migrationBuilder.CreateIndex(
                name: "EmailIsUnique",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "NameIsUnique",
                table: "Users",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HashSecurityAPIs");

            migrationBuilder.DropTable(
                name: "LeadCRMs");

            migrationBuilder.DropTable(
                name: "LeadRDs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "LeadForms");
        }
    }
}
