using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super.EWalletCore.PersonDataManagement.MockService.Builders
{
    public class CountryMock
    {
        public IEnumerable<Country> CreateCountriesList()
        {
            return new List<Country> {
                new Country
                {
                    Id = 1,
                    Name = "Chile"
                },
                new Country
                {
                    Id = 2,
                    Name = "Argentina"
                },
                new Country
                {
                    Id = 3,
                    Name = "Brasil"
                }
            };
        }
    }
}
