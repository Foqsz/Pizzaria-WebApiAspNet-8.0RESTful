using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzaria_WebApiAspNet_8._0RESTful.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaPizzaId",
                table: "Pizza",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PizzaCategoriaId",
                table: "Pizza",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pizza_CategoriaPizzaId",
                table: "Pizza",
                column: "CategoriaPizzaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizza_CategoriaPizza_CategoriaPizzaId",
                table: "Pizza",
                column: "CategoriaPizzaId",
                principalTable: "CategoriaPizza",
                principalColumn: "PizzaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizza_CategoriaPizza_CategoriaPizzaId",
                table: "Pizza");

            migrationBuilder.DropIndex(
                name: "IX_Pizza_CategoriaPizzaId",
                table: "Pizza");

            migrationBuilder.DropColumn(
                name: "CategoriaPizzaId",
                table: "Pizza");

            migrationBuilder.DropColumn(
                name: "PizzaCategoriaId",
                table: "Pizza");
        }
    }
}
