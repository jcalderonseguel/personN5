using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Super.EWalletCore.PersonDataManagement.API.Models;
using Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.GetPersonByDocumentNumber;
using System.Linq;

using Super.EWalletCore.PersonDataManagement.API.Presenters.Interfaces;

using Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.Insert;
using Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.ValidateEmail;

using System.Threading.Tasks;
using Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.PersonExistById;
using Super.EWalletCore.PersonDataManagement.API.Presenters;
using Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.ValidatePhoneNumber;
using Super.EWalletCore.PersonDataManagement.API.Model;

namespace Super.EWalletCore.PersonDataManagement.API.Controllers
{
    [ApiController]
    [Route("persons")]
    public class PersonController : BaseController
    {
        private readonly IMediator mediator;
        private readonly IPersonPresenter personPresenter;
        private readonly IValidateEmailPresenter validateEmailPresenter;
        private readonly IValidatePhoneNumberPresenter validatePhoneNumberPresenter;

        public PersonController(IMediator mediator, IValidateEmailPresenter validateEmailPresenter, IValidatePhoneNumberPresenter validatePhoneNumberPresenter, IPersonPresenter personPresenter)
        {
            this.mediator = mediator;
            this.validateEmailPresenter = validateEmailPresenter;
            this.validatePhoneNumberPresenter = validatePhoneNumberPresenter;
            this.personPresenter = personPresenter;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int countryId, int identificationDocumentTypeId, string documentNumber, int genderId)
        {
            //GetPersonByDocumentNumberQuery request = new GetPersonByDocumentNumberQuery
            //{
            //    CountryId = countryId,
            //    IdentificationDocumentTypeId = identificationDocumentTypeId,
            //    DocumentNumber = documentNumber,
            //    GenderId = genderId
            //};
                      
            return personPresenter.GetPersonByDocumentNumber(await this.mediator.Send(
                new GetPersonByDocumentNumberQuery(documentNumber,identificationDocumentTypeId,countryId, genderId)
                ));
           
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody]InsertPerson person)
        {
            //long response = await this.mediator.Send(person);
            //return this.Ok(response);

            return personPresenter.InsertResult(await this.mediator.Send(person));
        }

        [HttpGet("exist/{personId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PersonExistVm>> ExsitPersonById(long personId)
        {
            return Ok(await this.mediator.Send(new PersonExistByIdQuery { personId = personId }));
        }
      

        /// <summary>
        /// Validate if the email is property of a active account person and return if exists else
        /// return was not found
        /// </summary>
        /// <param name="email">email of the person</param>
        /// <returns></returns>
        [HttpGet]
        [Route("validateemail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ValidateEmail(string email)
        {
            var query = new ValidateEmailRequest(email);

            return validateEmailPresenter.GetResult(await mediator.Send(query));
        }

        /// <summary>
        /// Validate if the email is property of a active account person and return PersonID else
        /// return was not found
        /// </summary>
        /// <param name="email">email of the person</param>
        /// <returns></returns>
        [HttpGet]
        [Route("backoffice/validateemail")]
        [ProducesResponseType(typeof(ValidateEmailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ValidateEmailBackOffice(string email)
        {
            var query = new ValidateEmailRequest(email);
            return validateEmailPresenter.GetResultFromBackoffice(await mediator.Send(query));
        }


        /// <summary>
        /// Validate if a specific phone number is already registered for an active person
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="areaCode"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("validatephonenumber")]
        [ProducesResponseType(typeof(ExistDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ValidatePhoneNumber(string countryCode, string areaCode, string phoneNumber)
        {
            var request = new ValidatePhoneNumberRequest(countryCode, areaCode, phoneNumber);            
            return validatePhoneNumberPresenter.GetResult(await mediator.Send(request));
        }

        /// <summary>
        /// Validate if a specific phone number is already registered for an active person for backoffice
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="areaCode"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("backoffice/validatephonenumber")]
        [ProducesResponseType(typeof(ValidatePhoneNumberDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ValidatePhoneNumberBackoffice(string countryCode, string areaCode, string phoneNumber)
        {
            var request = new ValidatePhoneNumberRequest(countryCode, areaCode, phoneNumber);
            return validatePhoneNumberPresenter.GetResultFromBackoffice(await mediator.Send(request));
        }
    }
}
