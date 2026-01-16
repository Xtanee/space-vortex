using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Content.Server.Database.Migrations.Postgres
{
	/// <inheritdoc />
    public partial class oocnotesfix : Migration
    {	
		/// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "oocnotes",
                table: "profile",
                type: "TEXT",
                nullable: false,
                defaultValue: ""
            );
        }

		/// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "oocnotes",
                table: "profile"
            );
        }
    }
}
