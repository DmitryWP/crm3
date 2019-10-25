using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace crm.db.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "crm");

            migrationBuilder.CreateTable(
                name: "Contact",
                schema: "crm",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Family = table.Column<string>(maxLength: 50, nullable: true),
                    FName = table.Column<string>(maxLength: 50, nullable: true),
                    SName = table.Column<string>(maxLength: 50, nullable: true),
                    Address = table.Column<string>(maxLength: 50, nullable: true),
                    Phone = table.Column<string>(maxLength: 11, nullable: true),
                    Phone2 = table.Column<string>(maxLength: 11, nullable: true),
                    Phone3 = table.Column<string>(maxLength: 11, nullable: true),
                    EMail = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contact_H",
                schema: "crm",
                columns: table => new
                {
                    HistoryId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Family = table.Column<string>(maxLength: 50, nullable: true),
                    FName = table.Column<string>(maxLength: 50, nullable: true),
                    SName = table.Column<string>(maxLength: 50, nullable: true),
                    Address = table.Column<string>(maxLength: 50, nullable: true),
                    Phone = table.Column<string>(maxLength: 11, nullable: true),
                    Phone2 = table.Column<string>(maxLength: 11, nullable: true),
                    Phone3 = table.Column<string>(maxLength: 11, nullable: true),
                    EMail = table.Column<string>(maxLength: 50, nullable: true),
                    Id = table.Column<long>(nullable: false),
                    ChangedUserId = table.Column<long>(nullable: false),
                    Changed = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: true),
                    RowData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact_H", x => x.HistoryId);
                });

            migrationBuilder.CreateTable(
                name: "GardenSociety",
                schema: "crm",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GardenSociety", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GardenSociety_H",
                schema: "crm",
                columns: table => new
                {
                    HistoryId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    Id = table.Column<long>(nullable: false),
                    ChangedUserId = table.Column<long>(nullable: false),
                    Changed = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: true),
                    RowData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GardenSociety_H", x => x.HistoryId);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                schema: "crm",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kind = table.Column<long>(nullable: false),
                    DateFrom = table.Column<DateTime>(nullable: false),
                    DateTo = table.Column<DateTime>(nullable: true),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Street",
                schema: "crm",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GardenSocietyId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Street", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Street_GardenSociety_GardenSocietyId",
                        column: x => x.GardenSocietyId,
                        principalSchema: "crm",
                        principalTable: "GardenSociety",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Plot",
                schema: "crm",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GardenSocietyId = table.Column<long>(nullable: false),
                    PersonalAccount = table.Column<string>(maxLength: 50, nullable: true),
                    Area = table.Column<decimal>(nullable: false),
                    StreetId = table.Column<long>(nullable: false),
                    HouseNumber = table.Column<int>(nullable: false),
                    OwnerId = table.Column<long>(nullable: false),
                    Age = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plot_GardenSociety_GardenSocietyId",
                        column: x => x.GardenSocietyId,
                        principalSchema: "crm",
                        principalTable: "GardenSociety",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Plot_Contact_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "crm",
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Plot_Street_StreetId",
                        column: x => x.StreetId,
                        principalSchema: "crm",
                        principalTable: "Street",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Plot_H",
                schema: "crm",
                columns: table => new
                {
                    HistoryId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GardenSocietyId = table.Column<long>(nullable: false),
                    PersonalAccount = table.Column<string>(maxLength: 50, nullable: true),
                    Area = table.Column<decimal>(nullable: false),
                    StreetId = table.Column<long>(nullable: false),
                    HouseNumber = table.Column<int>(nullable: false),
                    OwnerId = table.Column<long>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    Id = table.Column<long>(nullable: false),
                    ChangedUserId = table.Column<long>(nullable: false),
                    Changed = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: true),
                    RowData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plot_H", x => x.HistoryId);
                    table.ForeignKey(
                        name: "FK_Plot_H_GardenSociety_GardenSocietyId",
                        column: x => x.GardenSocietyId,
                        principalSchema: "crm",
                        principalTable: "GardenSociety",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Plot_H_Contact_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "crm",
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Plot_H_Street_StreetId",
                        column: x => x.StreetId,
                        principalSchema: "crm",
                        principalTable: "Street",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheet",
                schema: "crm",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TSDate = table.Column<DateTime>(nullable: false),
                    PlotId = table.Column<long>(nullable: false),
                    Target = table.Column<long>(nullable: false),
                    CounterReading = table.Column<long>(nullable: false),
                    SettingId = table.Column<long>(nullable: false),
                    Direct = table.Column<int>(nullable: false),
                    Volume = table.Column<long>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSheet_Plot_PlotId",
                        column: x => x.PlotId,
                        principalSchema: "crm",
                        principalTable: "Plot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeSheet_Settings_SettingId",
                        column: x => x.SettingId,
                        principalSchema: "crm",
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheet_H",
                schema: "crm",
                columns: table => new
                {
                    HistoryId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TSDate = table.Column<DateTime>(nullable: false),
                    PlotId = table.Column<long>(nullable: false),
                    Target = table.Column<long>(nullable: false),
                    CounterReading = table.Column<long>(nullable: false),
                    SettingId = table.Column<long>(nullable: false),
                    Direct = table.Column<int>(nullable: false),
                    Volume = table.Column<long>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Id = table.Column<long>(nullable: false),
                    ChangedUserId = table.Column<long>(nullable: false),
                    Changed = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: true),
                    RowData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheet_H", x => x.HistoryId);
                    table.ForeignKey(
                        name: "FK_TimeSheet_H_Plot_PlotId",
                        column: x => x.PlotId,
                        principalSchema: "crm",
                        principalTable: "Plot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeSheet_H_Settings_SettingId",
                        column: x => x.SettingId,
                        principalSchema: "crm",
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plot_GardenSocietyId",
                schema: "crm",
                table: "Plot",
                column: "GardenSocietyId");

            migrationBuilder.CreateIndex(
                name: "IX_Plot_OwnerId",
                schema: "crm",
                table: "Plot",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Plot_StreetId",
                schema: "crm",
                table: "Plot",
                column: "StreetId");

            migrationBuilder.CreateIndex(
                name: "IX_Plot_H_GardenSocietyId",
                schema: "crm",
                table: "Plot_H",
                column: "GardenSocietyId");

            migrationBuilder.CreateIndex(
                name: "IX_Plot_H_OwnerId",
                schema: "crm",
                table: "Plot_H",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Plot_H_StreetId",
                schema: "crm",
                table: "Plot_H",
                column: "StreetId");

            migrationBuilder.CreateIndex(
                name: "IX_Street_GardenSocietyId",
                schema: "crm",
                table: "Street",
                column: "GardenSocietyId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheet_PlotId",
                schema: "crm",
                table: "TimeSheet",
                column: "PlotId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheet_SettingId",
                schema: "crm",
                table: "TimeSheet",
                column: "SettingId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheet_H_PlotId",
                schema: "crm",
                table: "TimeSheet_H",
                column: "PlotId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheet_H_SettingId",
                schema: "crm",
                table: "TimeSheet_H",
                column: "SettingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact_H",
                schema: "crm");

            migrationBuilder.DropTable(
                name: "GardenSociety_H",
                schema: "crm");

            migrationBuilder.DropTable(
                name: "Plot_H",
                schema: "crm");

            migrationBuilder.DropTable(
                name: "TimeSheet",
                schema: "crm");

            migrationBuilder.DropTable(
                name: "TimeSheet_H",
                schema: "crm");

            migrationBuilder.DropTable(
                name: "Plot",
                schema: "crm");

            migrationBuilder.DropTable(
                name: "Settings",
                schema: "crm");

            migrationBuilder.DropTable(
                name: "Contact",
                schema: "crm");

            migrationBuilder.DropTable(
                name: "Street",
                schema: "crm");

            migrationBuilder.DropTable(
                name: "GardenSociety",
                schema: "crm");
        }
    }
}
