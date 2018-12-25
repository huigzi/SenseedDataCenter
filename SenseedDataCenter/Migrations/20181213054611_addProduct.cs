using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SenseedDataCenter.Migrations
{
    public partial class addProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AnemometerRecord",
                table: "AnemometerRecord");

            migrationBuilder.RenameTable(
                name: "AnemometerRecord",
                newName: "AnemometerRecords");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "AnemometerRecords",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnemometerRecords",
                table: "AnemometerRecords",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SoftWareRev = table.Column<float>(nullable: false),
                    HardWareRev = table.Column<float>(nullable: false),
                    Locate = table.Column<string>(nullable: true),
                    TypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnemometerRecords_ProductId",
                table: "AnemometerRecords",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TypeId",
                table: "Products",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnemometerRecords_Products_ProductId",
                table: "AnemometerRecords",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnemometerRecords_Products_ProductId",
                table: "AnemometerRecords");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnemometerRecords",
                table: "AnemometerRecords");

            migrationBuilder.DropIndex(
                name: "IX_AnemometerRecords_ProductId",
                table: "AnemometerRecords");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "AnemometerRecords");

            migrationBuilder.RenameTable(
                name: "AnemometerRecords",
                newName: "AnemometerRecord");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnemometerRecord",
                table: "AnemometerRecord",
                column: "Id");
        }
    }
}
