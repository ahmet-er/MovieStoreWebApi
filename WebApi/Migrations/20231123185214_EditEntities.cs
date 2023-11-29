using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class EditEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Customers_CustomerId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_PurchasingCustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Movies_PurchasedMovieId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Movies_CustomerId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "PurchasingCustomerId",
                table: "Orders",
                newName: "MovieId");

            migrationBuilder.RenameColumn(
                name: "PurchasedMovieId",
                table: "Orders",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_PurchasingCustomerId",
                table: "Orders",
                newName: "IX_Orders_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_PurchasedMovieId",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Movies_MovieId",
                table: "Orders",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Movies_MovieId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "Orders",
                newName: "PurchasingCustomerId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Orders",
                newName: "PurchasedMovieId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_MovieId",
                table: "Orders",
                newName: "IX_Orders_PurchasingCustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                newName: "IX_Orders_PurchasedMovieId");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_CustomerId",
                table: "Movies",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Customers_CustomerId",
                table: "Movies",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_PurchasingCustomerId",
                table: "Orders",
                column: "PurchasingCustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Movies_PurchasedMovieId",
                table: "Orders",
                column: "PurchasedMovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
