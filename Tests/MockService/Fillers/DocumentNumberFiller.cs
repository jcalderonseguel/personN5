using GenFu;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super.EWalletCore.PersonDataManagement.MockService.Fillers
{
    public class DocumentNumberFiller: PropertyFiller<string>
    {
        public DocumentNumberFiller()
            : base(
                    new[] { "object" },
                    new[] { "DocumentNumber" })
        {
        }

        public override object GetValue(object instance)
        {
            var documentNumbers = new List<string> { "12345678-1", "14707626-6", "1234" };
            return documentNumbers[new Random().Next(documentNumbers.Count)];
        }
    }
}
