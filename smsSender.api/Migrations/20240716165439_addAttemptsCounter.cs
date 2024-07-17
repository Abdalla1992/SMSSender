using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace smsSender.api.Migrations
{
    public partial class addAttemptsCounter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_smsLog_Provider_ProviderId",
                table: "smsLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_smsLog",
                table: "smsLog");

            migrationBuilder.RenameTable(
                name: "smsLog",
                newName: "SMSLog");

            migrationBuilder.RenameIndex(
                name: "IX_smsLog_ProviderId",
                table: "SMSLog",
                newName: "IX_SMSLog_ProviderId");

            migrationBuilder.AlterColumn<string>(
                name: "Receiver",
                table: "SMSLog",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "SMSLog",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AttemptsCounter",
                table: "SMSLog",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Provider",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ApiUrl",
                table: "Provider",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SMSLog",
                table: "SMSLog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SMSLog_Provider_ProviderId",
                table: "SMSLog",
                column: "ProviderId",
                principalTable: "Provider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SMSLog_Provider_ProviderId",
                table: "SMSLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SMSLog",
                table: "SMSLog");

            migrationBuilder.DropColumn(
                name: "AttemptsCounter",
                table: "SMSLog");

            migrationBuilder.RenameTable(
                name: "SMSLog",
                newName: "smsLog");

            migrationBuilder.RenameIndex(
                name: "IX_SMSLog_ProviderId",
                table: "smsLog",
                newName: "IX_smsLog_ProviderId");

            migrationBuilder.AlterColumn<string>(
                name: "Receiver",
                table: "smsLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "smsLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Provider",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApiUrl",
                table: "Provider",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_smsLog",
                table: "smsLog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_smsLog_Provider_ProviderId",
                table: "smsLog",
                column: "ProviderId",
                principalTable: "Provider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
