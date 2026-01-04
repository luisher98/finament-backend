using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finament.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeUserPasswordHashRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                 ALTER TABLE "Users"
                 ALTER COLUMN password_hash SET NOT NULL;
             """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                 ALTER TABLE "Users"
                 ALTER COLUMN password_hash DROP NOT NULL;
             """);
        }
    }
}
