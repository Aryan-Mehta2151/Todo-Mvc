using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Final_1.Migrations
{
    /// <inheritdoc />
    public partial class abcd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Task",
                table: "MainLead");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Task",
                table: "MainLead",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
