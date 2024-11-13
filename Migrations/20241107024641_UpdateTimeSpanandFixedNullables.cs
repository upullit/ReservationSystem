using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTimeSpanandFixedNullables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Tables_TableId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.RenameColumn(
                name: "TableId",
                table: "Reservations",
                newName: "RestaurantTableId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_TableId",
                table: "Reservations",
                newName: "IX_Reservations_RestaurantTableId");

            migrationBuilder.RenameColumn(
                name: "Contact",
                table: "Guests",
                newName: "Phone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Sittings",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Sittings",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<string>(
                name: "SpecialRequests",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RestaurantTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantTables", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_RestaurantTables_RestaurantTableId",
                table: "Reservations",
                column: "RestaurantTableId",
                principalTable: "RestaurantTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_RestaurantTables_RestaurantTableId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "RestaurantTables");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Guests");

            migrationBuilder.RenameColumn(
                name: "RestaurantTableId",
                table: "Reservations",
                newName: "TableId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_RestaurantTableId",
                table: "Reservations",
                newName: "IX_Reservations_TableId");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Guests",
                newName: "Contact");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                table: "Sittings",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTime",
                table: "Sittings",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "SpecialRequests",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Tables_TableId",
                table: "Reservations",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
