using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cocktail.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cocktail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cocktail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AlcoholLevel = table.Column<double>(type: "REAL", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Step",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CocktailId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Step", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Step_Cocktail_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktail",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Composition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<double>(type: "REAL", nullable: false),
                    Unit = table.Column<int>(type: "INTEGER", nullable: false),
                    IngredientId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CocktailId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Composition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Composition_Cocktail_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Composition_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Composition_CocktailId",
                table: "Composition",
                column: "CocktailId");

            migrationBuilder.CreateIndex(
                name: "IX_Composition_IngredientId",
                table: "Composition",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Step_CocktailId",
                table: "Step",
                column: "CocktailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Composition");

            migrationBuilder.DropTable(
                name: "Step");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "Cocktail");
        }
    }
}
