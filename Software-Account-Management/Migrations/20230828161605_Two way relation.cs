using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Software_Account_Management.Migrations
{
    /// <inheritdoc />
    public partial class Twowayrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AppLicenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    AppName = table.Column<string>(type: "longtext", nullable: false),
                    AppService = table.Column<string>(type: "longtext", nullable: false),
                    SpaceId = table.Column<int>(type: "int", nullable: false),
                    TestStationPool = table.Column<string>(type: "longtext", nullable: false),
                    LicenseOrderBookId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "longtext", nullable: false),
                    Password = table.Column<string>(type: "longtext", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LicenseStatus = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppLicenses", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LicenseOrderBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TestStationName = table.Column<string>(type: "longtext", nullable: false),
                    TestCaseID = table.Column<string>(type: "longtext", nullable: false),
                    Orchestrator = table.Column<string>(type: "longtext", nullable: false),
                    AppLicenseId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ReservationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EstCompletionTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CompletionTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ReservedByUser = table.Column<string>(type: "longtext", nullable: false),
                    Framework = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseOrderBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LicenseOrderBooks_AppLicenses_AppLicenseId",
                        column: x => x.AppLicenseId,
                        principalTable: "AppLicenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AppLicenses_LicenseOrderBookId",
                table: "AppLicenses",
                column: "LicenseOrderBookId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseOrderBooks_AppLicenseId",
                table: "LicenseOrderBooks",
                column: "AppLicenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppLicenses_LicenseOrderBooks_LicenseOrderBookId",
                table: "AppLicenses",
                column: "LicenseOrderBookId",
                principalTable: "LicenseOrderBooks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppLicenses_LicenseOrderBooks_LicenseOrderBookId",
                table: "AppLicenses");

            migrationBuilder.DropTable(
                name: "LicenseOrderBooks");

            migrationBuilder.DropTable(
                name: "AppLicenses");
        }
    }
}
