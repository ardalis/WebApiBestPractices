using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendData.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PluralsightUrl = table.Column<string>(type: "TEXT", nullable: true),
                    TwitterAlias = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name", "PluralsightUrl", "TwitterAlias" },
                values: new object[] { 1, "Steve Smith", "https://www.pluralsight.com/authors/steve-smith", "ardalis" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name", "PluralsightUrl", "TwitterAlias" },
                values: new object[] { 2, "Julie Lerman", "https://www.pluralsight.com/authors/julie-lerman", "julialerman" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
