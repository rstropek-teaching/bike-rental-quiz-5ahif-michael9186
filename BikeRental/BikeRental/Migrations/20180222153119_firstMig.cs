using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BikeRental.Migrations
{
    public partial class firstMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bikes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BikeCategory = table.Column<int>(nullable: false),
                    DateOfLastService = table.Column<DateTime>(nullable: true),
                    Notes = table.Column<string>(maxLength: 1000, nullable: true),
                    PurchaseDate = table.Column<DateTime>(nullable: false),
                    RentalPriceAdditionalHour = table.Column<double>(nullable: false),
                    RentalPriceFirstHour = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bikes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Birthday = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    HouseNumber = table.Column<string>(maxLength: 10, nullable: true),
                    LastName = table.Column<string>(maxLength: 75, nullable: false),
                    Town = table.Column<string>(maxLength: 75, nullable: false),
                    ZipCode = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BikeID = table.Column<int>(nullable: false),
                    CustomerID = table.Column<int>(nullable: false),
                    Paid = table.Column<bool>(nullable: false),
                    RentalBegin = table.Column<DateTime>(nullable: false),
                    RentalCosts = table.Column<double>(nullable: false),
                    RentalEnd = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Rentals_Bikes_BikeID",
                        column: x => x.BikeID,
                        principalTable: "Bikes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rentals_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_BikeID",
                table: "Rentals",
                column: "BikeID");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_CustomerID",
                table: "Rentals",
                column: "CustomerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "Bikes");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
