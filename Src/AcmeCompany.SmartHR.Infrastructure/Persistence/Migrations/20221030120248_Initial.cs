using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcmeCompany.SmartHR.Infrastructure.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "HumanResources");

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "HumanResources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FullName = table.Column<string>(type: "varchar(350)", unicode: false, maxLength: 350, nullable: false),
                    Email = table.Column<string>(type: "varchar(350)", unicode: false, maxLength: 350, nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobTitle = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees",
                schema: "HumanResources");
        }
    }
}
