using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyToDo.Library.Migrations
{
    /// <inheritdoc />
    public partial class addUserKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifyDate",
                table: "T_ToDos",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 23, 15, 28, 16, 222, DateTimeKind.Local).AddTicks(688),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 14, 1, 38, 38, 187, DateTimeKind.Local).AddTicks(1564));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "T_ToDos",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 23, 15, 28, 16, 222, DateTimeKind.Local).AddTicks(484),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 14, 1, 38, 38, 187, DateTimeKind.Local).AddTicks(1359));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "T_ToDos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifyDate",
                table: "T_Memos",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 23, 15, 28, 16, 221, DateTimeKind.Local).AddTicks(1842),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 14, 1, 38, 38, 186, DateTimeKind.Local).AddTicks(3713));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "T_Memos",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 23, 15, 28, 16, 221, DateTimeKind.Local).AddTicks(1483),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 14, 1, 38, 38, 186, DateTimeKind.Local).AddTicks(3461));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "T_Memos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_T_ToDos_UserId",
                table: "T_ToDos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Memos_UserId",
                table: "T_Memos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_T_Memos_T_Users_UserId",
                table: "T_Memos",
                column: "UserId",
                principalTable: "T_Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_ToDos_T_Users_UserId",
                table: "T_ToDos",
                column: "UserId",
                principalTable: "T_Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_Memos_T_Users_UserId",
                table: "T_Memos");

            migrationBuilder.DropForeignKey(
                name: "FK_T_ToDos_T_Users_UserId",
                table: "T_ToDos");

            migrationBuilder.DropIndex(
                name: "IX_T_ToDos_UserId",
                table: "T_ToDos");

            migrationBuilder.DropIndex(
                name: "IX_T_Memos_UserId",
                table: "T_Memos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "T_ToDos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "T_Memos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifyDate",
                table: "T_ToDos",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 14, 1, 38, 38, 187, DateTimeKind.Local).AddTicks(1564),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 23, 15, 28, 16, 222, DateTimeKind.Local).AddTicks(688));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "T_ToDos",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 14, 1, 38, 38, 187, DateTimeKind.Local).AddTicks(1359),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 23, 15, 28, 16, 222, DateTimeKind.Local).AddTicks(484));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifyDate",
                table: "T_Memos",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 14, 1, 38, 38, 186, DateTimeKind.Local).AddTicks(3713),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 23, 15, 28, 16, 221, DateTimeKind.Local).AddTicks(1842));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "T_Memos",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 14, 1, 38, 38, 186, DateTimeKind.Local).AddTicks(3461),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 23, 15, 28, 16, 221, DateTimeKind.Local).AddTicks(1483));
        }
    }
}
