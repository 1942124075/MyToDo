using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyToDo.Library.Migrations
{
    /// <inheritdoc />
    public partial class addToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PassWord",
                table: "T_Users",
                newName: "Password");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "T_Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifyDate",
                table: "T_ToDos",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 14, 1, 38, 38, 187, DateTimeKind.Local).AddTicks(1564),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 10, 15, 23, 20, 788, DateTimeKind.Local).AddTicks(6922));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "T_ToDos",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 14, 1, 38, 38, 187, DateTimeKind.Local).AddTicks(1359),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 10, 15, 23, 20, 788, DateTimeKind.Local).AddTicks(6690));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifyDate",
                table: "T_Memos",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 14, 1, 38, 38, 186, DateTimeKind.Local).AddTicks(3713),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 10, 15, 23, 20, 787, DateTimeKind.Local).AddTicks(9070));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "T_Memos",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 14, 1, 38, 38, 186, DateTimeKind.Local).AddTicks(3461),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 10, 15, 23, 20, 787, DateTimeKind.Local).AddTicks(8789));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "T_Users");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "T_Users",
                newName: "PassWord");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifyDate",
                table: "T_ToDos",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 10, 15, 23, 20, 788, DateTimeKind.Local).AddTicks(6922),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 14, 1, 38, 38, 187, DateTimeKind.Local).AddTicks(1564));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "T_ToDos",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 10, 15, 23, 20, 788, DateTimeKind.Local).AddTicks(6690),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 14, 1, 38, 38, 187, DateTimeKind.Local).AddTicks(1359));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifyDate",
                table: "T_Memos",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 10, 15, 23, 20, 787, DateTimeKind.Local).AddTicks(9070),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 14, 1, 38, 38, 186, DateTimeKind.Local).AddTicks(3713));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "T_Memos",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 10, 15, 23, 20, 787, DateTimeKind.Local).AddTicks(8789),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 14, 1, 38, 38, 186, DateTimeKind.Local).AddTicks(3461));
        }
    }
}
