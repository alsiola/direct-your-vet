using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DYV.Data.Migrations
{
    public partial class daylist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DayLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    SubscriberUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayLists_AspNetUsers_SubscriberUserId",
                        column: x => x.SubscriberUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlaceDayLists",
                columns: table => new
                {
                    PlaceId = table.Column<int>(nullable: false),
                    DayListId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceDayLists", x => new { x.PlaceId, x.DayListId });
                    table.ForeignKey(
                        name: "FK_PlaceDayLists_DayLists_DayListId",
                        column: x => x.DayListId,
                        principalTable: "DayLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaceDayLists_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlaceDayLists_DayListId",
                table: "PlaceDayLists",
                column: "DayListId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceDayLists_PlaceId",
                table: "PlaceDayLists",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_DayLists_SubscriberUserId",
                table: "DayLists",
                column: "SubscriberUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaceDayLists");

            migrationBuilder.DropTable(
                name: "DayLists");
        }
    }
}
