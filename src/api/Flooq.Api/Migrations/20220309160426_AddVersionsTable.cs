using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
  public partial class AddVersionsTable : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Versions",
          columns: table => new
          {
            Tag = table.Column<string>(type: "text", nullable: false),
            Name = table.Column<string>(type: "text", nullable: false),
            Notes = table.Column<string>(type: "text", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Versions", x => x.Tag);
          });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Versions");
    }
  }
}
