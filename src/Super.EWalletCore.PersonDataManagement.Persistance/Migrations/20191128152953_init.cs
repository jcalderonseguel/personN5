using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Super.EWalletCore.PersonDataManagement.Persistance.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleType = table.Column<int>(nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentificationDocumentType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdType = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: false),
                    CheckDigit = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentificationDocumentType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryIdentificationDocumentType",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonNumber = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonNumber);
                    table.ForeignKey(
                        name: "FK_RolePerson",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(nullable: false),
                    Region = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    PostCode = table.Column<string>(nullable: false),
                    StreetName = table.Column<string>(nullable: true),
                    BuildingNumber = table.Column<string>(nullable: true),
                    AddressLine = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    AddressType = table.Column<int>(nullable: false),
                    PostOfficeBoxCode = table.Column<string>(nullable: true),
                    POBoxPostalCode = table.Column<string>(nullable: true),
                    StatusCodeAddress = table.Column<int>(nullable: true),
                    COName = table.Column<string>(nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime", nullable: true),
                    PersonPersonNumber = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonAddressData",
                        column: x => x.PersonPersonNumber,
                        principalTable: "Persons",
                        principalColumn: "PersonNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Email",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailAddress = table.Column<string>(nullable: true),
                    Validated = table.Column<bool>(nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime", nullable: true),
                    PersonNumber = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Email", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonEmail",
                        column: x => x.PersonNumber,
                        principalTable: "Persons",
                        principalColumn: "PersonNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentificationDocument",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentNumber = table.Column<string>(nullable: false),
                    IssuingDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IssuingAuthority = table.Column<string>(nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime", nullable: true),
                    PersonNumber = table.Column<long>(nullable: false),
                    IdentificationDocumentTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentificationDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentificationDocumentTypeIdentificationDocument",
                        column: x => x.IdentificationDocumentTypeId,
                        principalTable: "IdentificationDocumentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonIdentificationDocument",
                        column: x => x.PersonNumber,
                        principalTable: "Persons",
                        principalColumn: "PersonNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LegalPerson",
                columns: table => new
                {
                    PersonNumber = table.Column<long>(nullable: false),
                    FullName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalPerson", x => x.PersonNumber);
                    table.ForeignKey(
                        name: "FK_LegalPerson_inherits_Person",
                        column: x => x.PersonNumber,
                        principalTable: "Persons",
                        principalColumn: "PersonNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NaturalPerson",
                columns: table => new
                {
                    PersonNumber = table.Column<long>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastNamePrefix = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateOfDeath = table.Column<DateTime>(type: "datetime", nullable: true),
                    MaritalStatus = table.Column<int>(nullable: true),
                    Nationality = table.Column<int>(nullable: true),
                    GenderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaturalPerson", x => x.PersonNumber);
                    table.ForeignKey(
                        name: "FK_GenderNaturalPerson",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NaturalPerson_inherits_Person",
                        column: x => x.PersonNumber,
                        principalTable: "Persons",
                        principalColumn: "PersonNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phone",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryCode = table.Column<string>(nullable: false),
                    AreaCode = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Extension = table.Column<string>(nullable: true),
                    PhoneType = table.Column<int>(nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime", nullable: true),
                    PersonNumber = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonPhone",
                        column: x => x.PersonNumber,
                        principalTable: "Persons",
                        principalColumn: "PersonNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    OwnerKey = table.Column<string>(nullable: false),
                    FileSize = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    EncodedKey = table.Column<string>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    IdentificationDocumentId = table.Column<long>(nullable: true),
                    PersonNumber = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentificationDocumentAttachment",
                        column: x => x.IdentificationDocumentId,
                        principalTable: "IdentificationDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonAttachment",
                        column: x => x.PersonNumber,
                        principalTable: "Persons",
                        principalColumn: "PersonNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Income",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    Currency = table.Column<int>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    Periodicity = table.Column<int>(nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime", nullable: true),
                    PersonNumber = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Income", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NaturalPersonIncome",
                        column: x => x.PersonNumber,
                        principalTable: "NaturalPerson",
                        principalColumn: "PersonNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FK_PersonAddressData",
                table: "Address",
                column: "PersonPersonNumber");

            migrationBuilder.CreateIndex(
                name: "IX_FK_IdentificationDocumentAttachment",
                table: "Attachment",
                column: "IdentificationDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_PersonAttachment",
                table: "Attachment",
                column: "PersonNumber");

            migrationBuilder.CreateIndex(
                name: "IX_FK_PersonEmail",
                table: "Email",
                column: "PersonNumber");

            migrationBuilder.CreateIndex(
                name: "IX_FK_IdentificationDocumentTypeIdentificationDocument",
                table: "IdentificationDocument",
                column: "IdentificationDocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_PersonIdentificationDocument",
                table: "IdentificationDocument",
                column: "PersonNumber");

            migrationBuilder.CreateIndex(
                name: "IX_FK_CountryIdentificationDocumentType",
                table: "IdentificationDocumentType",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_NaturalPersonIncome",
                table: "Income",
                column: "PersonNumber");

            migrationBuilder.CreateIndex(
                name: "IX_FK_GenderNaturalPerson",
                table: "NaturalPerson",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_RolePerson",
                table: "Persons",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_PersonPhone",
                table: "Phone",
                column: "PersonNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "Email");

            migrationBuilder.DropTable(
                name: "Income");

            migrationBuilder.DropTable(
                name: "LegalPerson");

            migrationBuilder.DropTable(
                name: "Phone");

            migrationBuilder.DropTable(
                name: "IdentificationDocument");

            migrationBuilder.DropTable(
                name: "NaturalPerson");

            migrationBuilder.DropTable(
                name: "IdentificationDocumentType");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
