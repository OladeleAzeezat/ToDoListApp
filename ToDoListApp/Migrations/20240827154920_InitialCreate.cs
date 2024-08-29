using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;

#nullable disable

namespace ToDoListApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        //protected override void Up(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.DropPrimaryKey(
        //        name: "PK_AspNetUserLogins",
        //        table: "AspNetUserLogins");

        //    migrationBuilder.AlterColumn<string>(
        //        name: "ProviderKey",
        //        table: "AspNetUserLogins",
        //        type: "nvarchar(128)",
        //        nullable: false,
        //        oldClrType: typeof(string),
        //        oldType: "nvarchar(450)");

        //    migrationBuilder.AddPrimaryKey(
        //        name: "PK_AspNetUserLogins",
        //        table: "AspNetUserLogins",
        //        columns: new[]
        //        {"LoginProvider", "ProviderKey"});

        //    migrationBuilder.AlterColumn<string>(
        //        name: "Name",
        //        table: "AspNetUserTokens",
        //        type: "nvarchar(128)",
        //        maxLength: 128,
        //        nullable: false,
        //        oldClrType: typeof(string),
        //        oldType: "nvarchar(450)");

        //    migrationBuilder.AlterColumn<string>(
        //        name: "LoginProvider",
        //        table: "AspNetUserTokens",
        //        type: "nvarchar(128)",
        //        maxLength: 128,
        //        nullable: false,
        //        oldClrType: typeof(string),
        //        oldType: "nvarchar(450)");

        //    migrationBuilder.AlterColumn<string>(
        //        name: "ProviderKey",
        //        table: "AspNetUserLogins",
        //        type: "nvarchar(128)",
        //        maxLength: 128,
        //        nullable: false,
        //        oldClrType: typeof(string),
        //        oldType: "nvarchar(450)");

        //    migrationBuilder.AlterColumn<string>(
        //        name: "LoginProvider",
        //        table: "AspNetUserLogins",
        //        type: "nvarchar(128)",
        //        maxLength: 128,
        //        nullable: false,
        //        oldClrType: typeof(string),
        //        oldType: "nvarchar(450)");

        //    migrationBuilder.CreateTable(
        //        name: "ToDoItems",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
        //            Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            IsCompleted = table.Column<bool>(type: "bit", nullable: false),
        //            CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_ToDoItems", x => x.Id);
        //            table.ForeignKey(
        //                name: "FK_ToDoItems_AspNetUsers_UserId",
        //                column: x => x.UserId,
        //                principalTable: "AspNetUsers",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateIndex(
        //        name: "IX_ToDoItems_UserId",
        //        table: "ToDoItems",
        //        column: "UserId");
        //}

        ///// <inheritdoc />
        //protected override void Down(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.DropPrimaryKey(
        //        name: "PK_AspNetUserLogins",
        //        table: "AspNetUserLogins");

        //    migrationBuilder.AlterColumn<string>(
        //        name: "ProviderKey",
        //        table: "AspNetUserLogins",
        //        type: "nvarchar(128)",
        //        nullable: false,
        //        oldClrType: typeof(string),
        //        oldType: "nvarchar(450)");

        //    migrationBuilder.AddPrimaryKey(
        //        name: "PK_AspNetUserLogins",
        //        table: "AspNetUserLogins",
        //       columns: new[]
        //        {"LoginProvider", "ProviderKey"}) ;

        //    migrationBuilder.DropTable(
        //        name: "ToDoItems");

        //    migrationBuilder.AlterColumn<string>(
        //        name: "Name",
        //        table: "AspNetUserTokens",
        //        type: "nvarchar(450)",
        //        nullable: false,
        //        oldClrType: typeof(string),
        //        oldType: "nvarchar(128)",
        //        oldMaxLength: 128);

        //    migrationBuilder.AlterColumn<string>(
        //        name: "LoginProvider",
        //        table: "AspNetUserTokens",
        //        type: "nvarchar(450)",
        //        nullable: false,
        //        oldClrType: typeof(string),
        //        oldType: "nvarchar(128)",
        //        oldMaxLength: 128);

        //    migrationBuilder.AlterColumn<string>(
        //        name: "ProviderKey",
        //        table: "AspNetUserLogins",
        //        type: "nvarchar(450)",
        //        nullable: false,
        //        oldClrType: typeof(string),
        //        oldType: "nvarchar(128)",
        //        oldMaxLength: 128);

        //    migrationBuilder.AlterColumn<string>(
        //        name: "LoginProvider",
        //        table: "AspNetUserLogins",
        //        type: "nvarchar(450)",
        //        nullable: false,
        //        oldClrType: typeof(string),
        //        oldType: "nvarchar(128)",
        //        oldMaxLength: 128);
        //}

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop existing primary key for AspNetUserLogins
            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            // Alter columns on AspNetUserLogins
            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            // Recreate primary key for AspNetUserLogins
            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            // Alter columns on AspNetUserTokens
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            // Create ToDoItems table
            migrationBuilder.CreateTable(
                name: "ToDoItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToDoItems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItems_UserId",
                table: "ToDoItems",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop ToDoItems table
            migrationBuilder.DropTable(name: "ToDoItems");

            // Drop primary key for AspNetUserLogins
            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            // Revert column alterations for AspNetUserTokens
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            // Revert column alterations for AspNetUserLogins
            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            // Recreate original primary key for AspNetUserLogins
            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });
        }

    }
}
