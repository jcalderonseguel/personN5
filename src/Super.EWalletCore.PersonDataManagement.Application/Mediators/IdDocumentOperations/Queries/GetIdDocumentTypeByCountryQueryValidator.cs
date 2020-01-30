using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Super.EWalletCore.PersonDataManagement.Application.Common.Interfaces;
using System.Threading;

namespace Super.EWalletCore.PersonDataManagement.Application.Mediators.IdDocumentOperations.Queries
{
    public class GetIdDocumentTypeByCountryQueryValidator : AbstractValidator<GetIdDocumentTypeByCountryQuery>
    {
        private readonly IClientDbContext _context;

        public GetIdDocumentTypeByCountryQueryValidator(IClientDbContext context)
        {
            _context = context;
            RuleFor(x => x.CountryId).NotNull().MustAsync(async (countryId, cancellation) =>
            {
               
                return await _context.Country.AnyAsync(x => x.Id == countryId, cancellation);
            }).WithMessage(x => $"CountryId: {x.CountryId} does not exists.");
        }
    }
}
