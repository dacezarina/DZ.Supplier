using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DZ.SupplierProcessor.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedBoxTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BoxId",
                table: "Contents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Boxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoxIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contents_BoxId",
                table: "Contents",
                column: "BoxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Boxes_BoxId",
                table: "Contents",
                column: "BoxId",
                principalTable: "Boxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Boxes_BoxId",
                table: "Contents");

            migrationBuilder.DropTable(
                name: "Boxes");

            migrationBuilder.DropIndex(
                name: "IX_Contents_BoxId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "BoxId",
                table: "Contents");
        }
    }
}
