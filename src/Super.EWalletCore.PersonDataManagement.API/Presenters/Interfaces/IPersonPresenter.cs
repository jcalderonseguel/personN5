using Microsoft.AspNetCore.Mvc;
using Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.GetPersonByDocumentNumber;
using Super.EWalletCore.PersonDataManagement.Application.Notifications;

namespace Super.EWalletCore.PersonDataManagement.API.Presenters.Interfaces
{
    public interface IPersonPresenter
    {

        IActionResult GetPersonByDocumentNumber(PersonInfoDto personInfoDto);
        IActionResult InsertResult(long personId);

        //IActionResult GetInsertResult(Result result);
        //IActionResult GetListResult(IEnumerable<MonthlyIncomeDataDto> list);
        //IActionResult GetResult(EntityResult<ClientVm> result);
    }
}
