using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Data.Migrations
{
    public partial class PostUserRelation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_AspNetUsers",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_AspNetUsers",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AspNetUsers",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "AspNetUsers",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AspNetUsers",
                table: "Posts",
                column: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_AspNetUsers",
                table: "Posts",
                column: "AspNetUsers",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
