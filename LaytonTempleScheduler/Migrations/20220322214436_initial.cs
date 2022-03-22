using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LaytonTempleScheduler.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table => new
                {
                    Start = table.Column<DateTime>(nullable: false),
                    Available = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.Start);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TimeSlotStart = table.Column<DateTime>(nullable: false),
                    NameOfGroup = table.Column<string>(nullable: false),
                    GroupSize = table.Column<int>(nullable: false),
                    emailAddress = table.Column<string>(nullable: false),
                    phoneNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentID);
                    table.ForeignKey(
                        name: "FK_Appointments_TimeSlots_TimeSlotStart",
                        column: x => x.TimeSlotStart,
                        principalTable: "TimeSlots",
                        principalColumn: "Start",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_TimeSlotStart",
                table: "Appointments",
                column: "TimeSlotStart");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "TimeSlots");
        }
    }
}
