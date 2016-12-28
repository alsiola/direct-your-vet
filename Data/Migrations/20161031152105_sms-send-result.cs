using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DYV.Data.Migrations
{
    public partial class smssendresult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SMSGroupSendResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PracticeId = table.Column<int>(nullable: false),
                    TotalExisting = table.Column<int>(nullable: false),
                    TotalFailed = table.Column<int>(nullable: false),
                    TotalRequested = table.Column<int>(nullable: false),
                    TotalSucceeded = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSGroupSendResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SMSGroupSendResults_Practices_PracticeId",
                        column: x => x.PracticeId,
                        principalTable: "Practices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SMSGroupSendResults_PracticeId",
                table: "SMSGroupSendResults",
                column: "PracticeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SMSGroupSendResults");
        }
    }
}
