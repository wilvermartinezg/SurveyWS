using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyWS.Migrations
{
    public partial class InitialSurveyTemplateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "survey_template",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modified_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_survey_template", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "survey_template_detail",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    field_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    field_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    field_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    survey_template_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_survey_template_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_survey_template_detail_survey_template_survey_template_id",
                        column: x => x.survey_template_id,
                        principalTable: "survey_template",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_survey_template_detail_survey_template_id",
                table: "survey_template_detail",
                column: "survey_template_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "survey_template_detail");

            migrationBuilder.DropTable(
                name: "survey_template");
        }
    }
}
