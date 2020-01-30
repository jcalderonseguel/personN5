using Microsoft.AspNetCore.Mvc;
using Super.EWalletCore.PersonDataManagement.API.Presenters.Interfaces;
using Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.GetPersonByDocumentNumber;
//using Super.EWalletCore.PersonDataManagement.Application.Notifications;
//using System;

namespace Super.EWalletCore.PersonDataManagement.API.Presenters
{
    public class PersonPresenter :  IPersonPresenter
    {
        //public IActionResult GetInsertResult(Result result)
        //{
        //    var urlBuilder =
        //        new UriBuilder()
        //        {
        //            Path = "~/api/clients",
        //            Query = null,
        //        };

        //    Uri uri = urlBuilder.Uri;
        //    string url = urlBuilder.ToString();

        //    return result.Invalid ? base.GetActionResult(result) : new CreatedResult(url, null);
        //}


        public IActionResult GetPersonByDocumentNumber(PersonInfoDto personInfoDto)
        {

            CustomResult result = new CustomResult();
            if (personInfoDto != null )
            {
                result.Content = personInfoDto;
            }
            else
            {
                result.StatusCode = 404;
                result.Message = "No Content";
            }

            return new JsonResult(result);

        }
        public IActionResult InsertResult(long personId)
        {
            CustomResult result = new CustomResult();
            if (personId > 0)
            {
                result.Message = "Created";
                result.StatusCode = 201;
                result.Content = new { personId = personId };
            }

            return new JsonResult(result);
        }
    }
}
