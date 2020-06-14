using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

/*É usado aqui o workflow CODE-FIRST
 * Existem basicamente 2 workflow`s para trabalhar com Migration
 * o CODE-FIRST e o DATABASE-FIRST
 * DATABASE-FIRST = o banco de dados é criado à partir do próprio banco de dados com comandos SQL, linguagem de BD relacional
 * as classes são geradas à partir do BD na linguagem OO. Serviço dobrado.
 * CODE-FIRST = o banco de dados é criado à partir das classes usando o Migration. Mais usado na Linguagem OO e mais prático.
 */

namespace SalesWebMvc.Migrations
{
    public partial class Initial : Migration // Migration: Script específico para gerar e versionar a base de dados
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
