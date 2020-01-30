using Microsoft.AspNetCore.Mvc;
using Super.EWalletCore.PersonDataManagement.API.Presenters.Interfaces;
using Super.EWalletCore.PersonDataManagement.Application.Notifications;
using Super.EWalletCore.PersonDataManagement.Application.Queries;

namespace Super.EWalletCore.PersonDataManagement.API.Presenters
{
    public class ValidateEmailPresenter : BasePresenter, IValidateEmailPresenter
    {
        public IActionResult GetResult(EntityResult<ValidateEmailDto> result)
        {
            return result.Invalid ? base.GetActionResult(result) :
                    new JsonResult(new Model.ExistDto(result != null && result.Entity.PersonID > 0 ? true : false))
                    {
                        StatusCode = 200
                    };
        }

        public IActionResult GetResultFromBackoffice(EntityResult<ValidateEmailDto> result)
        {
            var personId = result.Entity != null ? result.Entity.PersonID : 0;

            return result.Invalid ? base.GetActionResult(result) :
                new JsonResult(result.Entity)
                {
                    StatusCode = 200
                };
        }
    }
}
