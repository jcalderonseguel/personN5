using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Super.EWalletCore.PersonDataManagement.API.Presenters.Interfaces;
using Super.EWalletCore.PersonDataManagement.Application.Mediators.IdDocumentOperations.Queries;

namespace Super.EWalletCore.PersonDataManagement.API.Controllers
{
    [ApiController]
    [Route("documentTypes")]
    public class DocumentTypeController : BaseController
    {
        private readonly IMediator mediator;
        private readonly IDocumentTypePresenter documentTypePresenter;

        public DocumentTypeController(IMediator mediator, IDocumentTypePresenter documentTypePresenter)
        {
            this.mediator = mediator;
            this.documentTypePresenter = documentTypePresenter;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int countryId)
        {

            //return documentTypePresenter.GetIdDocumentTypeByCountry(await this.mediator.Send(new GetIdDocumentTypeByCountryQuery(countryId)));
            var response = await this.mediator.Send(new GetIdDocumentTypeByCountryQuery(countryId));

            return this.Ok(response);

        }
    }
}
