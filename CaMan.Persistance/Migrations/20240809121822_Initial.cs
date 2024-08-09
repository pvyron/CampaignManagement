using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaMan.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdministrativeRegion",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    ShortName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdministrativeRegion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Campaign",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    CreatorId = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaign", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Municipality",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    ShortName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipality", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MunicipalUnit",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    ShortName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MunicipalUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegionalUnit",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    ShortName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionalUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CampaignContact",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    CampaignId = table.Column<string>(type: "TEXT", nullable: false),
                    ContactId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampaignContact_Campaign_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ShortName = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber_Phone = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber_RegionalPrefix = table.Column<string>(type: "TEXT", nullable: true),
                    CommunicationMethod = table.Column<int>(type: "INTEGER", nullable: false),
                    AgeGroup = table.Column<int>(type: "INTEGER", nullable: false),
                    AdministrativeRegionId = table.Column<byte[]>(type: "BLOB", nullable: true),
                    RegionalUnitId = table.Column<byte[]>(type: "BLOB", nullable: true),
                    MunicipalityId = table.Column<byte[]>(type: "BLOB", nullable: true),
                    MunicipalUnitId = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_AdministrativeRegion_AdministrativeRegionId",
                        column: x => x.AdministrativeRegionId,
                        principalTable: "AdministrativeRegion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contact_MunicipalUnit_MunicipalUnitId",
                        column: x => x.MunicipalUnitId,
                        principalTable: "MunicipalUnit",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contact_Municipality_MunicipalityId",
                        column: x => x.MunicipalityId,
                        principalTable: "Municipality",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contact_RegionalUnit_RegionalUnitId",
                        column: x => x.RegionalUnitId,
                        principalTable: "RegionalUnit",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ShortName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    ContactInfoId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Contact_ContactInfoId",
                        column: x => x.ContactInfoId,
                        principalTable: "Contact",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampaignContact_CampaignId",
                table: "CampaignContact",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_AdministrativeRegionId",
                table: "Contact",
                column: "AdministrativeRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_MunicipalityId",
                table: "Contact",
                column: "MunicipalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_MunicipalUnitId",
                table: "Contact",
                column: "MunicipalUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_RegionalUnitId",
                table: "Contact",
                column: "RegionalUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ContactInfoId",
                table: "Users",
                column: "ContactInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampaignContact");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Campaign");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "AdministrativeRegion");

            migrationBuilder.DropTable(
                name: "MunicipalUnit");

            migrationBuilder.DropTable(
                name: "Municipality");

            migrationBuilder.DropTable(
                name: "RegionalUnit");
        }
    }
}
