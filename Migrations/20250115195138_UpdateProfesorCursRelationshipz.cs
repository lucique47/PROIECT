using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AplicatieStudenti.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProfesorCursRelationshipz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inscrieri_Cursuri_CursID",
                table: "Inscrieri");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscrieri_Profesori_ProfesorID",
                table: "Inscrieri");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscrieri_Studenti_StudentID",
                table: "Inscrieri");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfesorCurs_Cursuri_CursuriPredateID",
                table: "ProfesorCurs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfesorCurs_Profesori_ProfesoriID",
                table: "ProfesorCurs");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCurs_Cursuri_CursuriInscriseID",
                table: "StudentCurs");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCurs_Studenti_StudentiInscrisiID",
                table: "StudentCurs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentCurs",
                table: "StudentCurs");

            migrationBuilder.RenameTable(
                name: "StudentCurs",
                newName: "CursStudent");

            migrationBuilder.RenameColumn(
                name: "ProfesoriID",
                table: "ProfesorCurs",
                newName: "ProfesorId");

            migrationBuilder.RenameColumn(
                name: "CursuriPredateID",
                table: "ProfesorCurs",
                newName: "CursId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfesorCurs_ProfesoriID",
                table: "ProfesorCurs",
                newName: "IX_ProfesorCurs_ProfesorId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentCurs_StudentiInscrisiID",
                table: "CursStudent",
                newName: "IX_CursStudent_StudentiInscrisiID");

            migrationBuilder.AddColumn<string>(
                name: "Prenume",
                table: "Inscrieri",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CursStudent",
                table: "CursStudent",
                columns: new[] { "CursuriInscriseID", "StudentiInscrisiID" });

            migrationBuilder.AddForeignKey(
                name: "FK_CursStudent_Cursuri_CursuriInscriseID",
                table: "CursStudent",
                column: "CursuriInscriseID",
                principalTable: "Cursuri",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CursStudent_Studenti_StudentiInscrisiID",
                table: "CursStudent",
                column: "StudentiInscrisiID",
                principalTable: "Studenti",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscrieri_Cursuri_CursID",
                table: "Inscrieri",
                column: "CursID",
                principalTable: "Cursuri",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscrieri_Profesori_ProfesorID",
                table: "Inscrieri",
                column: "ProfesorID",
                principalTable: "Profesori",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscrieri_Studenti_StudentID",
                table: "Inscrieri",
                column: "StudentID",
                principalTable: "Studenti",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfesorCurs_Cursuri_CursId",
                table: "ProfesorCurs",
                column: "CursId",
                principalTable: "Cursuri",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfesorCurs_Profesori_ProfesorId",
                table: "ProfesorCurs",
                column: "ProfesorId",
                principalTable: "Profesori",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CursStudent_Cursuri_CursuriInscriseID",
                table: "CursStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_CursStudent_Studenti_StudentiInscrisiID",
                table: "CursStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscrieri_Cursuri_CursID",
                table: "Inscrieri");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscrieri_Profesori_ProfesorID",
                table: "Inscrieri");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscrieri_Studenti_StudentID",
                table: "Inscrieri");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfesorCurs_Cursuri_CursId",
                table: "ProfesorCurs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfesorCurs_Profesori_ProfesorId",
                table: "ProfesorCurs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CursStudent",
                table: "CursStudent");

            migrationBuilder.DropColumn(
                name: "Prenume",
                table: "Inscrieri");

            migrationBuilder.RenameTable(
                name: "CursStudent",
                newName: "StudentCurs");

            migrationBuilder.RenameColumn(
                name: "ProfesorId",
                table: "ProfesorCurs",
                newName: "ProfesoriID");

            migrationBuilder.RenameColumn(
                name: "CursId",
                table: "ProfesorCurs",
                newName: "CursuriPredateID");

            migrationBuilder.RenameIndex(
                name: "IX_ProfesorCurs_ProfesorId",
                table: "ProfesorCurs",
                newName: "IX_ProfesorCurs_ProfesoriID");

            migrationBuilder.RenameIndex(
                name: "IX_CursStudent_StudentiInscrisiID",
                table: "StudentCurs",
                newName: "IX_StudentCurs_StudentiInscrisiID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentCurs",
                table: "StudentCurs",
                columns: new[] { "CursuriInscriseID", "StudentiInscrisiID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Inscrieri_Cursuri_CursID",
                table: "Inscrieri",
                column: "CursID",
                principalTable: "Cursuri",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscrieri_Profesori_ProfesorID",
                table: "Inscrieri",
                column: "ProfesorID",
                principalTable: "Profesori",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscrieri_Studenti_StudentID",
                table: "Inscrieri",
                column: "StudentID",
                principalTable: "Studenti",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfesorCurs_Cursuri_CursuriPredateID",
                table: "ProfesorCurs",
                column: "CursuriPredateID",
                principalTable: "Cursuri",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfesorCurs_Profesori_ProfesoriID",
                table: "ProfesorCurs",
                column: "ProfesoriID",
                principalTable: "Profesori",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCurs_Cursuri_CursuriInscriseID",
                table: "StudentCurs",
                column: "CursuriInscriseID",
                principalTable: "Cursuri",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCurs_Studenti_StudentiInscrisiID",
                table: "StudentCurs",
                column: "StudentiInscrisiID",
                principalTable: "Studenti",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
