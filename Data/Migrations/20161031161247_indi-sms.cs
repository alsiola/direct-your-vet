using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DYV.Data.Migrations
{
    public partial class indisms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SMSSendResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Error = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    RemainingSMS = table.Column<int>(nullable: false),
                    SMSGroupSendResultId = table.Column<int>(nullable: false),
                    Success = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSSendResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SMSSendResult_SMSGroupSendResults_SMSGroupSendResultId",
                        column: x => x.SMSGroupSendResultId,
                        principalTable: "SMSGroupSendResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SMSSendResult_SMSGroupSendResultId",
                table: "SMSSendResult",
                column: "SMSGroupSendResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SMSSendResult");
        }
    }
}
