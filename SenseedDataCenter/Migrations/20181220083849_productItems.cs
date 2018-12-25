using Microsoft.EntityFrameworkCore.Migrations;

namespace SenseedDataCenter.Migrations
{
    public partial class productItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "AnemometerRecords",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "AnemometerRecords",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
