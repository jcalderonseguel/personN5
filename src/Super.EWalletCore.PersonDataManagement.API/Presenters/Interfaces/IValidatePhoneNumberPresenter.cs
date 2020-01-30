using Microsoft.AspNetCore.Mvc;
using Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.ValidatePhoneNumber;
using Super.EWalletCore.PersonDataManagement.Application.Notifications;

namespace Super.EWalletCore.PersonDataManagement.API.Presenters
{
    public interface IValidatePhoneNumberPresenter
    {
        IActionResult GetResult(EntityResult<ValidatePhoneNumberDto> result);

        IActionResult GetResultFromBackoffice(EntityResult<ValidatePhoneNumberDto> result);
    }
}
