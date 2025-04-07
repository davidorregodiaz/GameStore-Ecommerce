using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class RelationUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VideoGameOrder",
                table: "VideoGameOrder");

            migrationBuilder.DropIndex(
                name: "IX_VideoGameOrder_VideoGameId",
                table: "VideoGameOrder");

            migrationBuilder.DropColumn(
                name: "VideoGameOrderId",
                table: "VideoGameOrder");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideoGameOrder",
                table: "VideoGameOrder",
                columns: new[] { "VideoGameId", "OrderId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VideoGameOrder",
                table: "VideoGameOrder");

            migrationBuilder.AddColumn<int>(
                name: "VideoGameOrderId",
                table: "VideoGameOrder",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideoGameOrder",
                table: "VideoGameOrder",
                column: "VideoGameOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoGameOrder_VideoGameId",
                table: "VideoGameOrder",
                column: "VideoGameId");
        }
    }
}
