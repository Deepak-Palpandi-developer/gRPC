using Gmail.Domain.Entities.Contacts;
using Gmail.Domain.Entities.Emails;
using Gmail.Domain.Entities.Folders;
using Gmail.Domain.Entities.Recipients;
using Gmail.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gmail.Domain.Data;

public class GmailBaseContext<T> : DbContext where T : DbContext
{

    public GmailBaseContext(DbContextOptions<T> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Email> Emails { get; set; }
    public DbSet<Folder> Folders { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Recipient> Recipients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(ConfigureUserEntity);
        modelBuilder.Entity<Email>(ConfigureEmailEntity);
        modelBuilder.Entity<Folder>(ConfigureFolderEntity);
        modelBuilder.Entity<Contact>(ConfigureContactEntity);
        modelBuilder.Entity<Recipient>(ConfigureRecipientEntity);
    }

    private static void ConfigureUserEntity(EntityTypeBuilder<User> entity)
    {
        entity.HasKey(u => u.Id);
        entity.HasIndex(u => u.EmailAddress).IsUnique();
        entity.Property(u => u.Username).HasMaxLength(100);
        entity.Property(u => u.EmailAddress).HasMaxLength(200);
        entity.Property(u => u.FirstName).HasMaxLength(100);
        entity.Property(u => u.LastName).HasMaxLength(100);
        entity.Property(u => u.Gender).HasMaxLength(20);
        entity.Property(u => u.PhoneNumber).HasMaxLength(15);

        entity.HasMany(u => u.Folders)
            .WithOne()
            .HasForeignKey(f => f.UserId);

        entity.HasMany(u => u.Contacts)
            .WithOne()
            .HasForeignKey(c => c.UserId);
    }

    private static void ConfigureEmailEntity(EntityTypeBuilder<Email> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Subject).HasMaxLength(500);
        entity.Property(e => e.Body).HasMaxLength(4000);
        entity.Property(e => e.ImportanceLevel).HasMaxLength(50);
        entity.Property(e => e.AttachmentPath).HasMaxLength(1000);
        entity.Property(e => e.MimeType).HasMaxLength(100);
        entity.Property(e => e.ReplyTo).HasMaxLength(200);

        entity.HasMany(e => e.Recipients)
            .WithOne()
            .HasForeignKey(r => r.EmailId);
    }

    private static void ConfigureFolderEntity(EntityTypeBuilder<Folder> entity)
    {
        entity.HasKey(f => f.Id);
        entity.Property(f => f.Name).HasMaxLength(100);

        entity.HasMany(f => f.Emails)
            .WithOne()
            .HasForeignKey(e => e.SenderId);
    }

    private static void ConfigureContactEntity(EntityTypeBuilder<Contact> entity)
    {
        entity.HasKey(c => c.Id);
        entity.Property(c => c.ContactName).HasMaxLength(200);
        entity.Property(c => c.ContactEmail).HasMaxLength(200);
        entity.Property(c => c.PhoneNumber).HasMaxLength(15);
        entity.Property(c => c.Notes).HasMaxLength(1000);
    }

    private static void ConfigureRecipientEntity(EntityTypeBuilder<Recipient> entity)
    {
        entity.HasKey(r => r.Id);
        entity.Property(r => r.RecipientEmail).HasMaxLength(200);
    }
}

public class GmailContext : GmailBaseContext<GmailContext>
{
    public GmailContext(DbContextOptions<GmailContext> options) : base(options)
    {
    }
}