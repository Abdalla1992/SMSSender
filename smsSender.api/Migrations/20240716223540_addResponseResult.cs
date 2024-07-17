using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace smsSender.api.Migrations
{
    public partial class addResponseResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResponseResult",
                table: "SMSLog",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponseResult",
                table: "SMSLog");
        }
    }
}
