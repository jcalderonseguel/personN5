using Microsoft.AspNetCore.Mvc;
using Super.EWalletCore.PersonDataManagement.Application.Notifications;

namespace Super.EWalletCore.PersonDataManagement.API.Presenters.Interfaces
{
    public interface IBasePresenter
    {
        IActionResult GetActionResult<T, Y>(T result)
            where T : EntityResult<Y>
            where Y : class;
        IActionResult GetActionResult<T>(T result) where T : Result;
    }
}