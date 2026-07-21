using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFoodOrderingSystem.Infrastructure.Persistence.Database.Migrations
{
    /// <inheritdoc />
    public partial class MakeIndexToOutboxMessageTableOccurredAtUtc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "error",
                table: "outbox_messages",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_outbox_messages_occurred_at_utc",
                table: "outbox_messages",
                column: "occurred_at_utc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_outbox_messages_occurred_at_utc",
                table: "outbox_messages");

            migrationBuilder.AlterColumn<string>(
                name: "error",
                table: "outbox_messages",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
