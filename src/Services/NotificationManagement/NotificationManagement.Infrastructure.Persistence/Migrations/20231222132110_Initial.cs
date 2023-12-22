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
                    NotificationTypeId = table.Column<int>(type: "int", nullable: false),
                    NotificationPriorityId = table.Column<int>(type: "int", nullable: false),
                    NotificationStatusId = table.Column<int>(type: "int", nullable: false),
                    Read = table.Column<bool>(type: "bit", nullable: false),
                    RetryCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationPriorities_NotificationPriorityId",
                        column: x => x.NotificationPriorityId,
                        principalSchema: "NotificationManagement",
                        principalTable: "NotificationPriorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationStatuses_NotificationStatusId",
                        column: x => x.NotificationStatusId,
                        principalSchema: "NotificationManagement",
                        principalTable: "NotificationStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationTypes_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalSchema: "NotificationManagement",
                        principalTable: "NotificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationAcitvities",
                schema: "NotificationManagement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationId = table.Column<int>(type: "int", nullable: false),
                    NotificationStatusId = table.Column<int>(type: "int", nullable: false),
                    NotificationId1 = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationAcitvities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationAcitvities_NotificationStatuses_NotificationStatusId",
                        column: x => x.NotificationStatusId,
                        principalSchema: "NotificationManagement",
                        principalTable: "NotificationStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationAcitvities_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalSchema: "NotificationManagement",
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationAcitvities_Notifications_NotificationId1",
                        column: x => x.NotificationId1,
                        principalSchema: "NotificationManagement",
                        principalTable: "Notifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationAcitvities_NotificationId",
                schema: "NotificationManagement",
                table: "NotificationAcitvities",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationAcitvities_NotificationId1",
                schema: "NotificationManagement",
                table: "NotificationAcitvities",
                column: "NotificationId1");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationAcitvities_NotificationStatusId",
                schema: "NotificationManagement",
                table: "NotificationAcitvities",
                column: "NotificationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationPriorityId",
                schema: "NotificationManagement",
                table: "Notifications",
                column: "NotificationPriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationStatusId",
                schema: "NotificationManagement",
                table: "Notifications",
                column: "NotificationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationTypeId",
                schema: "NotificationManagement",
                table: "Notifications",
                column: "NotificationTypeId");
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
