using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeFriendr.Network.Migrations
{
    public partial class AddedMessageEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderID = table.Column<int>(type: "int", nullable: false),
                    SenderUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderProfileID = table.Column<int>(type: "int", nullable: true),
                    RecipientID = table.Column<int>(type: "int", nullable: false),
                    RecipientUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipientProfileID = table.Column<int>(type: "int", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRead = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateSent = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Messages_Profiles_RecipientProfileID",
                        column: x => x.RecipientProfileID,
                        principalTable: "Profiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Profiles_SenderProfileID",
                        column: x => x.SenderProfileID,
                        principalTable: "Profiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RecipientProfileID",
                table: "Messages",
                column: "RecipientProfileID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderProfileID",
                table: "Messages",
                column: "SenderProfileID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}
