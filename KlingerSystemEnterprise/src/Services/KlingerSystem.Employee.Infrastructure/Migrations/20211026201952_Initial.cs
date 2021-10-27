using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KlingerSystem.Employee.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "varchar(250)", nullable: false),
                    Cpf = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true),
                    Rg_Number = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    RG_ExpeditionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Rg_Issuer = table.Column<int>(type: "int", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImagePath = table.Column<string>(type: "varchar(255)", nullable: true),
                    Commission = table.Column<double>(type: "float", nullable: true),
                    Annotation = table.Column<string>(type: "varchar(255)", nullable: true),
                    EmployeeType = table.Column<int>(type: "int", nullable: false),
                    TypeSexo = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_TB_Address_TB_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "TB_Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Email",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressEmail = table.Column<string>(type: "varchar(255)", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Email", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_Email_TB_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "TB_Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Phone",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_TB_Phone_TB_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "TB_Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Address_EmployeeId",
                table: "TB_Address",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_Email_EmployeeId",
                table: "TB_Email",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_Phone_EmployeeId",
                table: "TB_Phone",
                column: "EmployeeId");
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
                name: "TB_Employee");
        }
    }
}
