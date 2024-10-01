using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace form_builder.Migrations
{
    /// <inheritdoc />
    public partial class addLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "FormAppearances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "FormAppearances");
        }
    }
}
