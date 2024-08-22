using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzaria_WebApiAspNet_8._0RESTful.Migrations
{
    /// <inheritdoc />
    public partial class Atts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CategoriaPizza",
                newName: "PizzaCategoria");

            migrationBuilder.AddColumn<int>(
                name: "PizzaEstoque",
                table: "CategoriaPizza",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PizzaEstoque",
                table: "CategoriaPizza");

            migrationBuilder.RenameColumn(
                name: "PizzaCategoria",
                table: "CategoriaPizza",
                newName: "Name");
        }
    }
}
