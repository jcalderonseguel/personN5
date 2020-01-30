using System;
using Newtonsoft.Json;

namespace Super.EWalletCore.PersonDataManagement.API.Models
{
    public partial class PersonDto
    {
        [JsonProperty("personNumber")]
        public long PersonNumber { get; set; }

        [JsonProperty("category")]
        public long Category { get; set; }

        [JsonProperty("roleId")]
        public long RoleId { get; set; }

        [JsonProperty("role")]
        public Role Role { get; set; }

        [JsonProperty("legalPerson")]
        public LegalPerson LegalPerson { get; set; }

        [JsonProperty("naturalPerson")]
        public NaturalPerson NaturalPerson { get; set; }

        [JsonProperty("address")]
        public Address[] Address { get; set; }

        [JsonProperty("attachment")]
        public Attachment[] Attachment { get; set; }

        [JsonProperty("email")]
        public Email[] Email { get; set; }

        [JsonProperty("identificationDocument")]
        public IdentificationDocument[] IdentificationDocument { get; set; }

        [JsonProperty("phone")]
        public Phone[] Phone { get; set; }
    }

    public partial class Address
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("postCode")]
        public string PostCode { get; set; }

        [JsonProperty("streetName")]
        public string StreetName { get; set; }

        [JsonProperty("buildingNumber")]
        public string BuildingNumber { get; set; }

        [JsonProperty("addressLine")]
        public string AddressLine { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("addressType")]
        public long AddressType { get; set; }

        [JsonProperty("postOfficeBoxCode")]
        public string PostOfficeBoxCode { get; set; }

        [JsonProperty("poBoxPostalCode")]
        public string PoBoxPostalCode { get; set; }

        [JsonProperty("statusCodeAddress")]
        public long StatusCodeAddress { get; set; }

        [JsonProperty("coname")]
        public string Coname { get; set; }

        [JsonProperty("validFrom")]
        public DateTimeOffset ValidFrom { get; set; }

        [JsonProperty("validTo")]
        public DateTimeOffset ValidTo { get; set; }

        [JsonProperty("personNumber")]
        public long PersonNumber { get; set; }
    }

    public partial class Attachment
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("ownerKey")]
        public string OwnerKey { get; set; }

        [JsonProperty("fileSize")]
        public long FileSize { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("encodedKey")]
        public string EncodedKey { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("identificationDocumentId")]
        public long IdentificationDocumentId { get; set; }

        [JsonProperty("personNumber")]
        public long PersonNumber { get; set; }
    }

    public partial class Email
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; }

        [JsonProperty("validated")]
        public bool Validated { get; set; }

        [JsonProperty("validFrom")]
        public DateTimeOffset ValidFrom { get; set; }

        [JsonProperty("validTo")]
        public DateTimeOffset ValidTo { get; set; }

        [JsonProperty("personNumber")]
        public long PersonNumber { get; set; }
    }

    public partial class IdentificationDocument
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("documentNumber")]
        public string DocumentNumber { get; set; }

        [JsonProperty("issuingDate")]
        public DateTimeOffset IssuingDate { get; set; }

        [JsonProperty("issuingAuthority")]
        public string IssuingAuthority { get; set; }

        [JsonProperty("expiryDate")]
        public DateTimeOffset ExpiryDate { get; set; }

        [JsonProperty("validFrom")]
        public DateTimeOffset ValidFrom { get; set; }

        [JsonProperty("validTo")]
        public DateTimeOffset ValidTo { get; set; }

        [JsonProperty("personNumber")]
        public long PersonNumber { get; set; }

        [JsonProperty("identificationDocumentTypeId")]
        public long IdentificationDocumentTypeId { get; set; }
    }

    public partial class LegalPerson
    {
        public string FullName { get; set; }

        public long PersonNumber { get; set; }
    }

    public partial class NaturalPerson
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastNamePrefix")]
        public string LastNamePrefix { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("birthDate")]
        public DateTimeOffset BirthDate { get; set; }

        [JsonProperty("DateOfDeath")]
        public DateTimeOffset DateOfDeath { get; set; }

        [JsonProperty("maritalStatus")]
        public long MaritalStatus { get; set; }

        [JsonProperty("nationality")]
        public long Nationality { get; set; }

        [JsonProperty("genderId")]
        public long GenderId { get; set; }

        [JsonProperty("personNumber")]
        public long PersonNumber { get; set; }

        [JsonProperty("income")]
        public Income[] Income { get; set; }
    }

    public partial class Income
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("currency")]
        public long Currency { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("periodicity")]
        public long Periodicity { get; set; }

        [JsonProperty("validFrom")]
        public DateTimeOffset ValidFrom { get; set; }

        [JsonProperty("validTo")]
        public DateTimeOffset ValidTo { get; set; }

        [JsonProperty("personNumber")]
        public long PersonNumber { get; set; }
    }

    public partial class Phone
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("areaCode")]
        public string AreaCode { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("extension")]
        public string Extension { get; set; }

        [JsonProperty("phoneType")]
        public long PhoneType { get; set; }

        [JsonProperty("validFrom")]
        public DateTimeOffset ValidFrom { get; set; }

        [JsonProperty("validTo")]
        public DateTimeOffset ValidTo { get; set; }

        [JsonProperty("personNumber")]
        public long PersonNumber { get; set; }
    }

    public partial class Role
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("roleType")]
        public long RoleType { get; set; }

        [JsonProperty("validFrom")]
        public DateTimeOffset ValidFrom { get; set; }

        [JsonProperty("validTo")]
        public DateTimeOffset ValidTo { get; set; }
    }
}
