using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeFriendr.Network.Migrations
{
    public partial class RelationshipEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Relationships",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SendingProfileID = table.Column<int>(type: "int", nullable: false),
                    ReceivingProfileID = table.Column<int>(type: "int", nullable: false),
                    BlockedByProfileID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relationships", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Relationships_Profiles_ReceivingProfileID",
                        column: x => x.ReceivingProfileID,
                        principalTable: "Profiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Relationships_Profiles_SendingProfileID",
                        column: x => x.SendingProfileID,
                        principalTable: "Profiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relationships_ReceivingProfileID",
                table: "Relationships",
                column: "ReceivingProfileID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationships_SendingProfileID",
                table: "Relationships",
                column: "SendingProfileID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relationships");
        }
    }
}
