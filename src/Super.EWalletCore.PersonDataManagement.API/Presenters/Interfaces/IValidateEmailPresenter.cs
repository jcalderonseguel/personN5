using Microsoft.AspNetCore.Mvc;
using Super.EWalletCore.PersonDataManagement.Application.Notifications;
using Super.EWalletCore.PersonDataManagement.Application.Queries;

namespace Super.EWalletCore.PersonDataManagement.API.Presenters.Interfaces
{
    public interface IValidateEmailPresenter
    {
        IActionResult GetResult(EntityResult<ValidateEmailDto> result);
        IActionResult GetResultFromBackoffice(EntityResult<ValidateEmailDto> result);
    }
}