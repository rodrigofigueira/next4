using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class AdicaoCamposLeadForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataIntegracao",
                table: "LeadForms",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeTentativas",
                table: "LeadForms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataIntegracao",
                table: "LeadForms");

            migrationBuilder.DropColumn(
                name: "QuantidadeTentativas",
                table: "LeadForms");
        }
    }
}
