using Microsoft.EntityFrameworkCore;
using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Super.EWalletCore.PersonDataManagement.Application.Common.Interfaces
{
    public interface IClientDbContext
    {
        DbSet<Address> Address { get; set; }
        DbSet<Attachment> Attachment { get; set; }
        DbSet<Country> Country { get; set; }
        DbSet<Email> Email { get; set; }
        DbSet<Gender> Gender { get; set; }
        DbSet<IdentificationDocument> IdentificationDocument { get; set; }
        DbSet<IdentificationDocumentType> IdentificationDocumentType { get; set; }
        DbSet<Income> Income { get; set; }
        DbSet<Person> Persons { get; set; }
        DbSet<LegalPerson> LegalPerson { get; set; }
        DbSet<NaturalPerson> NaturalPerson { get; set; }
        DbSet<Phone> Phone { get; set; }
        DbSet<Role> Role { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
