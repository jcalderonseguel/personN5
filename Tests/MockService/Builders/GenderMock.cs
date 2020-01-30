using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super.EWalletCore.PersonDataManagement.MockService.Builders
{
    public class GenderMock
    {
        public IEnumerable<Gender> CreateGendersList()
        {
            return new List<Gender> {
                 new Gender
                 {
                      Id=1,
                     Description = "Male"
                 },
                new Gender
                {
                     Id=2,
                    Description = "Female"
                }
            };
        }
    }
}
