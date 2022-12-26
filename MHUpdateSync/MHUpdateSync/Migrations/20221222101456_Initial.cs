using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MHUpdateSync.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SyncDataArchive",
                table: "SyncDataArchive");

            migrationBuilder.RenameTable(
                name: "SyncDataArchive",
                newName: "SyncDataArchived");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SyncDataArchived",
                table: "SyncDataArchived",
                column: "SyncId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SyncDataArchived",
                table: "SyncDataArchived");

            migrationBuilder.RenameTable(
                name: "SyncDataArchived",
                newName: "SyncDataArchive");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SyncDataArchive",
                table: "SyncDataArchive",
                column: "SyncId");
        }
    }
}
