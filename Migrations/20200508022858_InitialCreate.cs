using Microsoft.EntityFrameworkCore.Migrations;

namespace w_list.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ForumModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Locked = table.Column<bool>(nullable: false),
                    IsPublic = table.Column<bool>(nullable: false),
                    AllowedWord1 = table.Column<string>(nullable: true),
                    AllowedWord2 = table.Column<string>(nullable: true),
                    AllowedWord3 = table.Column<string>(nullable: true),
                    AllowedWord4 = table.Column<string>(nullable: true),
                    AllowedWord5 = table.Column<string>(nullable: true),
                    AllowedWord6 = table.Column<string>(nullable: true),
                    AllowedWord7 = table.Column<string>(nullable: true),
                    AllowedWord8 = table.Column<string>(nullable: true),
                    AllowedWord9 = table.Column<string>(nullable: true),
                    AllowedWord10 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForumModel");
        }
    }
}
