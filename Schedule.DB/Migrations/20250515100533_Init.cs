using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Schedule.DB.Migrations {
	/// <inheritdoc />
	public partial class Init : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.CreateTable(
				name: "Bells",
				columns: table => new {
					Id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					Start = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
					End = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Bells", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Grades",
				columns: table => new {
					Id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					Year = table.Column<int>(type: "integer", nullable: false),
					Description = table.Column<string>(type: "text", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Grades", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "SchoolClasses",
				columns: table => new {
					Id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					Letter = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
					GradeId = table.Column<int>(type: "integer", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_SchoolClasses", x => x.Id);
					table.ForeignKey(
						name: "FK_SchoolClasses_Grades_GradeId",
						column: x => x.GradeId,
						principalTable: "Grades",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Days",
				columns: table => new {
					Id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					SchoolClassId = table.Column<int>(type: "integer", nullable: false),
					DayOfWeek = table.Column<int>(type: "integer", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Days", x => x.Id);
					table.ForeignKey(
						name: "FK_Days_SchoolClasses_SchoolClassId",
						column: x => x.SchoolClassId,
						principalTable: "SchoolClasses",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Lessons",
				columns: table => new {
					Id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					Name = table.Column<string>(type: "text", nullable: false),
					Difficulty = table.Column<int>(type: "integer", nullable: false),
					HoursPerWeek = table.Column<int>(type: "integer", nullable: false),
					HoursPerDay = table.Column<int>(type: "integer", nullable: false),
					DayId = table.Column<int>(type: "integer", nullable: true)
				},
				constraints: table => {
					table.PrimaryKey("PK_Subjects", x => x.Id);
					table.ForeignKey(
						name: "FK_Subjects_Days_DayId",
						column: x => x.DayId,
						principalTable: "Days",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "Lessons",
				columns: table => new {
					Id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					SubjectId = table.Column<int>(type: "integer", nullable: false),
					BellId = table.Column<int>(type: "integer", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Lessons", x => x.Id);
					table.ForeignKey(
						name: "FK_Lessons_Bells_BellId",
						column: x => x.BellId,
						principalTable: "Bells",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Lessons_Subjects_SubjectId",
						column: x => x.SubjectId,
						principalTable: "Lessons",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Days_SchoolClassId",
				table: "Days",
				column: "SchoolClassId");

			migrationBuilder.CreateIndex(
				name: "IX_Lessons_BellId",
				table: "Lessons",
				column: "BellId");

			migrationBuilder.CreateIndex(
				name: "IX_Lessons_SubjectId",
				table: "Lessons",
				column: "SubjectId");

			migrationBuilder.CreateIndex(
				name: "IX_SchoolClasses_GradeId",
				table: "SchoolClasses",
				column: "GradeId");

			migrationBuilder.CreateIndex(
				name: "IX_Subjects_DayId",
				table: "Lessons",
				column: "DayId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(
				name: "Lessons");

			migrationBuilder.DropTable(
				name: "Bells");

			migrationBuilder.DropTable(
				name: "Lessons");

			migrationBuilder.DropTable(
				name: "Days");

			migrationBuilder.DropTable(
				name: "SchoolClasses");

			migrationBuilder.DropTable(
				name: "Grades");
		}
	}
}
