// <copyright file="20231217181115_Initial.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
#pragma warning disable CA1062 // Validate arguments of public methods
        migrationBuilder.CreateTable(
            name: "Tags",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ShortUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LongUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IsPublic = table.Column<bool>(type: "bit", nullable: false),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Tags", x => x.Id);
            });
#pragma warning restore CA1062 // Validate arguments of public methods

        migrationBuilder.CreateTable(
            name: "TagDetails",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                TagId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                ClickedCount = table.Column<int>(type: "int", nullable: false),
                LastCallTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TagDetails", x => x.Id);
                table.ForeignKey(
                    name: "FK_TagDetails_Tags_TagId",
                    column: x => x.TagId,
                    principalTable: "Tags",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_TagDetails_TagId",
            table: "TagDetails",
            column: "TagId",
            unique: true,
            filter: "[TagId] IS NOT NULL");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
#pragma warning disable CA1062 // Validate arguments of public methods
        migrationBuilder.DropTable(
            name: "TagDetails");
#pragma warning restore CA1062 // Validate arguments of public methods

        migrationBuilder.DropTable(
            name: "Tags");
    }
}
