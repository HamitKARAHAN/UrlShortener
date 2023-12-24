using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            migrationBuilder.DropForeignKey(
                name: "FK_tagdetails_tags_TagId",
                schema: "dbo",
                table: "tagdetails");
#pragma warning restore CA1062 // Validate arguments of public methods

            migrationBuilder.DropIndex(
                name: "IX_tagdetails_TagId",
                schema: "dbo",
                table: "tagdetails");

            migrationBuilder.AlterColumn<string>(
                name: "TagId",
                schema: "dbo",
                table: "tagdetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tagdetails_TagId",
                schema: "dbo",
                table: "tagdetails",
                column: "TagId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tagdetails_tags_TagId",
                schema: "dbo",
                table: "tagdetails",
                column: "TagId",
                principalSchema: "dbo",
                principalTable: "tags",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            migrationBuilder.DropForeignKey(
                name: "FK_tagdetails_tags_TagId",
                schema: "dbo",
                table: "tagdetails");
#pragma warning restore CA1062 // Validate arguments of public methods

            migrationBuilder.DropIndex(
                name: "IX_tagdetails_TagId",
                schema: "dbo",
                table: "tagdetails");

            migrationBuilder.AlterColumn<string>(
                name: "TagId",
                schema: "dbo",
                table: "tagdetails",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_tagdetails_TagId",
                schema: "dbo",
                table: "tagdetails",
                column: "TagId",
                unique: true,
                filter: "[TagId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_tagdetails_tags_TagId",
                schema: "dbo",
                table: "tagdetails",
                column: "TagId",
                principalSchema: "dbo",
                principalTable: "tags",
                principalColumn: "id");
        }
    }
}
