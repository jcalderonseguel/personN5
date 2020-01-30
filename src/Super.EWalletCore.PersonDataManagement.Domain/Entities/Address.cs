using System;
using System.Collections.Generic;

namespace Super.EWalletCore.PersonDataManagement.Domain.Entities
{
    public partial class Address
    {
        public long Id { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string StreetName { get; set; }
        public string BuildingNumber { get; set; }
        public string AddressLine { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public AddressType AddressType { get; set; }
        public string PostOfficeBoxCode { get; set; }
        public string PoboxPostalCode { get; set; }
        public StatusCodeAddress? StatusCodeAddress { get; set; }
        public string Coname { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public long? PersonPersonNumber { get; set; }

        public virtual Person PersonPersonNumberNavigation { get; set; }
    }
}
