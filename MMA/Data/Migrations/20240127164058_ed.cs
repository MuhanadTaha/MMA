using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMA.Data.Migrations
{
    public partial class ed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discription",
                table: "informationsWeb",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "informationsWeb",
                newName: "Discription");
        }
    }
}
