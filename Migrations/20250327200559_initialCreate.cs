using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tedarix.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Atolyes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atolyes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cinss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cinss", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoyAndColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoyId = table.Column<int>(type: "int", nullable: false),
                    Renk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adet = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoyAndColors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kesims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kesims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Foys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    AtolyeId = table.Column<int>(type: "int", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifierTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArsivlendiMi = table.Column<bool>(type: "bit", nullable: false),
                    SevkeHazir = table.Column<bool>(type: "bit", nullable: true),
                    SevkEdildi = table.Column<bool>(type: "bit", nullable: true),
                    Tamamlandi = table.Column<bool>(type: "bit", nullable: true),
                    SevkTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foys_Atolyes_AtolyeId",
                        column: x => x.AtolyeId,
                        principalTable: "Atolyes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Foys_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoyAndCinsS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoyId = table.Column<int>(type: "int", nullable: false),
                    CinsId = table.Column<int>(type: "int", nullable: false),
                    RenkAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoyAndCinsS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoyAndCinsS_Cinss_CinsId",
                        column: x => x.CinsId,
                        principalTable: "Cinss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoyAndCinsS_Foys_FoyId",
                        column: x => x.FoyId,
                        principalTable: "Foys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoyAndKesims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoyId = table.Column<int>(type: "int", nullable: false),
                    KesimId = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoyAndKesims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoyAndKesims_Foys_FoyId",
                        column: x => x.FoyId,
                        principalTable: "Foys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoyAndKesims_Kesims_KesimId",
                        column: x => x.KesimId,
                        principalTable: "Kesims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoyAndCinsS_CinsId",
                table: "FoyAndCinsS",
                column: "CinsId");

            migrationBuilder.CreateIndex(
                name: "IX_FoyAndCinsS_FoyId",
                table: "FoyAndCinsS",
                column: "FoyId");

            migrationBuilder.CreateIndex(
                name: "IX_FoyAndKesims_FoyId",
                table: "FoyAndKesims",
                column: "FoyId");

            migrationBuilder.CreateIndex(
                name: "IX_FoyAndKesims_KesimId",
                table: "FoyAndKesims",
                column: "KesimId");

            migrationBuilder.CreateIndex(
                name: "IX_Foys_AtolyeId",
                table: "Foys",
                column: "AtolyeId");

            migrationBuilder.CreateIndex(
                name: "IX_Foys_TenantId",
                table: "Foys",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ages");

            migrationBuilder.DropTable(
                name: "FoyAndCinsS");

            migrationBuilder.DropTable(
                name: "FoyAndColors");

            migrationBuilder.DropTable(
                name: "FoyAndKesims");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Cinss");

            migrationBuilder.DropTable(
                name: "Foys");

            migrationBuilder.DropTable(
                name: "Kesims");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Atolyes");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
