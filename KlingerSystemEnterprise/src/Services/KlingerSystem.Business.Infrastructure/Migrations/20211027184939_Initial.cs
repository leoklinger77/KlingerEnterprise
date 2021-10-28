using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KlingerSystem.Business.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Cnae",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Devision = table.Column<string>(type: "varchar(7)", nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Cnae", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_Company",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatrizId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CnaeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "varchar(255)", nullable: true),
                    FantasyName = table.Column<string>(type: "varchar(255)", nullable: true),
                    Cpf = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true),
                    Cnpj = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: true),
                    MunicipalRegistration = table.Column<string>(type: "varchar(255)", nullable: true),
                    StateRegistration = table.Column<string>(type: "varchar(255)", nullable: true),
                    Site = table.Column<string>(type: "varchar(255)", nullable: true),
                    CompanyType = table.Column<int>(type: "int", nullable: false),
                    PersonType = table.Column<int>(type: "int", nullable: false),
                    TaxRegime = table.Column<int>(type: "int", nullable: false),
                    SpecialTaxRegime = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_Company_TB_Cnae_CnaeId",
                        column: x => x.CnaeId,
                        principalTable: "TB_Cnae",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_Company_TB_Company_MatrizId",
                        column: x => x.MatrizId,
                        principalTable: "TB_Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZipCode = table.Column<string>(type: "varchar(8)", nullable: false),
                    Street = table.Column<string>(type: "varchar(255)", nullable: false),
                    Number = table.Column<string>(type: "varchar(50)", nullable: false),
                    Complement = table.Column<string>(type: "varchar(255)", nullable: true),
                    Reference = table.Column<string>(type: "varchar(255)", nullable: true),
                    Neighborhood = table.Column<string>(type: "varchar(255)", nullable: true),
                    City = table.Column<string>(type: "varchar(255)", nullable: false),
                    State = table.Column<string>(type: "char(2)", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_Address_TB_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "TB_Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Email",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressEmail = table.Column<string>(type: "varchar(255)", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Email", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_Email_TB_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "TB_Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Phone",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ddd = table.Column<string>(type: "char(2)", nullable: false),
                    Number = table.Column<string>(type: "varchar(9)", nullable: false),
                    PhoneType = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Phone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_Phone_TB_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "TB_Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Address_CompanyId",
                table: "TB_Address",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_Company_CnaeId",
                table: "TB_Company",
                column: "CnaeId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Company_MatrizId",
                table: "TB_Company",
                column: "MatrizId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Email_CompanyId",
                table: "TB_Email",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_Phone_CompanyId",
                table: "TB_Phone",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_Address");

            migrationBuilder.DropTable(
                name: "TB_Email");

            migrationBuilder.DropTable(
                name: "TB_Phone");

            migrationBuilder.DropTable(
                name: "TB_Company");

            migrationBuilder.DropTable(
                name: "TB_Cnae");
        }
    }
}
