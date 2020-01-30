using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Super.EWalletCore.PersonDataManagement.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.PersonExistById
{
   public class PersonExistByIdQuery : IRequest<PersonExistVm>
    {
        public long personId { get; set; }
    }

    public class PersonExistByIdQueryHandler : IRequestHandler<PersonExistByIdQuery, PersonExistVm>
    {
        private readonly IClientDbContext _context;
        public PersonExistByIdQueryHandler(IClientDbContext context)
        {
            _context = context;
        }

        public async Task<PersonExistVm> Handle(PersonExistByIdQuery request, CancellationToken cancellationToken)
        {
            var req = request.personId;
            var existValue = false;
            var exist = await _context.Persons.Where(x => x.PersonNumber == request.personId).FirstOrDefaultAsync();

            if(exist != null)
            {
                existValue = true;
            }
            var vm = new PersonExistVm { exist = existValue };

            return vm;
        }

    }
}
