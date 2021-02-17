using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthServer.Persistence.Migrations
{
    public partial class RemoveAuditFiledFromUserIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserIdentities");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "UserIdentities");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "UserIdentities");

            migrationBuilder.DropColumn(
                name: "ModifiedTime",
                table: "UserIdentities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "UserIdentities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "UserIdentities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "UserIdentities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedTime",
                table: "UserIdentities",
                type: "datetime2",
                nullable: true);
        }
    }
}
