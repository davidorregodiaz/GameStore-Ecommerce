using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class Notificaitons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order");

            

            migrationBuilder.DropForeignKey(
                name: "FK_UserNotification_AspNetUsers_UserId",
                table: "UserNotification");

            migrationBuilder.DropForeignKey(
                name: "FK_UserNotification_Notification_NotificationId",
                table: "UserNotification");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoGameOrder_Order_OrderId",
                table: "VideoGameOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoGameOrder_VideoGames_VideoGameId",
                table: "VideoGameOrder");

            migrationBuilder.DropTable(
                name: "NotificationUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VideoGameOrder",
                table: "VideoGameOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserNotification",
                table: "UserNotification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notification",
                table: "Notification");

            migrationBuilder.RenameTable(
                name: "VideoGameOrder",
                newName: "VideoGameOrders");

            migrationBuilder.RenameTable(
                name: "UserNotification",
                newName: "UserNotifications");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "Notification",
                newName: "Notifications");

            migrationBuilder.RenameIndex(
                name: "IX_VideoGameOrder_VideoGameId",
                table: "VideoGameOrders",
                newName: "IX_VideoGameOrders_VideoGameId");

            migrationBuilder.RenameIndex(
                name: "IX_VideoGameOrder_OrderId",
                table: "VideoGameOrders",
                newName: "IX_VideoGameOrders_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_UserNotification_UserId",
                table: "UserNotifications",
                newName: "IX_UserNotifications_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserNotification_NotificationId",
                table: "UserNotifications",
                newName: "IX_UserNotifications_NotificationId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_UserId",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideoGameOrders",
                table: "VideoGameOrders",
                column: "VideoGameOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserNotifications",
                table: "UserNotifications",
                column: "UserNotificationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotifications_AspNetUsers_UserId",
                table: "UserNotifications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotifications_Notifications_NotificationId",
                table: "UserNotifications",
                column: "NotificationId",
                principalTable: "Notifications",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoGameOrders_Orders_OrderId",
                table: "VideoGameOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoGameOrders_VideoGames_VideoGameId",
                table: "VideoGameOrders",
                column: "VideoGameId",
                principalTable: "VideoGames",
                principalColumn: "VideoGameId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            

            migrationBuilder.DropForeignKey(
                name: "FK_UserNotifications_AspNetUsers_UserId",
                table: "UserNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_UserNotifications_Notifications_NotificationId",
                table: "UserNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoGameOrders_Orders_OrderId",
                table: "VideoGameOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoGameOrders_VideoGames_VideoGameId",
                table: "VideoGameOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VideoGameOrders",
                table: "VideoGameOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserNotifications",
                table: "UserNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.RenameTable(
                name: "VideoGameOrders",
                newName: "VideoGameOrder");

            migrationBuilder.RenameTable(
                name: "UserNotifications",
                newName: "UserNotification");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "Notification");

            migrationBuilder.RenameIndex(
                name: "IX_VideoGameOrders_VideoGameId",
                table: "VideoGameOrder",
                newName: "IX_VideoGameOrder_VideoGameId");

            migrationBuilder.RenameIndex(
                name: "IX_VideoGameOrders_OrderId",
                table: "VideoGameOrder",
                newName: "IX_VideoGameOrder_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_UserNotifications_UserId",
                table: "UserNotification",
                newName: "IX_UserNotification_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserNotifications_NotificationId",
                table: "UserNotification",
                newName: "IX_UserNotification_NotificationId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Order",
                newName: "IX_Order_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideoGameOrder",
                table: "VideoGameOrder",
                column: "VideoGameOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserNotification",
                table: "UserNotification",
                column: "UserNotificationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notification",
                table: "Notification",
                column: "NotificationId");

            migrationBuilder.CreateTable(
                name: "NotificationUser",
                columns: table => new
                {
                    NotificationsNotificationId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationUser", x => new { x.NotificationsNotificationId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_NotificationUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationUser_Notification_NotificationsNotificationId",
                        column: x => x.NotificationsNotificationId,
                        principalTable: "Notification",
                        principalColumn: "NotificationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationUser_UsersId",
                table: "NotificationUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotification_AspNetUsers_UserId",
                table: "UserNotification",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotification_Notification_NotificationId",
                table: "UserNotification",
                column: "NotificationId",
                principalTable: "Notification",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoGameOrder_Order_OrderId",
                table: "VideoGameOrder",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoGameOrder_VideoGames_VideoGameId",
                table: "VideoGameOrder",
                column: "VideoGameId",
                principalTable: "VideoGames",
                principalColumn: "VideoGameId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
