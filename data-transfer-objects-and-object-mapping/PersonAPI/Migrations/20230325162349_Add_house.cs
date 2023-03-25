using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonAPI.Migrations
{
    /// <inheritdoc />
    public partial class Add_house : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "House",
                table: "People",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "House",
                table: "People");
        }
    }
}
