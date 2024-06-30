using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DivingCompetitionAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    CompetitionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.CompetitionId);
                });

            migrationBuilder.CreateTable(
                name: "Divers",
                columns: table => new
                {
                    DiverId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Divers", x => x.DiverId);
                });

            migrationBuilder.CreateTable(
                name: "Dives",
                columns: table => new
                {
                    DiveId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiveCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Difficulty = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dives", x => x.DiveId);
                });

            migrationBuilder.CreateTable(
                name: "Judges",
                columns: table => new
                {
                    JudgeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Judges", x => x.JudgeId);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionDivers",
                columns: table => new
                {
                    CompetitionId = table.Column<int>(type: "int", nullable: false),
                    DiverId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionDivers", x => new { x.CompetitionId, x.DiverId });
                    table.ForeignKey(
                        name: "FK_CompetitionDivers_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "CompetitionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetitionDivers_Divers_DiverId",
                        column: x => x.DiverId,
                        principalTable: "Divers",
                        principalColumn: "DiverId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiverDives",
                columns: table => new
                {
                    DiverId = table.Column<int>(type: "int", nullable: false),
                    DiveId = table.Column<int>(type: "int", nullable: false),
                    CompetitionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiverDives", x => new { x.DiverId, x.DiveId, x.CompetitionId });
                    table.ForeignKey(
                        name: "FK_DiverDives_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "CompetitionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiverDives_Divers_DiverId",
                        column: x => x.DiverId,
                        principalTable: "Divers",
                        principalColumn: "DiverId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiverDives_Dives_DiveId",
                        column: x => x.DiveId,
                        principalTable: "Dives",
                        principalColumn: "DiveId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionJudges",
                columns: table => new
                {
                    CompetitionId = table.Column<int>(type: "int", nullable: false),
                    JudgeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionJudges", x => new { x.CompetitionId, x.JudgeId });
                    table.ForeignKey(
                        name: "FK_CompetitionJudges_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "CompetitionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetitionJudges_Judges_JudgeId",
                        column: x => x.JudgeId,
                        principalTable: "Judges",
                        principalColumn: "JudgeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    ScoreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Points = table.Column<double>(type: "float", nullable: false),
                    DiverDiveDiverId = table.Column<int>(type: "int", nullable: false),
                    DiverDiveDiveId = table.Column<int>(type: "int", nullable: false),
                    DiverDiveCompetitionId = table.Column<int>(type: "int", nullable: false),
                    JudgeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.ScoreId);
                    table.ForeignKey(
                        name: "FK_Scores_DiverDives_DiverDiveDiverId_DiverDiveDiveId_DiverDiveCompetitionId",
                        columns: x => new { x.DiverDiveDiverId, x.DiverDiveDiveId, x.DiverDiveCompetitionId },
                        principalTable: "DiverDives",
                        principalColumns: new[] { "DiverId", "DiveId", "CompetitionId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Scores_Judges_JudgeId",
                        column: x => x.JudgeId,
                        principalTable: "Judges",
                        principalColumn: "JudgeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionDivers_DiverId",
                table: "CompetitionDivers",
                column: "DiverId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionJudges_JudgeId",
                table: "CompetitionJudges",
                column: "JudgeId");

            migrationBuilder.CreateIndex(
                name: "IX_DiverDives_CompetitionId",
                table: "DiverDives",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_DiverDives_DiveId",
                table: "DiverDives",
                column: "DiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_DiverDiveDiverId_DiverDiveDiveId_DiverDiveCompetitionId",
                table: "Scores",
                columns: new[] { "DiverDiveDiverId", "DiverDiveDiveId", "DiverDiveCompetitionId" });

            migrationBuilder.CreateIndex(
                name: "IX_Scores_JudgeId",
                table: "Scores",
                column: "JudgeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompetitionDivers");

            migrationBuilder.DropTable(
                name: "CompetitionJudges");

            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "DiverDives");

            migrationBuilder.DropTable(
                name: "Judges");

            migrationBuilder.DropTable(
                name: "Competitions");

            migrationBuilder.DropTable(
                name: "Divers");

            migrationBuilder.DropTable(
                name: "Dives");
        }
    }
}
