using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DYV.Data.Migrations
{
    public partial class msgtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SMSSendResults");

            migrationBuilder.DropTable(
                name: "SMSGroupSendResults");

            migrationBuilder.CreateTable(
                name: "MessageGroupResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateSent = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Error = table.Column<string>(nullable: true),
                    PracticeId = table.Column<int>(nullable: false),
                    SubscriberUserId = table.Column<string>(nullable: true),
                    TotalExisting = table.Column<int>(nullable: false),
                    TotalFailed = table.Column<int>(nullable: false),
                    TotalRequested = table.Column<int>(nullable: false),
                    TotalSucceeded = table.Column<int>(nullable: false),
                    PracticeId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageGroupResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageGroupResult_Practices_PracticeId",
                        column: x => x.PracticeId,
                        principalTable: "Practices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageGroupResult_AspNetUsers_SubscriberUserId",
                        column: x => x.SubscriberUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageGroupResult_Practices_PracticeId1",
                        column: x => x.PracticeId1,
                        principalTable: "Practices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MessageSendResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    Error = table.Column<string>(nullable: true),
                    RecipientSignedUp = table.Column<bool>(nullable: false),
                    SlugOpened = table.Column<bool>(nullable: false),
                    Success = table.Column<bool>(nullable: false),
                    UniqueSignupSlug = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    GroupSendResultId = table.Column<int>(nullable: true),
                    DragonFlyMessageIdentifier = table.Column<int>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    RemainingSMS = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageSendResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageSendResult_MessageGroupResult_GroupSendResultId",
                        column: x => x.GroupSendResultId,
                        principalTable: "MessageGroupResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageGroupResult_PracticeId",
                table: "MessageGroupResult",
                column: "PracticeId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageGroupResult_SubscriberUserId",
                table: "MessageGroupResult",
                column: "SubscriberUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageGroupResult_PracticeId1",
                table: "MessageGroupResult",
                column: "PracticeId1");

            migrationBuilder.CreateIndex(
                name: "IX_MessageSendResult_GroupSendResultId",
                table: "MessageSendResult",
                column: "GroupSendResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageSendResult");

            migrationBuilder.DropTable(
                name: "MessageGroupResult");

            migrationBuilder.CreateTable(
                name: "SMSGroupSendResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateSent = table.Column<DateTime>(nullable: false),
                    Error = table.Column<string>(nullable: true),
                    PracticeId = table.Column<int>(nullable: false),
                    SubscriberUserId = table.Column<string>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_SMSGroupSendResults_AspNetUsers_SubscriberUserId",
                        column: x => x.SubscriberUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SMSSendResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DragonFlyMessageIdentifier = table.Column<int>(nullable: false),
                    Error = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    RecipientSignedUp = table.Column<bool>(nullable: false),
                    RemainingSMS = table.Column<int>(nullable: false),
                    SMSGroupSendResultId = table.Column<int>(nullable: false),
                    SlugOpened = table.Column<bool>(nullable: false),
                    Success = table.Column<bool>(nullable: false),
                    UniqueSignupSlug = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSSendResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SMSSendResults_SMSGroupSendResults_SMSGroupSendResultId",
                        column: x => x.SMSGroupSendResultId,
                        principalTable: "SMSGroupSendResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SMSGroupSendResults_PracticeId",
                table: "SMSGroupSendResults",
                column: "PracticeId");

            migrationBuilder.CreateIndex(
                name: "IX_SMSGroupSendResults_SubscriberUserId",
                table: "SMSGroupSendResults",
                column: "SubscriberUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SMSSendResults_SMSGroupSendResultId",
                table: "SMSSendResults",
                column: "SMSGroupSendResultId");
        }
    }
}
