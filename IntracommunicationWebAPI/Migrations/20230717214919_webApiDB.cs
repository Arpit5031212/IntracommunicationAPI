using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IntraCommunicationWebApi.Migrations
{
    public partial class webApiDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User_Profile",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    First_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Last_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Contact = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    DOB = table.Column<DateTime>(type: "date", nullable: false),
                    Password = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    AddressLine1 = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    AddressLine2 = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    State = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    PermanentCity = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    PermanentState = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Profile", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Group_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Group_Description = table.Column<string>(type: "text", nullable: false),
                    Group_Type = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "date", nullable: false),
                    Created_By = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupID);
                    table.ForeignKey(
                        name: "FK_Groups_Created_By",
                        column: x => x.Created_By,
                        principalTable: "User_Profile",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Group_Invites_Requests",
                columns: table => new
                {
                    InviteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sent_to = table.Column<int>(type: "int", nullable: false),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    isAccepted = table.Column<bool>(type: "bit", nullable: false),
                    isApproved = table.Column<bool>(type: "bit", nullable: false),
                    Created_by = table.Column<int>(type: "int", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group_Invites_Requests", x => x.InviteID);
                    table.ForeignKey(
                        name: "FK_Group_Invites_Requests_Created_by",
                        column: x => x.Created_by,
                        principalTable: "User_Profile",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Group_Invites_Requests_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "GroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Group_Invites_Requests_Sent_to",
                        column: x => x.Sent_to,
                        principalTable: "User_Profile",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Group_Members",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    GroupID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group_Members", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Group_Members_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "GroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Group_Members_MemberID",
                        column: x => x.MemberID,
                        principalTable: "User_Profile",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Post_Type = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    Posted_On_Group = table.Column<int>(type: "int", nullable: false),
                    PostedAt = table.Column<DateTime>(type: "date", nullable: false),
                    PostedBy = table.Column<int>(type: "int", nullable: false),
                    Post_Description = table.Column<string>(type: "text", nullable: false),
                    Start_Time = table.Column<DateTime>(type: "datetime", nullable: true),
                    End_Time = table.Column<DateTime>(type: "datetime", nullable: true),
                    URL = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostID);
                    table.ForeignKey(
                        name: "FK_Posts_Posted_On",
                        column: x => x.Posted_On_Group,
                        principalTable: "Groups",
                        principalColumn: "GroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_PostedBy",
                        column: x => x.PostedBy,
                        principalTable: "User_Profile",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Commented_By = table.Column<int>(type: "int", nullable: false),
                    PostID = table.Column<int>(type: "int", nullable: false),
                    CommentDescription = table.Column<string>(type: "text", nullable: false),
                    Commented_At = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Comments_Commented_By",
                        column: x => x.Commented_By,
                        principalTable: "User_Profile",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    PostID = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Likes_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Likes_UserId",
                        column: x => x.UserId,
                        principalTable: "User_Profile",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Commented_By",
                table: "Comments",
                column: "Commented_By");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostID",
                table: "Comments",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_Group_Invites_Requests_Created_by",
                table: "Group_Invites_Requests",
                column: "Created_by");

            migrationBuilder.CreateIndex(
                name: "IX_Group_Invites_Requests_GroupID",
                table: "Group_Invites_Requests",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Group_Invites_Requests_Sent_to",
                table: "Group_Invites_Requests",
                column: "Sent_to");

            migrationBuilder.CreateIndex(
                name: "IX_Group_Members_GroupID",
                table: "Group_Members",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Group_Members_MemberID",
                table: "Group_Members",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Created_By",
                table: "Groups",
                column: "Created_By");

            migrationBuilder.CreateIndex(
                name: "UK_Groups_Group_Name",
                table: "Groups",
                column: "Group_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Likes_PostID",
                table: "Likes",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Posted_On_Group",
                table: "Posts",
                column: "Posted_On_Group");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostedBy",
                table: "Posts",
                column: "PostedBy");

            migrationBuilder.CreateIndex(
                name: "UK_User_Profile_Contact",
                table: "User_Profile",
                column: "Contact",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_User_Profile_Email",
                table: "User_Profile",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Group_Invites_Requests");

            migrationBuilder.DropTable(
                name: "Group_Members");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "User_Profile");
        }
    }
}
