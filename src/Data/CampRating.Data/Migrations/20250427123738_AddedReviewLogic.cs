using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampRating.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedReviewLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CampId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CampId",
                table: "Reviews",
                column: "CampId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Camps_CampId",
                table: "Reviews",
                column: "CampId",
                principalTable: "Camps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Camps_CampId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CampId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CampId",
                table: "Reviews");
        }
    }
}
