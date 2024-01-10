// <copyright file="20240110130255_Initial.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
        migrationBuilder.EnsureSchema(
            name: "dbo");

        migrationBuilder.CreateTable(
            name: "tags",
            schema: "dbo",
            columns: table => new
            {
                id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                scheme = table.Column<int>(type: "int", nullable: false),
                host = table.Column<string>(type: "nvarchar(max)", nullable: false),
                short_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ip_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ip_address_type = table.Column<int>(type: "int", nullable: false),
                is_public = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                creation_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                last_modify_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                deleted_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_tags", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "tagdetails",
            schema: "dbo",
            columns: table => new
            {
                id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                TagId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                creation_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                click_count = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                last_access_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                last_modify_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                deleted_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_tagdetails", x => x.id);
                table.ForeignKey(
                    name: "FK_tagdetails_tags_TagId",
                    column: x => x.TagId,
                    principalSchema: "dbo",
                    principalTable: "tags",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_tagdetails_TagId",
            schema: "dbo",
            table: "tagdetails",
            column: "TagId",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "tagdetails",
            schema: "dbo");

        migrationBuilder.DropTable(
            name: "tags",
            schema: "dbo");
    }
}
