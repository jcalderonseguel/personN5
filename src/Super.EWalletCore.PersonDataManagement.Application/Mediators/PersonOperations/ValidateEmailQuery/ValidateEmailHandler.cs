using Flunt.Notifications;
using MediatR;
using Refit;
using Super.EWalletCore.AccountManagement.ApiClients;
using Super.EWalletCore.PersonDataManagement.Application.Notifications;
using Super.EWalletCore.PersonDataManagement.Application.Queries;
using Super.EWalletCore.PersonDataManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.ValidateEmail
{
    public class ValidateEmailHandler : IRequestHandler<ValidateEmailRequest, EntityResult<ValidateEmailDto>>
    {
        private readonly IPersonQuery validateEmailQuery;
        private readonly IAccountService _accountService;

        public ValidateEmailHandler(IPersonQuery validateEmailQuery,
            IAccountService accountService)
        {
            this.validateEmailQuery = validateEmailQuery;
            _accountService = accountService;
        }

        public async Task<EntityResult<ValidateEmailDto>> Handle(ValidateEmailRequest request, CancellationToken cancellationToken)
        {
            ValidateEmailDto personResult = null;

            var result = new EntityResult<ValidateEmailDto>(null);

            if (request.Invalid)
                return new EntityResult<ValidateEmailDto>(request.Notifications, ErrorCode.BadRequest);

            personResult = await validateEmailQuery.GetPersonByEmailAndCountryAsync(request.Email);

            if (personResult is null)
            {
                return new EntityResult<ValidateEmailDto>("The person was not found.", ErrorCode.NotFound);
            }

            try
            {
                var accountResult = await _accountService.ValidateActiveAccountAsync(personResult.PersonID);

                if (!accountResult.HasActiveAccount)
                {
                    return new EntityResult<ValidateEmailDto>("The person doesnt have an active account.",
                        ErrorCode.NotFound);
                }
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                    return new EntityResult<ValidateEmailDto>("The person was not found", ErrorCode.NotFound);
            }

            return new EntityResult<ValidateEmailDto>(personResult);
        }

    }
}