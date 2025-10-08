using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenStock.Migrations
{
    /// <inheritdoc />
    public partial class greenstockprub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleItemModel_Products_ProductId",
                table: "SaleItemModel");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleItemModel_Sales_SaleId",
                table: "SaleItemModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_RoleModel_RoleId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleItemModel",
                table: "SaleItemModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleModel",
                table: "RoleModel");

            migrationBuilder.RenameTable(
                name: "SaleItemModel",
                newName: "SaleItems");

            migrationBuilder.RenameTable(
                name: "RoleModel",
                newName: "Roles");

            migrationBuilder.RenameIndex(
                name: "IX_SaleItemModel_SaleId",
                table: "SaleItems",
                newName: "IX_SaleItems_SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleItemModel_ProductId",
                table: "SaleItems",
                newName: "IX_SaleItems_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleItems",
                table: "SaleItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleItems_Products_ProductId",
                table: "SaleItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleItems_Sales_SaleId",
                table: "SaleItems",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleItems_Products_ProductId",
                table: "SaleItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleItems_Sales_SaleId",
                table: "SaleItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleItems",
                table: "SaleItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "SaleItems",
                newName: "SaleItemModel");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "RoleModel");

            migrationBuilder.RenameIndex(
                name: "IX_SaleItems_SaleId",
                table: "SaleItemModel",
                newName: "IX_SaleItemModel_SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleItems_ProductId",
                table: "SaleItemModel",
                newName: "IX_SaleItemModel_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleItemModel",
                table: "SaleItemModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleModel",
                table: "RoleModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleItemModel_Products_ProductId",
                table: "SaleItemModel",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleItemModel_Sales_SaleId",
                table: "SaleItemModel",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_RoleModel_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "RoleModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
