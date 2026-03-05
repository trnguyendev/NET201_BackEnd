using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductSizeRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "ProductSizes");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ProductSizes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizes_CategoryId",
                table: "ProductSizes",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizes_Categories_CategoryId",
                table: "ProductSizes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizes_Categories_CategoryId",
                table: "ProductSizes");

            migrationBuilder.DropIndex(
                name: "IX_ProductSizes_CategoryId",
                table: "ProductSizes");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ProductSizes");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "ProductSizes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
