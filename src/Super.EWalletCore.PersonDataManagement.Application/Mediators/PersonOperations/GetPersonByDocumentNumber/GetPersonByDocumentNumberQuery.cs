using AutoMapper;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Super.EWalletCore.PersonDataManagement.Application.Common.Interfaces;
using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using Super.EWalletCore.PersonDataManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.GetPersonByDocumentNumber
{
    public class GetPersonByDocumentNumberQuery :  IRequest<PersonInfoDto>
    {
        // public GetPersonByDocumentNumberRequest GetPersonByDocumentNumberRequest { get; }

        //public GetPersonByDocumentNumberQuery(GetPersonByDocumentNumberRequest getPersonByDocumentNumberRequest)
        //{
        //    GetPersonByDocumentNumberRequest = getPersonByDocumentNumberRequest;
        //}

      
        public string DocumentNumber { get; set; }
        public int IdentificationDocumentTypeId { get; set; }
        public int CountryId { get; set; }
        public int GenderId { get; set; }

        public GetPersonByDocumentNumberQuery(string DocumentNumber, int IdentificationDocumentTypeId, int CountryId, int GenderId)
        {
            this.DocumentNumber = DocumentNumber;
            this.IdentificationDocumentTypeId = IdentificationDocumentTypeId;
            this.CountryId = CountryId;
            this.GenderId = GenderId;
    }
    }

    public class GetPersonByDocumentNumberQueryHandler : IRequestHandler<GetPersonByDocumentNumberQuery, PersonInfoDto>
    {
        //private readonly IPersonDataRepository personDataRepository;
        private readonly IClientDbContext _context;
        public GetPersonByDocumentNumberQueryHandler(IClientDbContext context)
        {
           _context = context;
        }

        public async Task<PersonInfoDto> Handle(GetPersonByDocumentNumberQuery request, CancellationToken cancellationToken)
        {

            var p = await _context.Persons.Include(x => x.PersonLegalPerson).Include(x => x.PersonNaturalPerson)
                        .Where(i => i.PersonNaturalPerson.GenderId == request.GenderId &&
                                    i.IdentificationDocument.Any(d => d.IdentificationDocumentType.CountryId == request.CountryId &&
                                    d.IdentificationDocumentTypeId == request.IdentificationDocumentTypeId &&
                                    d.DocumentNumber == request.DocumentNumber)).Select(p => new PersonInfoDto
                                    {
                                        PersonName = p.PersonNaturalPerson.FullName,
                                        PersonNumber = p.PersonNumber,

                                    }).FirstOrDefaultAsync(cancellationToken);

            return p;   

           
        }
    }
}
