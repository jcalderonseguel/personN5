using Microsoft.AspNetCore.Mvc;
using Super.EWalletCore.PersonDataManagement.Application.Mediators.IdDocumentOperations.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Super.EWalletCore.PersonDataManagement.API.Presenters.Interfaces
{
   public interface IDocumentTypePresenter
    {
        //<IdentificationDocumentVm
        IActionResult GetIdDocumentTypeByCountry(IdentificationDocumentVm identificationDocumentVm);
    }
}
