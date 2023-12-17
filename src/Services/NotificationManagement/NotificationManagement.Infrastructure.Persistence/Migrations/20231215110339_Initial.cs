using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotificationManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "NotificationManagement");

            migrationBuilder.CreateSequence(
                name: "notificationseq",
                schema: "NotificationManagement",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "NotificationPriorities",
                schema: "NotificationManagement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationPriorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationStatuses",
                schema: "NotificationManagement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTypes",
                schema: "NotificationManagement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                schema: "NotificationManagement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Receiver = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NotificationTypeId1 = table.Column<int>(type: "int", nullable: true),
                    NotificationPriorityId1 = table.Column<int>(type: "int", nullable: true),
                    NotificationStatusId1 = table.Column<int>(type: "int", nullable: true),
                    NotificationPriorityId = table.Column<int>(type: "int", nullable: false),
                    NotificationStatusId = table.Column<int>(type: "int", nullable: false),
                    NotificationTypeId = table.Column<int>(type: "int", nullable: false),
                    Read = table.Column<bool>(type: "bit", nullable: false),
                    RetryCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationPriorities_NotificationPriorityId1",
                        column: x => x.NotificationPriorityId1,
                        principalSchema: "NotificationManagement",
                        principalTable: "NotificationPriorities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationStatuses_NotificationStatusId1",
                        column: x => x.NotificationStatusId1,
                        principalSchema: "NotificationManagement",
                        principalTable: "NotificationStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationTypes_NotificationTypeId1",
                        column: x => x.NotificationTypeId1,
                        principalSchema: "NotificationManagement",
                        principalTable: "NotificationTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NotificationAcitvities",
                schema: "NotificationManagement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationStatusId1 = table.Column<int>(type: "int", nullable: true),
                    NotificationId1 = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NotificationId = table.Column<int>(type: "int", nullable: false),
                    NotificationStatusId = table.Column<int>(type: "int", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationAcitvities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationAcitvities_NotificationStatuses_NotificationStatusId1",
                        column: x => x.NotificationStatusId1,
                        principalSchema: "NotificationManagement",
                        principalTable: "NotificationStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NotificationAcitvities_Notifications_NotificationId1",
                        column: x => x.NotificationId1,
                        principalSchema: "NotificationManagement",
                        principalTable: "Notifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationAcitvities_NotificationId1",
                schema: "NotificationManagement",
                table: "NotificationAcitvities",
                column: "NotificationId1");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationAcitvities_NotificationStatusId1",
                schema: "NotificationManagement",
                table: "NotificationAcitvities",
                column: "NotificationStatusId1");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationPriorityId1",
                schema: "NotificationManagement",
                table: "Notifications",
                column: "NotificationPriorityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationStatusId1",
                schema: "NotificationManagement",
                table: "Notifications",
                column: "NotificationStatusId1");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationTypeId1",
                schema: "NotificationManagement",
                table: "Notifications",
                column: "NotificationTypeId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationAcitvities",
                schema: "NotificationManagement");

            migrationBuilder.DropTable(
                name: "Notifications",
                schema: "NotificationManagement");

            migrationBuilder.DropTable(
                name: "NotificationPriorities",
                schema: "NotificationManagement");

            migrationBuilder.DropTable(
                name: "NotificationStatuses",
                schema: "NotificationManagement");

            migrationBuilder.DropTable(
                name: "NotificationTypes",
                schema: "NotificationManagement");

            migrationBuilder.DropSequence(
                name: "notificationseq",
                schema: "NotificationManagement");
        }
    }
}
