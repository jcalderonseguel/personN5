using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Super.EWalletCore.PersonDataManagement.Application.Common.Exceptions;
using Super.EWalletCore.PersonDataManagement.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Super.EWalletCore.PersonDataManagement.Application.Mediators.IdDocumentOperations.Queries
{
    public class GetIdDocumentTypeByCountryQuery : IRequest<IdentificationDocumentVm>
    {
        public int CountryId { get; set; }
        public  GetIdDocumentTypeByCountryQuery(int CountryId)
        {
            this.CountryId = CountryId;
        }

    }

    public class GetIdDocumentTypeByCountryQueryHandler : IRequestHandler<GetIdDocumentTypeByCountryQuery, IdentificationDocumentVm>
    {
        private readonly IClientDbContext _context;
       

        public GetIdDocumentTypeByCountryQueryHandler(IClientDbContext context)
        {
            _context = context;
        }

        public async Task<IdentificationDocumentVm> Handle(GetIdDocumentTypeByCountryQuery request, CancellationToken cancellationToken)
        {

            var list = await _context.IdentificationDocumentType.Where(x => x.CountryId == request.CountryId).Select(x => new IdentificationDocumentDto
            {
                Id = x.Id,
                IdType = x.IdType,
                Description = x.Description,
                CountryId = x.CountryId,
                CountryName = x.Country.Name,
                CheckDigit =(bool)x.CheckDigit,
            }).ToListAsync();

            IdentificationDocumentVm vm = new IdentificationDocumentVm { idDocumentTypeList = list };
            return vm;
           
        }
    }
}
