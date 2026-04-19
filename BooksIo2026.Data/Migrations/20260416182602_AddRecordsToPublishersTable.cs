using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksIo2026.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRecordsToPublishersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Publishers (Name) values ('The Planet')");
            migrationBuilder.Sql("Insert into Publishers (Name) values ('Saturn')");
            migrationBuilder.Sql("Insert into Publishers (Name) values ('Mercury')");
            migrationBuilder.Sql("Insert into Publishers (Name) values ('Triton')");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Publishers");

        }
    }
}
