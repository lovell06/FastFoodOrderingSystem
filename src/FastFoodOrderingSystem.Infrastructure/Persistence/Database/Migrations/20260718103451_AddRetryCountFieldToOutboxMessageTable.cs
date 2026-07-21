using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFoodOrderingSystem.Infrastructure.Persistence.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddRetryCountFieldToOutboxMessageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RetryCount",
                table: "outbox_messages",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RetryCount",
                table: "outbox_messages");
        }
    }
}
