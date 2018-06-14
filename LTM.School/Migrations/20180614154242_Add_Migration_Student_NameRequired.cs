using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LTM.School.Migrations
{
    public partial class Add_Migration_Student_NameRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RealName",
                table: "Student",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 8,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RealName",
                table: "Student",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 8);
        }
    }
}
