using FluentValidation;
using Super.EWalletCore.PersonDataManagement.Application.Common.Helpers;
using Super.EWalletCore.PersonDataManagement.Domain.Entities;

namespace Super.EWalletCore.PersonDataManagement.Application.Validations
{
    public class LegalPersonValidator : AbstractValidator<LegalPerson>
    {
        public LegalPersonValidator() {

            RuleFor(x => x.FullName).NotNull().Must((fullName) =>
            {
                return (fullName.Trim().Equals("")) ? true : CommonHelper.OnlyLetters(fullName);
            }).WithMessage("FullName must only contain letters");
        }
    }
}
