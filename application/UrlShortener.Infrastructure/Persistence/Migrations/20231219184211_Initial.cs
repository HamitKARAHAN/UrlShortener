using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
#pragma warning disable CA1062 // Validate arguments of public methods
        migrationBuilder.EnsureSchema(
            name: "dbo");
#pragma warning restore CA1062 // Validate arguments of public methods

        migrationBuilder.CreateTable(
            name: "tags",
            schema: "dbo",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                IsPublic = table.Column<bool>(type: "bit", nullable: false),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                Description_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Ip_Type = table.Column<int>(type: "int", nullable: false),
                Ip_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LongUrl_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ShortUrl_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_tags", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "tagdetails",
            schema: "dbo",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                TagId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                ClickedCount = table.Column<long>(type: "bigint", nullable: false),
                LastCallTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_tagdetails", x => x.Id);
                table.ForeignKey(
                    name: "FK_tagdetails_tags_TagId",
                    column: x => x.TagId,
                    principalSchema: "dbo",
                    principalTable: "tags",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_tagdetails_TagId",
            schema: "dbo",
            table: "tagdetails",
            column: "TagId",
            unique: true,
            filter: "[TagId] IS NOT NULL");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
#pragma warning disable CA1062 // Validate arguments of public methods
        migrationBuilder.DropTable(
            name: "tagdetails",
            schema: "dbo");
#pragma warning restore CA1062 // Validate arguments of public methods

        migrationBuilder.DropTable(
            name: "tags",
            schema: "dbo");
    }
}
