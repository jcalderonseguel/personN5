using Super.EWalletCore.PersonDataManagement.Application.Common.Interfaces;
using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Super.EWalletCore.PersonDataManagement.Application.System.SeedSampleData
{
    public class SampleDataSeeder
    {
        private readonly IClientDbContext _context;

        private readonly List<Country> Countries = new List<Country>{
                new Country { Name = "Chile" },
                new Country { Name = "Argentina" },
                new Country { Name = "Brasil" }
            };

        private readonly List<IdentificationDocumentType> IdDocumentTypes = new List<IdentificationDocumentType>{
                new IdentificationDocumentType { IdType = "Passport", CountryId =2 },
                new IdentificationDocumentType { IdType = "DNI", CountryId =3 },
                new IdentificationDocumentType { IdType = "RUN", CountryId =1 },
                new IdentificationDocumentType { IdType = "Passport", CountryId =1 },
            };

        private readonly List<Gender> Genders= new List<Gender>{
                new Gender { Description = "Male" },
                new Gender { Description = "Female" }
            };

        private readonly List<Role> Roles = new List<Role>{
                new Role { RoleType = RoleType.Administrator,ValidFrom = DateTime.Now, ValidTo = DateTime.Now.AddYears(1) },
                new Role { RoleType = RoleType.Employee,ValidFrom = DateTime.Now, ValidTo = DateTime.Now.AddYears(1) },
                new Role { RoleType = RoleType.Client,ValidFrom = DateTime.Now, ValidTo = DateTime.Now.AddYears(1)},
                new Role { RoleType = RoleType.Guest,ValidFrom = DateTime.Now, ValidTo = DateTime.Now.AddYears(1)},
            };

        public SampleDataSeeder(IClientDbContext context)
        {
            _context = context;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            if (_context.Country.Count() != Countries.Count)
            {
                await SeedCountriesAsync(cancellationToken);
            }

            if (_context.IdentificationDocumentType.Count() != IdDocumentTypes.Count)
            {
                await SeedIdDocumentTypesAsync(cancellationToken);
            }

            if (_context.Gender.Count() != Genders.Count)
            {
                await SeedGendersAsync(cancellationToken);
            }
            if (_context.Role.Count() != Roles.Count)
            {
                await SeedRolesAsync(cancellationToken);
            }
        }
       
        private async Task SeedCountriesAsync(CancellationToken cancellationToken)
        {         
            foreach (var item in Countries)
            {
                if (_context.Country.Any(x => x.Name == item.Name)) continue;
                else _context.Country.Add(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task SeedIdDocumentTypesAsync(CancellationToken cancellationToken)
        {
            foreach (var item in IdDocumentTypes)
            {
                if (_context.IdentificationDocumentType.Any(x => x.IdType == item.IdType && x.CountryId == item.CountryId)) continue;
                else _context.IdentificationDocumentType.Add(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task SeedGendersAsync(CancellationToken cancellationToken)
        {
            foreach (var item in Genders)
            {
                if (_context.Gender.Any(x => x.Description == item.Description)) continue;
                else _context.Gender.Add(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task SeedRolesAsync(CancellationToken cancellationToken)
        {
            foreach (var item in Roles)
            {
                if (_context.Role.Any(x => x.RoleType == item.RoleType)) continue;
                else _context.Role.Add(item);
            }
            _context.Role.AddRange(Roles);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
