using MediatR;
using Refit;
using Super.EWalletCore.AccountManagement.ApiClients;
using Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.ValidatePhoneNumber;
using Super.EWalletCore.PersonDataManagement.Application.Notifications;
using Super.EWalletCore.PersonDataManagement.Domain.Repositories;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Super.EWalletCore.PersonDataManagement.Application.Mediators.ValidatePhoneNumber.Query
{
    public class ValidatePhoneNumberHandler : IRequestHandler<ValidatePhoneNumberRequest, EntityResult<ValidatePhoneNumberDto>>
    {
        private readonly IPersonQuery personQuery;
        private readonly IAccountService _accountService;

        public ValidatePhoneNumberHandler(IPersonQuery personQuery, IAccountService accountService)
        {
            this.personQuery = personQuery;
            _accountService = accountService;
        }

        public async Task<EntityResult<ValidatePhoneNumberDto>> Handle(ValidatePhoneNumberRequest request, CancellationToken cancellationToken)
        {

            if (request.Invalid)
                return new EntityResult<ValidatePhoneNumberDto>(request.Notifications, ErrorCode.BadRequest);

            var entity = await personQuery.GetPersonByPhoneNumberAsync(request.CountryCode, request.AreaCode, request.PhoneNumber);
            
            if (entity is null)
            {
                return new EntityResult<ValidatePhoneNumberDto>("The person was not found", ErrorCode.NotFound );
            }

            try
            {
                var accountResult = await _accountService.ValidateActiveAccountAsync(entity.PersonID);

                if (!accountResult.HasActiveAccount)
                {
                    return new EntityResult<ValidatePhoneNumberDto>("The person doesnt have an active account.",
                        ErrorCode.NotFound);
                }
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                    return new EntityResult<ValidatePhoneNumberDto>("The person was not found", ErrorCode.NotFound);
            }

            return new EntityResult<ValidatePhoneNumberDto>(new ValidatePhoneNumberDto { PersonID = entity.PersonID });
        }
    }
}
