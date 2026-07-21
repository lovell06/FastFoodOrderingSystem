using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFoodOrderingSystem.Infrastructure.Persistence.Database.Migrations
{
    /// <inheritdoc />
    public partial class RenameRetryCountFieldOfOutboxMessageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RetryCount",
                table: "outbox_messages",
                newName: "retry_count");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "retry_count",
                table: "outbox_messages",
                newName: "RetryCount");
        }
    }
}
