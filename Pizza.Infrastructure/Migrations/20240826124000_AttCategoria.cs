using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzaria_WebApiAspNet_8._0RESTful.Migrations
{
    /// <inheritdoc />
    public partial class AttCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PizzaEstoque",
                table: "CategoriaPizza");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PizzaEstoque",
                table: "CategoriaPizza",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
