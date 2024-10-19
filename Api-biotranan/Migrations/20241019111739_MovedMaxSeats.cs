using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApi.Migrations
{
    /// <inheritdoc />
    public partial class MovedMaxSeats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Visnings");

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Visnings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReservedSeats",
                table: "Visnings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalongId",
                table: "Visnings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxSeats",
                table: "Salongs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Visnings_MovieId",
                table: "Visnings",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Visnings_SalongId",
                table: "Visnings",
                column: "SalongId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visnings_Movies_MovieId",
                table: "Visnings",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visnings_Salongs_SalongId",
                table: "Visnings",
                column: "SalongId",
                principalTable: "Salongs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visnings_Movies_MovieId",
                table: "Visnings");

            migrationBuilder.DropForeignKey(
                name: "FK_Visnings_Salongs_SalongId",
                table: "Visnings");

            migrationBuilder.DropIndex(
                name: "IX_Visnings_MovieId",
                table: "Visnings");

            migrationBuilder.DropIndex(
                name: "IX_Visnings_SalongId",
                table: "Visnings");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Visnings");

            migrationBuilder.DropColumn(
                name: "ReservedSeats",
                table: "Visnings");

            migrationBuilder.DropColumn(
                name: "SalongId",
                table: "Visnings");

            migrationBuilder.DropColumn(
                name: "MaxSeats",
                table: "Salongs");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Visnings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
