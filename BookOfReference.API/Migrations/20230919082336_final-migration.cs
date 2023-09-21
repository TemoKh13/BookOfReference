using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookOfReference.API.Migrations
{
    public partial class finalmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatedPeople_PersonWithRelation_PersonWithRelationId",
                table: "RelatedPeople");

            migrationBuilder.DropTable(
                name: "PersonWithRelation");

            migrationBuilder.DropIndex(
                name: "IX_RelatedPeople_PersonWithRelationId",
                table: "RelatedPeople");

            migrationBuilder.DropColumn(
                name: "PersonWithRelationId",
                table: "RelatedPeople");

            migrationBuilder.DropColumn(
                name: "RelatedPersonId",
                table: "RelatedPeople");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonWithRelationId",
                table: "RelatedPeople",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RelatedPersonId",
                table: "RelatedPeople",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PersonWithRelation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfNumber = table.Column<int>(type: "int", nullable: false),
                    ZipCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonWithRelation", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RelatedPeople_PersonWithRelationId",
                table: "RelatedPeople",
                column: "PersonWithRelationId");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedPeople_PersonWithRelation_PersonWithRelationId",
                table: "RelatedPeople",
                column: "PersonWithRelationId",
                principalTable: "PersonWithRelation",
                principalColumn: "Id");
        }
    }
}
