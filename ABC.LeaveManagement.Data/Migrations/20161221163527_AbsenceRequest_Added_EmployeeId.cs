using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ABC.LeaveManagement.Data.Migrations
{
    public partial class AbsenceRequest_Added_EmployeeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbsenceRequest_Employee_EmployeeId",
                table: "AbsenceRequest");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "AbsenceRequest",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AbsenceRequest_Employee_EmployeeId",
                table: "AbsenceRequest",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbsenceRequest_Employee_EmployeeId",
                table: "AbsenceRequest");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "AbsenceRequest",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_AbsenceRequest_Employee_EmployeeId",
                table: "AbsenceRequest",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
