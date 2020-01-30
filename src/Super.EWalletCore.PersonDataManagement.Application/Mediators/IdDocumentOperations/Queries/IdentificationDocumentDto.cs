using System;
using System.Collections.Generic;
using System.Text;

namespace Super.EWalletCore.PersonDataManagement.Application.Mediators.IdDocumentOperations.Queries
{
  public class IdentificationDocumentDto
    {
      
        public long Id { get; set; }
        public  string IdType { get; set; }
        public string Description { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public bool CheckDigit { get; set; }
    }
}
