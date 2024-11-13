using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRestaurantTableNumberLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_RestaurantTables_RestaurantTableId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "RestaurantTables");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_RestaurantTableId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "RestaurantTableId",
                table: "Reservations");

            migrationBuilder.AddColumn<string>(
                name: "TableNumber",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TableNumber",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "RestaurantTableId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RestaurantTables",
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
                    table.PrimaryKey("PK_RestaurantTables", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RestaurantTableId",
                table: "Reservations",
                column: "RestaurantTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_RestaurantTables_RestaurantTableId",
                table: "Reservations",
                column: "RestaurantTableId",
                principalTable: "RestaurantTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
