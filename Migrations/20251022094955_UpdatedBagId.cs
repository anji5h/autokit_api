using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoKitApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedBagId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BoxId",
                table: "Bags",
                newName: "BagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BagId",
                table: "Bags",
                newName: "BoxId");
        }
    }
}
