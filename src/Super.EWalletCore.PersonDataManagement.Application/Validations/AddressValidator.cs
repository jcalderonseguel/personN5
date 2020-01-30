using FluentValidation;
using Super.EWalletCore.PersonDataManagement.Application.Common.Helpers;
using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using System;

namespace Super.EWalletCore.PersonDataManagement.Application.Validations
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {

            RuleFor(x => x.AddressType).NotEqual(AddressType.UknownAddressType).WithMessage($"Only valid addressType values {CommonHelper.GetEnumsToString(typeof(AddressType))}");
            RuleFor(x => x.StatusCodeAddress).NotEqual(StatusCodeAddress.UknownStatusCode).WithMessage($"Only valid statusCodeAddress values {CommonHelper.GetEnumsToString(typeof(StatusCodeAddress))}");
            RuleFor(x => x.PostCode).NotEmpty().WithMessage("PostCode is required");
            RuleFor(x => x.ValidFrom).NotEmpty().WithMessage("ValidFrom is required");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required");

            RuleFor(x => x.Country).NotNull().Must((country) =>
            {
                return (country.Trim().Equals(""))?true:CommonHelper.OnlyLetters(country);
            }).WithMessage("Country must only contain letters");

            RuleFor(x => x.City).NotNull().Must((city) =>
            {
                return (city.Trim().Equals("")) ? true : CommonHelper.OnlyLetters(city);
            }).WithMessage("City must only contain letters");

            RuleFor(x => x.ValidFrom).NotNull().Must((ValidFrom) =>
            {
                return (ValidFrom.Equals("")) ? true : CommonHelper.DateFormat(ValidFrom.ToString());
            }).WithMessage("ValidFrom: Date Format must be dd/mm/yyyy hh:mm or dd-mm-yyyyy hh:mm or yyyy/mm/dd hh:mm or yyyy-mm-dd hh:mm");

            RuleFor(x => x.ValidTo).NotNull().Must((ValidTo) =>
            {
                return (ValidTo.Equals("")) ? true : CommonHelper.DateFormat(ValidTo.ToString());
            }).WithMessage("ValidTo: Date Format must be dd/mm/yyyy hh:mm or dd-mm-yyyyy hh:mm or yyyy/mm/dd hh:mm or yyyy-mm-dd hh:mm");

            RuleFor(x => x).Must((Address) =>
            {
                int result = DateTime.Compare(Convert.ToDateTime(Address.ValidFrom), Convert.ToDateTime(Address.ValidTo));
                return (result > 0) ? false : true;

            }).WithMessage("ValidTo must be >= ValidFrom")
            .When(x => CommonHelper.DateFormat(x.ValidFrom.ToString()) && CommonHelper.DateFormat(x.ValidTo.ToString()));
        }
    }
}
