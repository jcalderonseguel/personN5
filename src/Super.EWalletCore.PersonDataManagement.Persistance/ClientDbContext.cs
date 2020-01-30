using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Super.EWalletCore.PersonDataManagement.Application.Common.Interfaces;
using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Super.EWalletCore.PersonDataManagement.Persistance
{
    public class ClientDbContext : DbContext, IClientDbContext
    {
        public ClientDbContext(DbContextOptions<ClientDbContext> options)
            : base(options)
        { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasIndex(e => e.PersonPersonNumber)
                    .HasName("IX_FK_PersonAddressData");

                entity.Property(e => e.Coname)
                    .HasColumnName("COName");

                entity.Property(e => e.Country).IsRequired();

                entity.Property(e => e.PoboxPostalCode).HasColumnName("POBoxPostalCode");

                entity.Property(e => e.PostCode).IsRequired();

                entity.Property(e => e.ValidFrom).HasColumnType("datetime");

                entity.Property(e => e.ValidFrom).IsRequired();

                entity.Property(e => e.ValidTo).HasColumnType("datetime");

                entity.HasOne(d => d.PersonPersonNumberNavigation)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.PersonPersonNumber)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PersonAddressData");
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.HasIndex(e => e.IdentificationDocumentId)
                    .HasName("IX_FK_IdentificationDocumentAttachment");

                entity.HasIndex(e => e.PersonNumber)
                    .HasName("IX_FK_PersonAttachment");

                entity.Property(e => e.EncodedKey).IsRequired();

                entity.Property(e => e.FileName).IsRequired();

                entity.Property(e => e.Location).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.OwnerKey).IsRequired();

                entity.HasOne(d => d.IdentificationDocument)
                    .WithMany(p => p.Attachment)
                    .HasForeignKey(d => d.IdentificationDocumentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_IdentificationDocumentAttachment");

                entity.HasOne(d => d.PersonNumberNavigation)
                    .WithMany(p => p.Attachment)
                    .HasForeignKey(d => d.PersonNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonAttachment");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.HasIndex(e => e.PersonNumber)
                                .HasName("IX_FK_PersonEmail");

                entity.Property(e => e.ValidFrom).HasColumnType("datetime").IsRequired();

                entity.Property(e => e.ValidTo).HasColumnType("datetime");

                entity.HasOne(d => d.PersonNumberNavigation)
                    .WithMany(p => p.Email)
                    .HasForeignKey(d => d.PersonNumber)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PersonEmail");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.Property(e => e.Description).IsRequired();
            });

            modelBuilder.Entity<IdentificationDocument>(entity =>
            {
                entity.HasIndex(e => e.IdentificationDocumentTypeId)
                    .HasName("IX_FK_IdentificationDocumentTypeIdentificationDocument");

                entity.HasIndex(e => e.PersonNumber)
                    .HasName("IX_FK_PersonIdentificationDocument");

                entity.Property(e => e.DocumentNumber).IsRequired();

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.IssuingDate).HasColumnType("datetime");

                entity.Property(e => e.ValidFrom).HasColumnType("datetime");

                entity.Property(e => e.ValidFrom).IsRequired();

                entity.Property(e => e.ValidTo).HasColumnType("datetime");

                entity.HasOne(d => d.IdentificationDocumentType)
                    .WithMany(p => p.IdentificationDocument)
                    .HasForeignKey(d => d.IdentificationDocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IdentificationDocumentTypeIdentificationDocument");

                entity.HasOne(d => d.PersonNumberNavigation)
                    .WithMany(p => p.IdentificationDocument)
                    .HasForeignKey(d => d.PersonNumber)
                    .HasConstraintName("FK_PersonIdentificationDocument");
            });

            modelBuilder.Entity<IdentificationDocumentType>(entity =>
            {
                entity.HasIndex(e => e.CountryId)
                   .HasName("IX_FK_CountryIdentificationDocumentType");

                entity.Property(e => e.IdType).IsRequired();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.IdentificationDocumentType)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CountryIdentificationDocumentType");
            });

            modelBuilder.Entity<Income>(entity =>
            {
                entity.HasIndex(e => e.PersonNumber)
                    .HasName("IX_FK_NaturalPersonIncome");

                entity.Property(e => e.ValidFrom).HasColumnType("datetime").IsRequired();

                entity.Property(e => e.ValidTo).HasColumnType("datetime");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.PersonNumberNavigation)
                    .WithMany(p => p.Income)
                    .HasForeignKey(d => d.PersonNumber)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_NaturalPersonIncome");
            });

            modelBuilder.Entity<LegalPerson>(entity =>
            {
                entity.HasKey(e => e.PersonNumber);

                entity.ToTable("LegalPerson");

                entity.Property(e => e.PersonNumber).ValueGeneratedNever();

                entity.Property(e => e.FullName).IsRequired();

                entity.HasOne(d => d.PersonNumberNavigation)
                    .WithOne(p => p.PersonLegalPerson)
                    .HasForeignKey<LegalPerson>(d => d.PersonNumber)
                    .HasConstraintName("FK_LegalPerson_inherits_Person");
            });

            modelBuilder.Entity<NaturalPerson>(entity =>
            {
                entity.HasKey(e => e.PersonNumber);

                entity.ToTable("NaturalPerson");

                entity.HasIndex(e => e.GenderId)
                    .HasName("IX_FK_GenderNaturalPerson");

                entity.Property(e => e.PersonNumber).ValueGeneratedNever();

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.BirthDate).IsRequired();

                entity.Property(e => e.DateOfDeath).HasColumnType("datetime");

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.LastName).IsRequired();

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.PersonNaturalPerson)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GenderNaturalPerson");

                entity.HasOne(d => d.PersonNumberNavigation)
                    .WithOne(p => p.PersonNaturalPerson)
                    .HasForeignKey<NaturalPerson>(d => d.PersonNumber)
                    .HasConstraintName("FK_NaturalPerson_inherits_Person");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.PersonNumber);

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_FK_RolePerson");

                entity.Property(e => e.Category).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_RolePerson");
            });

            modelBuilder.Entity<Phone>(entity =>
            {
                entity.HasIndex(e => e.PersonNumber)
                      .HasName("IX_FK_PersonPhone");

                entity.Property(e => e.AreaCode).IsRequired();

                entity.Property(e => e.CountryCode).IsRequired();

                entity.Property(e => e.PhoneNumber).IsRequired();

                entity.Property(e => e.ValidFrom).HasColumnType("datetime").IsRequired();

                entity.Property(e => e.ValidTo).HasColumnType("datetime");

                entity.HasOne(d => d.PersonNumberNavigation)
                    .WithMany(p => p.Phone)
                    .HasForeignKey(d => d.PersonNumber)
                    .HasConstraintName("FK_PersonPhone");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.ValidFrom).HasColumnType("datetime").IsRequired();

                entity.Property(e => e.ValidTo).HasColumnType("datetime");
            });


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientDbContext).Assembly);
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Attachment> Attachment { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Email> Email { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<IdentificationDocument> IdentificationDocument { get; set; }
        public virtual DbSet<IdentificationDocumentType> IdentificationDocumentType { get; set; }
        public virtual DbSet<Income> Income { get; set; }
        public virtual DbSet<LegalPerson> LegalPerson { get; set; }
        public virtual DbSet<NaturalPerson> NaturalPerson { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Phone> Phone { get; set; }
        public virtual DbSet<Role> Role { get; set; }
         

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            //foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            //{
            //    switch (entry.State)
            //    {
            //        case EntityState.Added:
            //            entry.Entity.CreatedBy = _currentUserService.UserId;
            //            entry.Entity.Created = _dateTime.Now;
            //            break;
            //        case EntityState.Modified:
            //            entry.Entity.LastModifiedBy = _currentUserService.UserId;
            //            entry.Entity.LastModified = _dateTime.Now;
            //            break;
            //    }
            //}

            return base.SaveChangesAsync(cancellationToken);
        }
    }


}
