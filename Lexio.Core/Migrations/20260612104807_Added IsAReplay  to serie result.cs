using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lexio.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsAReplaytoserieresult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_a_replay",
                table: "series_results",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_a_replay",
                table: "series_results");
        }
    }
}
