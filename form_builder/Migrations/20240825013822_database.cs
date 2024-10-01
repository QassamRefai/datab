using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace form_builder.Migrations
{
    /// <inheritdoc />
    public partial class database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FieldContent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldContent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FieldTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FieldDesign",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldDesign", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldDesign_FieldTypes_FieldTypeId",
                        column: x => x.FieldTypeId,
                        principalTable: "FieldTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forms_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormAppearances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormId = table.Column<int>(type: "int", nullable: false),
                    StyleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dir = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleFontSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleFontType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextFontSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextFontType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionFontSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionFontType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneralColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BackgroundColor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormAppearances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormAppearances_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormId = table.Column<int>(type: "int", nullable: false),
                    Contents = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormGroups_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormResponses_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormResponses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    FieldTypeId = table.Column<int>(type: "int", nullable: false),
                    FieldContentId = table.Column<int>(type: "int", nullable: false),
                    DesignId = table.Column<int>(type: "int", nullable: true),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Placeholder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupFields_FieldContent_FieldContentId",
                        column: x => x.FieldContentId,
                        principalTable: "FieldContent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupFields_FieldDesign_DesignId",
                        column: x => x.DesignId,
                        principalTable: "FieldDesign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupFields_FieldTypes_FieldTypeId",
                        column: x => x.FieldTypeId,
                        principalTable: "FieldTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupFields_FormGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "FormGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormFieldId = table.Column<int>(type: "int", nullable: false),
                    OptionText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldOptions_GroupFields_FormFieldId",
                        column: x => x.FormFieldId,
                        principalTable: "GroupFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResponseDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormResponseId = table.Column<int>(type: "int", nullable: false),
                    FormFieldId = table.Column<int>(type: "int", nullable: false),
                    ResponseValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponseDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResponseDetails_FormResponses_FormResponseId",
                        column: x => x.FormResponseId,
                        principalTable: "FormResponses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResponseDetails_GroupFields_FormFieldId",
                        column: x => x.FormFieldId,
                        principalTable: "GroupFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "FieldContent",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "textLong" },
                    { 2, "textShort" },
                    { 3, "number" },
                    { 4, "date" },
                    { 5, "time" },
                    { 6, "multipleOptions" },
                    { 7, "rating" }
                });

            migrationBuilder.InsertData(
                table: "FieldTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "text" },
                    { 2, "email" },
                    { 3, "tel" },
                    { 4, "url" },
                    { 5, "password" },
                    { 6, "search" },
                    { 7, "number" },
                    { 8, "textarea" },
                    { 9, "datetime-local" },
                    { 10, "date" },
                    { 11, "time" },
                    { 12, "week" },
                    { 13, "month" },
                    { 14, "color" },
                    { 15, "file" },
                    { 16, "checkbox" },
                    { 17, "radio" },
                    { 18, "range" },
                    { 19, "hidden" },
                    { 20, "image" },
                    { 21, "button" },
                    { 22, "submit" },
                    { 23, "reset" },
                    { 24, "select" }
                });

            migrationBuilder.InsertData(
                table: "FieldDesign",
                columns: new[] { "Id", "FieldTypeId", "Name" },
                values: new object[] { 1, 16, "switch" });

            migrationBuilder.CreateIndex(
                name: "IX_FieldDesign_FieldTypeId",
                table: "FieldDesign",
                column: "FieldTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldOptions_FormFieldId",
                table: "FieldOptions",
                column: "FormFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_FormAppearances_FormId",
                table: "FormAppearances",
                column: "FormId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FormGroups_FormId",
                table: "FormGroups",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormResponses_FormId",
                table: "FormResponses",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormResponses_UserId",
                table: "FormResponses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_UserId",
                table: "Forms",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupFields_DesignId",
                table: "GroupFields",
                column: "DesignId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupFields_FieldContentId",
                table: "GroupFields",
                column: "FieldContentId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupFields_FieldTypeId",
                table: "GroupFields",
                column: "FieldTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupFields_GroupId",
                table: "GroupFields",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponseDetails_FormFieldId",
                table: "ResponseDetails",
                column: "FormFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponseDetails_FormResponseId",
                table: "ResponseDetails",
                column: "FormResponseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FieldOptions");

            migrationBuilder.DropTable(
                name: "FormAppearances");

            migrationBuilder.DropTable(
                name: "ResponseDetails");

            migrationBuilder.DropTable(
                name: "FormResponses");

            migrationBuilder.DropTable(
                name: "GroupFields");

            migrationBuilder.DropTable(
                name: "FieldContent");

            migrationBuilder.DropTable(
                name: "FieldDesign");

            migrationBuilder.DropTable(
                name: "FormGroups");

            migrationBuilder.DropTable(
                name: "FieldTypes");

            migrationBuilder.DropTable(
                name: "Forms");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
