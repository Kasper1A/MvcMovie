using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApi.Migrations
{
    /// <inheritdoc />
    public partial class addSalong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visnings_Movies_MovieId",
                table: "Visnings");

            migrationBuilder.DropIndex(
                name: "IX_Visnings_MovieId",
                table: "Visnings");

            migrationBuilder.DropColumn(
                name: "MaxSeats",
                table: "Visnings");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Visnings");

            migrationBuilder.DropColumn(
                name: "ReservedSeats",
                table: "Visnings");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Visnings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Reservs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VisningId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservs_Visnings_VisningId",
                        column: x => x.VisningId,
                        principalTable: "Visnings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Salongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salongs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservs_VisningId",
                table: "Reservs",
                column: "VisningId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservs");

            migrationBuilder.DropTable(
                name: "Salongs");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Visnings");

            migrationBuilder.AddColumn<int>(
                name: "MaxSeats",
                table: "Visnings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Visnings_MovieId",
                table: "Visnings",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visnings_Movies_MovieId",
                table: "Visnings",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
