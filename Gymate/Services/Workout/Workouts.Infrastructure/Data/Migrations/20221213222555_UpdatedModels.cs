using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workouts.Infrastructure.Data.Migrations
{
    public partial class UpdatedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Students",
                newName: "UserName");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Workouts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IdentityId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Students",
                newName: "LastName");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Workouts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
