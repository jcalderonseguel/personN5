using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super.EWalletCore.PersonDataManagement.MockService.Builders
{
    public class IdentificationDocumentTypeMock
    {
        public IEnumerable<IdentificationDocumentType> CreateIdentificationDocumentTypeList()
        {
            return new List<IdentificationDocumentType> {
                 new IdentificationDocumentType
                {
                      Id = 1,
                    IdType = "RUN", CountryId =1
                },
                new IdentificationDocumentType
                {
                    Id = 2,
                    IdType = "Passport", CountryId =1
                },
                new IdentificationDocumentType
                {
                     Id = 3,
                    IdType = "Passport", CountryId =2
                },
                new IdentificationDocumentType {
                    Id = 4,
                    IdType = "DNI", CountryId =3
                }
            };
        }
    }
}
