using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Flunt.Notifications;
using MediatR;
using Super.EWalletCore.PersonDataManagement.Application.Common.Interfaces;
using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using Super.EWalletCore.PersonDataManagement.Domain.Repositories;

namespace Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.Insert
{
    // Notifiable,
    public class InsertPerson : IRequest<long>
    {
        public long PersonNumber { get; set; }
        public PersonCategory Category { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public LegalPerson LegalPerson { get; set; }
        public NaturalPerson NaturalPerson { get; set; }
        public ICollection<Address> Address { get; set; }
        public ICollection<Attachment> Attachment { get; set; }
        public ICollection<Email> Email { get; set; }
        public ICollection<IdentificationDocument> IdentificationDocument { get; set; }
        public ICollection<Phone> Phone { get; set; }

    }



    public class InsertHandler : IRequestHandler<InsertPerson, long>
    {
        private readonly IClientDbContext _context;
        //private readonly IMediator _mediator;
        public InsertHandler(IClientDbContext context)
        {
            _context = context;
            //_mediator = mediator;
        }

        public async Task<long> Handle(InsertPerson request, CancellationToken cancellationToken)
        {
            LegalPerson legalPerson = new LegalPerson();

            if (request.LegalPerson != null)
            {
                legalPerson = new LegalPerson()
                {
                    FullName = request.LegalPerson.FullName
                };
            }

            NaturalPerson naturalPerson = new NaturalPerson();

            if (request.NaturalPerson != null)
            {
               
                List<Income> incomes = new List<Income>();
                foreach (var i in request.NaturalPerson.Income)
                {
                    var income = new Income
                    {
                        Value = (decimal)i.Value,
                        Currency = (Currency)i.Currency,
                        Company = i.Company,
                        Periodicity = (Periodicity)i.Periodicity,
                        ValidFrom = (i.ValidFrom.Equals(null)) ? null : new DateTime?(Convert.ToDateTime(i.ValidFrom)),
                        ValidTo = (i.ValidFrom.Equals(null)) ? null : new DateTime?(Convert.ToDateTime(i.ValidTo)),
                    };
                    incomes.Add(income);
                }
                naturalPerson = new NaturalPerson()
                {
                    FirstName = request.NaturalPerson.FirstName,
                    LastName = request.NaturalPerson.LastName,
                    LastNamePrefix = request.NaturalPerson.LastNamePrefix,
                    BirthDate = (request.NaturalPerson.BirthDate.Equals(null)) ? null : new DateTime?(Convert.ToDateTime(request.NaturalPerson.BirthDate)),
                    DateOfDeath = (request.NaturalPerson.DateOfDeath.Equals(null)) ? null : new DateTime?(Convert.ToDateTime(request.NaturalPerson.DateOfDeath)),
                    MaritalStatus = (MaritalStatus)request.NaturalPerson.MaritalStatus,
                    Nationality = (Nationality)request.NaturalPerson.Nationality,
                    GenderId = request.NaturalPerson.GenderId,
                    Income = incomes

                };
            }

            List<Address> addresses = new List<Address>();
            if (!request.Address.Equals(null))
            {
              
                foreach(var a in request.Address)
                {
                    var address = new Address
                    {
                        Country = a.Country,
                        Region = a.Region,
                        City = a.City,
                        PostCode = a.PostCode,
                        StreetName = a.StreetName,
                        BuildingNumber = a.BuildingNumber,
                        AddressLine = a.AddressLine,
                        Latitude = a.Latitude,
                        Longitude = a.Longitude,
                        AddressType = (AddressType)a.AddressType,
                        PostOfficeBoxCode = a.PostOfficeBoxCode,
                        PoboxPostalCode = a.PoboxPostalCode,
                        StatusCodeAddress = (StatusCodeAddress)a.StatusCodeAddress,
                        Coname = a.Coname,
                        ValidFrom = (a.ValidFrom.Equals(null)) ? null : new DateTime?(Convert.ToDateTime(a.ValidFrom)),
                        ValidTo = (a.ValidTo.Equals(null)) ? null : new DateTime?(Convert.ToDateTime(a.ValidTo))
                    };
                    addresses.Add(address);
                }
            }

            List<IdentificationDocument> identificationDocuments = new List<IdentificationDocument>();
            if (!request.IdentificationDocument.Equals(null))
            {
                foreach(var i in request.IdentificationDocument)
                {
                    var d = new IdentificationDocument
                    {
                        DocumentNumber = i.DocumentNumber,
                        IssuingDate = (i.IssuingDate.Equals(null)) ? null : new DateTime?(Convert.ToDateTime(i.IssuingDate)),
                        IssuingAuthority = i.IssuingAuthority,
                        ExpiryDate = (i.ExpiryDate.Equals(null)) ? null : new DateTime?(Convert.ToDateTime(i.ExpiryDate)),
                        ValidFrom = (i.ValidFrom.Equals(null)) ? null : new DateTime?(Convert.ToDateTime(i.ValidFrom)),
                        ValidTo = (i.ValidTo.Equals(null)) ? null : new DateTime?(Convert.ToDateTime(i.ValidTo)),
                        IdentificationDocumentTypeId = (int)i.IdentificationDocumentTypeId
                    };
                    identificationDocuments.Add(d);
                };
            }

            List<Attachment> attachments = new List<Attachment>();
            if (!request.Attachment.Equals(null))
            {
                foreach(var a in request.Attachment)
                {
                    var attachment = new Attachment
                    {
                        FileName = a.FileName,
                        Notes = a.Notes,
                        Type = (AttachmentType)a.Type,
                        OwnerKey = a.OwnerKey,
                        FileSize =a.FileSize,
                        Name = a.Name,
                        EncodedKey = a.EncodedKey,
                        Location = a.Location,
                    };
                    attachments.Add(attachment);
                }
                
            }
            List<Email> emails = new List<Email>();
            if (!request.Email.Equals(null))
            {
                foreach(var e in request.Email)
                {
                    var email = new Email
                    {
                         EmailAddress = e.EmailAddress,
                         Validated = e.Validated,
                         ValidFrom = (e.ValidFrom.Equals(null)) ? null : new DateTime?(Convert.ToDateTime(e.ValidFrom)),
                         ValidTo = (e.ValidTo.Equals(null)) ? null : new DateTime?(Convert.ToDateTime(e.ValidTo)),
                    };
                    emails.Add(email);
                };
               
            }

            List<Phone> phones = new List<Phone>();
            if (!request.Phone.Equals(null))
            {
                foreach(var f in request.Phone)
                {
                    var phone = new Phone
                    {
                        CountryCode = f.CountryCode,
                        AreaCode = f.AreaCode,
                        PhoneNumber = f.PhoneNumber,
                        Extension = f.Extension,
                        PhoneType = (PhoneType)f.PhoneType,
                        ValidFrom = (f.ValidFrom.Equals(null)) ? null : new DateTime?(Convert.ToDateTime(f.ValidFrom)),
                        ValidTo = (f.ValidTo.Equals(null)) ? null : new DateTime?(Convert.ToDateTime(f.ValidTo)),
                    };
                    phones.Add(phone);
                }
            }
            

            Person person = new Person
            {
                PersonNumber = request.PersonNumber,
                RoleId =request.RoleId,
                Category = request.Category,
                PersonLegalPerson = request.Category == PersonCategory.Legal ? legalPerson : null,
                PersonNaturalPerson = request.Category == PersonCategory.Natural ? naturalPerson : null,
                Address = addresses,
                IdentificationDocument = identificationDocuments,
                Email = emails,
                Phone = phones,
               
            };
            _context.Persons.Add(person);
            await _context.SaveChangesAsync(cancellationToken);
           
            return person.PersonNumber;
        }

       
    
    }
}
