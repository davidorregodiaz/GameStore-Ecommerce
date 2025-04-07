using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class StateAcceptedOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "Orders");
        }
    }
}
