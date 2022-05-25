using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flooq.Identity.Migrations
{
    public partial class AddRoles : Migration
    {
        private static readonly Guid FreeRoleId = Guid.NewGuid();
        private static readonly Guid PersonalRoleId = Guid.NewGuid();
        private static readonly Guid StartupRoleId = Guid.NewGuid();
        private static readonly Guid MaintainerRoleId = Guid.NewGuid();

        private const string FreeRoleName = "Free";
        private const string PersonalRoleName = "Personal";
        private const string StartupRoleName = "Startup";
        private const string MaintainerRoleName = "Maintainer";
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { FreeRoleId.ToString(), FreeRoleName, FreeRoleName.ToUpper(), Guid.NewGuid().ToString() });
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { PersonalRoleId.ToString(), PersonalRoleName, PersonalRoleName.ToUpper(), Guid.NewGuid().ToString() });
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { StartupRoleId.ToString(), StartupRoleName, StartupRoleName.ToUpper(), Guid.NewGuid().ToString() });
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { MaintainerRoleId.ToString(), MaintainerRoleName, MaintainerRoleName.ToUpper(), Guid.NewGuid().ToString() });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: FreeRoleId.ToString());
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: PersonalRoleId.ToString());
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: StartupRoleId.ToString());
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: MaintainerRoleId.ToString());
        }
    }
}
