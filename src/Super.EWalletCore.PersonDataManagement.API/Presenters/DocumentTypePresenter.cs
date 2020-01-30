using Microsoft.AspNetCore.Mvc;
using Super.EWalletCore.PersonDataManagement.API.Presenters.Interfaces;
using Super.EWalletCore.PersonDataManagement.Application.Mediators.IdDocumentOperations.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Super.EWalletCore.PersonDataManagement.API.Presenters
{
    public class DocumentTypePresenter : IDocumentTypePresenter
    {
        public IActionResult GetIdDocumentTypeByCountry(IdentificationDocumentVm identificationDocumentVm)
        {

            CustomResult result = new CustomResult();
            if (identificationDocumentVm != null && identificationDocumentVm.idDocumentTypeList.Any())
            {
                result.Content = identificationDocumentVm.idDocumentTypeList;
            }
            else
            {
                result.StatusCode = 404;
                result.Message = "No Content";
            }

            return new JsonResult(result);

        }
    }
}
