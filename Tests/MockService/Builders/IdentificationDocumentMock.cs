using GenFu;
using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using Super.EWalletCore.PersonDataManagement.MockService.Fillers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super.EWalletCore.PersonDataManagement.MockService.Builders
{
    public class IdentificationDocumentMock
    {
        public IdentificationDocumentMock()
        {
            A.Configure<IdentificationDocument>().Fill(x => x.Id,0);
            A.Default().FillerManager.RegisterFiller(new DocumentNumberFiller());
            A.Configure<IdentificationDocument>().Fill(x => x.IssuingDate).WithRandom(new List<DateTime?> { DateTime.Now.AddYears(new Random().Next(-10, -5)), null });
            A.Configure<IdentificationDocument>().Fill(x => x.IssuingAuthority).AsLoremIpsumWords();
            A.Configure<IdentificationDocument>().Fill(x => x.ExpiryDate).WithRandom(new List<DateTime?> { DateTime.Now.AddYears(new Random().Next(2, 5)), null });
            A.Configure<IdentificationDocument>().Fill(x => x.ValidFrom, DateTime.Now.AddYears(new Random().Next(-5, 0)));
            A.Configure<IdentificationDocument>().Fill(x => x.ValidTo, DateTime.Now.AddYears(new Random().Next(-5, 0)));
            A.Configure<IdentificationDocument>().Fill(x => x.Attachment, new List<Attachment>());

            A.Configure<IdentificationDocument>().Fill(x => x.IdentificationDocumentTypeId).WithinRange(1,4);

            A.Configure<IdentificationDocument>().Fill(x => x.IdentificationDocumentType,new IdentificationDocumentType());
        }

        public IdentificationDocument CreateIdentificationDocument()
        {
            return A.New(new IdentificationDocument());
        }

        public IEnumerable<IdentificationDocument> CreateIdentificationDocumentList(int count)
        {
            return A.ListOf<IdentificationDocument>(count);
        }
    }
}
