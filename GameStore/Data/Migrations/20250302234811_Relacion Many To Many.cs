using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class RelacionManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingCartVideoGame");

            migrationBuilder.DropTable(
                name: "VideoGameShoppingCarts");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Order_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderVideoGame",
                columns: table => new
                {
                    OrdersOrderId = table.Column<int>(type: "int", nullable: false),
                    videoGamesVideoGameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderVideoGame", x => new { x.OrdersOrderId, x.videoGamesVideoGameId });
                    table.ForeignKey(
                        name: "FK_OrderVideoGame_Order_OrdersOrderId",
                        column: x => x.OrdersOrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderVideoGame_VideoGames_videoGamesVideoGameId",
                        column: x => x.videoGamesVideoGameId,
                        principalTable: "VideoGames",
                        principalColumn: "VideoGameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoGameOrder",
                columns: table => new
                {
                    VideoGameOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    VideoGameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoGameOrder", x => x.VideoGameOrderId);
                    table.ForeignKey(
                        name: "FK_VideoGameOrder_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoGameOrder_VideoGames_VideoGameId",
                        column: x => x.VideoGameId,
                        principalTable: "VideoGames",
                        principalColumn: "VideoGameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId1",
                table: "Order",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderVideoGame_videoGamesVideoGameId",
                table: "OrderVideoGame",
                column: "videoGamesVideoGameId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoGameOrder_OrderId",
                table: "VideoGameOrder",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoGameOrder_VideoGameId",
                table: "VideoGameOrder",
                column: "VideoGameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderVideoGame");

            migrationBuilder.DropTable(
                name: "VideoGameOrder");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.ShoppingCartId);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartVideoGame",
                columns: table => new
                {
                    ShoppingCartsShoppingCartId = table.Column<int>(type: "int", nullable: false),
                    VideoGamesVideoGameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartVideoGame", x => new { x.ShoppingCartsShoppingCartId, x.VideoGamesVideoGameId });
                    table.ForeignKey(
                        name: "FK_ShoppingCartVideoGame_ShoppingCarts_ShoppingCartsShoppingCartId",
                        column: x => x.ShoppingCartsShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "ShoppingCartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCartVideoGame_VideoGames_VideoGamesVideoGameId",
                        column: x => x.VideoGamesVideoGameId,
                        principalTable: "VideoGames",
                        principalColumn: "VideoGameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoGameShoppingCarts",
                columns: table => new
                {
                    VideoGameShoppingCartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false),
                    VideoGameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoGameShoppingCarts", x => x.VideoGameShoppingCartId);
                    table.ForeignKey(
                        name: "FK_VideoGameShoppingCarts_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "ShoppingCartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoGameShoppingCarts_VideoGames_VideoGameId",
                        column: x => x.VideoGameId,
                        principalTable: "VideoGames",
                        principalColumn: "VideoGameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartVideoGame_VideoGamesVideoGameId",
                table: "ShoppingCartVideoGame",
                column: "VideoGamesVideoGameId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoGameShoppingCarts_ShoppingCartId",
                table: "VideoGameShoppingCarts",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoGameShoppingCarts_VideoGameId",
                table: "VideoGameShoppingCarts",
                column: "VideoGameId");
        }
    }
}
