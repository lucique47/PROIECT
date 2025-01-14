using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AplicatieStudenti.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cursuri",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeCurs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descriere = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursuri", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Profesori",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specializare = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesori", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Studenti",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNasterii = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studenti", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProfesorCurs",
                columns: table => new
                {
                    CursuriPredateID = table.Column<int>(type: "int", nullable: false),
                    ProfesoriID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfesorCurs", x => new { x.CursuriPredateID, x.ProfesoriID });
                    table.ForeignKey(
                        name: "FK_ProfesorCurs_Cursuri_CursuriPredateID",
                        column: x => x.CursuriPredateID,
                        principalTable: "Cursuri",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfesorCurs_Profesori_ProfesoriID",
                        column: x => x.ProfesoriID,
                        principalTable: "Profesori",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inscriere",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    CursID = table.Column<int>(type: "int", nullable: false),
                    ProfesorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscriere", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Inscriere_Cursuri_CursID",
                        column: x => x.CursID,
                        principalTable: "Cursuri",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscriere_Profesori_ProfesorID",
                        column: x => x.ProfesorID,
                        principalTable: "Profesori",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscriere_Studenti_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Studenti",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCurs",
                columns: table => new
                {
                    CursuriInscriseID = table.Column<int>(type: "int", nullable: false),
                    StudentiInscrisiID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCurs", x => new { x.CursuriInscriseID, x.StudentiInscrisiID });
                    table.ForeignKey(
                        name: "FK_StudentCurs_Cursuri_CursuriInscriseID",
                        column: x => x.CursuriInscriseID,
                        principalTable: "Cursuri",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCurs_Studenti_StudentiInscrisiID",
                        column: x => x.StudentiInscrisiID,
                        principalTable: "Studenti",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inscriere_CursID",
                table: "Inscriere",
                column: "CursID");

            migrationBuilder.CreateIndex(
                name: "IX_Inscriere_ProfesorID",
                table: "Inscriere",
                column: "ProfesorID");

            migrationBuilder.CreateIndex(
                name: "IX_Inscriere_StudentID",
                table: "Inscriere",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfesorCurs_ProfesoriID",
                table: "ProfesorCurs",
                column: "ProfesoriID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCurs_StudentiInscrisiID",
                table: "StudentCurs",
                column: "StudentiInscrisiID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inscriere");

            migrationBuilder.DropTable(
                name: "ProfesorCurs");

            migrationBuilder.DropTable(
                name: "StudentCurs");

            migrationBuilder.DropTable(
                name: "Profesori");

            migrationBuilder.DropTable(
                name: "Cursuri");

            migrationBuilder.DropTable(
                name: "Studenti");
        }
    }
}
