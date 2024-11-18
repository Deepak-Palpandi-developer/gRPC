using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gmail.Grpc.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_ip = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    updated_ip = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_ip = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email_address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    birth_month = table.Column<int>(type: "integer", nullable: false),
                    birth_day = table.Column<int>(type: "integer", nullable: false),
                    birth_year = table.Column<int>(type: "integer", nullable: false),
                    gender = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    prefix_phone_number = table.Column<string>(type: "text", nullable: false),
                    last_login = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    profile_picture_url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contacts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    contact_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    contact_email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    date_added = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contacts", x => x.id);
                    table.ForeignKey(
                        name: "fk_contacts_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "folders",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    date_created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    is_default = table.Column<bool>(type: "boolean", nullable: false),
                    last_modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    email_count = table.Column<int>(type: "integer", nullable: false),
                    is_archived = table.Column<bool>(type: "boolean", nullable: false),
                    is_shared = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_folders", x => x.id);
                    table.ForeignKey(
                        name: "fk_folders_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "emails",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_ip = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    updated_ip = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_ip = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    sender_id = table.Column<long>(type: "bigint", nullable: false),
                    subject = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    body = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    date_sent = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    date_received = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    date_read = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    is_read = table.Column<bool>(type: "boolean", nullable: false),
                    is_starred = table.Column<bool>(type: "boolean", nullable: false),
                    is_draft = table.Column<bool>(type: "boolean", nullable: false),
                    importance_level = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    attachment_path = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    tags = table.Column<List<string>>(type: "text[]", nullable: true),
                    has_attachments = table.Column<bool>(type: "boolean", nullable: false),
                    mime_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    reply_to = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    is_spam = table.Column<bool>(type: "boolean", nullable: false),
                    is_archived = table.Column<bool>(type: "boolean", nullable: false),
                    email_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_emails", x => x.id);
                    table.ForeignKey(
                        name: "fk_emails_emails_email_id",
                        column: x => x.email_id,
                        principalTable: "emails",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_emails_folders_sender_id",
                        column: x => x.sender_id,
                        principalTable: "folders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "recipients",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    email_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    has_read = table.Column<bool>(type: "boolean", nullable: false),
                    date_read = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    date_received = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    recipient_email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    is_starred = table.Column<bool>(type: "boolean", nullable: false),
                    is_archived = table.Column<bool>(type: "boolean", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_recipients", x => x.id);
                    table.ForeignKey(
                        name: "fk_recipients_emails_email_id",
                        column: x => x.email_id,
                        principalTable: "emails",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_contacts_user_id",
                table: "contacts",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_emails_email_id",
                table: "emails",
                column: "email_id");

            migrationBuilder.CreateIndex(
                name: "ix_emails_sender_id",
                table: "emails",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "ix_folders_user_id",
                table: "folders",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_recipients_email_id",
                table: "recipients",
                column: "email_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_email_address",
                table: "users",
                column: "email_address",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contacts");

            migrationBuilder.DropTable(
                name: "recipients");

            migrationBuilder.DropTable(
                name: "emails");

            migrationBuilder.DropTable(
                name: "folders");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
