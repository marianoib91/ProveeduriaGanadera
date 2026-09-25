using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProveeduriaGanadera.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCodigoDeBarras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoDeBarras",
                table: "Productos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CodigoDeBarras",
                table: "Productos",
                column: "CodigoDeBarras",
                unique: true,
                filter: "[CodigoDeBarras] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Productos_CodigoDeBarras",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "CodigoDeBarras",
                table: "Productos");
        }
    }
}
