using GenFu;
using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super.EWalletCore.PersonDataManagement.MockService.Builders
{
    public class LegalPersonMock
    {
        public LegalPersonMock()
        {
            Random random = new Random();
            A.Configure<LegalPerson>().Fill(x => x.FullName).AsArticleTitle();

            A.Configure<LegalPerson>().Fill(x => x.PersonNumber, 0);
        }

        public LegalPerson CreateLegalPerson()
        {
            return A.New(new LegalPerson());
        }

        public IEnumerable<LegalPerson> CreateLegalPersonList(int count)
        {
            return A.ListOf<LegalPerson>(count);
        }
    }
}
