using Microsoft.AspNetCore.Mvc;
using Super.EWalletCore.PersonDataManagement.API.Model;
using Super.EWalletCore.PersonDataManagement.API.Models;
using Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.ValidatePhoneNumber;
using Super.EWalletCore.PersonDataManagement.Application.Notifications;

namespace Super.EWalletCore.PersonDataManagement.API.Presenters
{
    public class ValidatePhoneNumberPresenter : BasePresenter, IValidatePhoneNumberPresenter
    {
        public IActionResult GetResult(EntityResult<ValidatePhoneNumberDto> result)
        {
            return result.Invalid ? base.GetActionResult(result) :
             new JsonResult(new ExistDto (  result != null && result.Entity.PersonID > 0 ? true : false ))
             {
                 StatusCode = 200
             };
        }

        public IActionResult GetResultFromBackoffice(EntityResult<ValidatePhoneNumberDto> result)
        {
            return result.Invalid ? base.GetActionResult(result) :
             new JsonResult(result.Entity)
             {
                 StatusCode = 200
             };
        }
    }
}
