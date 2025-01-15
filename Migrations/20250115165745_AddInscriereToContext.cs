using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AplicatieStudenti.Migrations
{
    /// <inheritdoc />
    public partial class AddInscriereToContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inscriere");

            migrationBuilder.AlterColumn<string>(
                name: "NumeCurs",
                table: "Cursuri",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descriere",
                table: "Cursuri",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Inscrieri",
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
                    table.PrimaryKey("PK_Inscrieri", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Inscrieri_Cursuri_CursID",
                        column: x => x.CursID,
                        principalTable: "Cursuri",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscrieri_Profesori_ProfesorID",
                        column: x => x.ProfesorID,
                        principalTable: "Profesori",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscrieri_Studenti_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Studenti",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inscrieri_CursID",
                table: "Inscrieri",
                column: "CursID");

            migrationBuilder.CreateIndex(
                name: "IX_Inscrieri_ProfesorID",
                table: "Inscrieri",
                column: "ProfesorID");

            migrationBuilder.CreateIndex(
                name: "IX_Inscrieri_StudentID",
                table: "Inscrieri",
                column: "StudentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inscrieri");

            migrationBuilder.AlterColumn<string>(
                name: "NumeCurs",
                table: "Cursuri",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descriere",
                table: "Cursuri",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Inscriere",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CursID = table.Column<int>(type: "int", nullable: false),
                    ProfesorID = table.Column<int>(type: "int", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: false)
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
        }
    }
}
