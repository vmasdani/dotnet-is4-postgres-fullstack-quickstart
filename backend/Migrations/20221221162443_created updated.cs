using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class createdupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Records",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Events",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "EventDatas",
                newName: "created");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Records",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Records",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Events",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Events",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "EventDatas",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "EventDatas",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "EventDatas");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "EventDatas");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "Records",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "Events",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "EventDatas",
                newName: "Created");
        }
    }
}
