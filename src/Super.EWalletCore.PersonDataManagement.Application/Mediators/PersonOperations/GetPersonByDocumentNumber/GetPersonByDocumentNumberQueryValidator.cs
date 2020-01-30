using FluentValidation;

namespace Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.GetPersonByDocumentNumber
{
    public class GetPersonByDocumentNumberQueryValidator: AbstractValidator<GetPersonByDocumentNumberQuery>
    {
        public GetPersonByDocumentNumberQueryValidator()
        {
            RuleFor(x => x.GenderId).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().WithMessage("GenderId is required").Must((t) =>
            {                
                return (t.GetType() == typeof(int)) ? true : false;
            }).WithMessage("GenderId, Should be a Integer value");

            RuleFor(x => x.IdentificationDocumentTypeId).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().WithMessage("IdentificationDocumentTypeId is required").Must((t) =>
            {
                return (t.GetType() == typeof(int)) ? true : false;
            }).WithMessage("IdentificationDocumentTypeId, Should be a Integer value");

            RuleFor(x => x.IdentificationDocumentTypeId).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().WithMessage("CountryId is required").Must((t) =>
            {
                return (t.GetType() == typeof(int)) ? true : false;
            }).WithMessage("CountryId, Should be a Integer value");


            RuleFor(x => x.GenderId).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().WithMessage("DocumentNumber is required");




        }
    }
}
