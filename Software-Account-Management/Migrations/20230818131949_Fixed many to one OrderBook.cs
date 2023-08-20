using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Software_Account_Management.Migrations
{
    /// <inheritdoc />
    public partial class FixedmanytooneOrderBook : Migration
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
                    AppLicenseId = table.Column<Guid>(type: "char(36)", nullable: true),
                    TestStation = table.Column<string>(type: "longtext", nullable: false),
                    OrderTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ReservationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CompletionTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    ReservedByUser = table.Column<string>(type: "longtext", nullable: false),
                    ReservedForSut = table.Column<string>(type: "longtext", nullable: false),
                    InstanceId = table.Column<string>(type: "longtext", nullable: false),
                    TestStationTaskId = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseOrderBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LicenseOrderBooks_AppLicenses_AppLicenseId",
                        column: x => x.AppLicenseId,
                        principalTable: "AppLicenses",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "licenseQueues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AppLicenseId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_licenseQueues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_licenseQueues_AppLicenses_AppLicenseId",
                        column: x => x.AppLicenseId,
                        principalTable: "AppLicenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_licenseQueues_LicenseOrderBooks_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "LicenseOrderBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseOrderBooks_AppLicenseId",
                table: "LicenseOrderBooks",
                column: "AppLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_licenseQueues_AppLicenseId",
                table: "licenseQueues",
                column: "AppLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_licenseQueues_ReservationId",
                table: "licenseQueues",
                column: "ReservationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "licenseQueues");

            migrationBuilder.DropTable(
                name: "LicenseOrderBooks");

            migrationBuilder.DropTable(
                name: "AppLicenses");
        }
    }
}
