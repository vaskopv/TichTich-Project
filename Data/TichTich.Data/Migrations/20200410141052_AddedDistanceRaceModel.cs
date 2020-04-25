namespace TichTich.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedDistanceRaceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Distances_Races_RaceId",
                table: "Distances");

            migrationBuilder.DropIndex(
                name: "IX_Distances_RaceId",
                table: "Distances");

            migrationBuilder.DropColumn(
                name: "RaceId",
                table: "Distances");

            migrationBuilder.CreateTable(
                name: "DistanceRace",
                columns: table => new
                {
                    DistanceId = table.Column<int>(nullable: false),
                    RaceId = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistanceRace", x => new { x.DistanceId, x.RaceId });
                    table.ForeignKey(
                        name: "FK_DistanceRace_Distances_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Distances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistanceRace_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DistanceRace_RaceId",
                table: "DistanceRace",
                column: "RaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistanceRace");

            migrationBuilder.AddColumn<int>(
                name: "RaceId",
                table: "Distances",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Distances_RaceId",
                table: "Distances",
                column: "RaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Distances_Races_RaceId",
                table: "Distances",
                column: "RaceId",
                principalTable: "Races",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
