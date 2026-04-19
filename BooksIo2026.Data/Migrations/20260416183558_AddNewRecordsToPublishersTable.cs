using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksIo2026.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewRecordsToPublishersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Publishers (Name, Country, FoundedDate, Email, IsActive) values ('The Planet', 'USA', GETDATE(), NULL, 1)");
            migrationBuilder.Sql("Insert into Publishers (Name, Country, FoundedDate, Email, IsActive) values ('Saturn', 'England', GETDATE(), NULL, 1)");
            migrationBuilder.Sql("Insert into Publishers (Name, Country, FoundedDate, Email, IsActive) values ('Mars Attack', 'Mexico', GETDATE(), NULL, 1)");
            migrationBuilder.Sql("Insert into Publishers (Name, Country, FoundedDate, Email, IsActive) values ('Mercury', 'Spain', GETDATE(), NULL, 1)");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Publishers");

        }
    }
}
