﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DYV.Data.Migrations
{
    public partial class smspurchaseusedgroupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberUsed",
                table: "MessagePurchase",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSent",
                table: "SMSGroupSendResults",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberUsed",
                table: "MessagePurchase");

            migrationBuilder.DropColumn(
                name: "DateSent",
                table: "SMSGroupSendResults");
        }
    }
}
