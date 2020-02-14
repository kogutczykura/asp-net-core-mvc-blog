using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Data.Migrations
{
    public partial class PostAddedImageUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_FK_Posts_AspNetUsers_CreatedById",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_FK_Posts_AspNetUsers_CreatedById",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "FK_Posts_AspNetUsers_CreatedById",
                table: "Posts");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Posts",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Posts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CreatedById",
                table: "Posts",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_CreatedById",
                table: "Posts",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_CreatedById",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_CreatedById",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Posts");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FK_Posts_AspNetUsers_CreatedById",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_FK_Posts_AspNetUsers_CreatedById",
                table: "Posts",
                column: "FK_Posts_AspNetUsers_CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_FK_Posts_AspNetUsers_CreatedById",
                table: "Posts",
                column: "FK_Posts_AspNetUsers_CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
