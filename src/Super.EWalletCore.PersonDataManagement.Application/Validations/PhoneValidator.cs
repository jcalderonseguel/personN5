using FluentValidation;
using Super.EWalletCore.PersonDataManagement.Application.Common.Helpers;
using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using System;

namespace Super.EWalletCore.PersonDataManagement.Application.Validations
{
    public class PhoneValidator : AbstractValidator<Phone>
    {

        public PhoneValidator()
        {

            RuleFor(x => x.PhoneType).NotEqual(PhoneType.UknownPhoneType).WithMessage($"Only valid PhoneType values {CommonHelper.GetEnumsToString(typeof(PhoneType))}");
            RuleFor(x => x.CountryCode).NotEmpty().WithMessage("ContryCode is required");
            RuleFor(x => x.AreaCode).NotEmpty().WithMessage("AreaCode is required");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber is required");

            RuleFor(x => x.PhoneNumber).Must((phoneNumber) =>
            {
                return (phoneNumber.Trim().Equals(""))?true:CommonHelper.IsInt(phoneNumber);
            }).WithMessage("phoneNumber must be a number");

            RuleFor(x => x.ValidFrom).NotEmpty().WithMessage("ValidFrom is required");

            RuleFor(x => x.ValidFrom).NotNull().Must((ValidFrom) =>
            {
                return (ValidFrom.Equals("")) ? true : CommonHelper.DateFormat(ValidFrom.ToString());
            }).WithMessage("ValidFrom: Date Format must be dd/mm/yyyy hh:mm or dd-mm-yyyyy hh:mm or yyyy/mm/dd hh:mm or yyyy-mm-dd hh:mm");

            RuleFor(x => x.ValidTo).NotNull().Must((ValidTo) =>
            {
                return (ValidTo.Equals("")) ? true : CommonHelper.DateFormat(ValidTo.ToString());
            }).WithMessage("ValidTo: Date Format must be dd/mm/yyyy hh:mm or dd-mm-yyyyy hh:mm or yyyy/mm/dd hh:mm or yyyy-mm-dd hh:mm");

            RuleFor(x => x).Must((Phone) =>
            {
                int result = DateTime.Compare(Convert.ToDateTime(Phone.ValidFrom), Convert.ToDateTime(Phone.ValidTo));
                return (result > 0) ? false : true;

            }).WithMessage("ValidTo must be >= ValidFrom")
            .When(x => CommonHelper.DateFormat(x.ValidFrom.ToString()) && CommonHelper.DateFormat(x.ValidTo.ToString()));
        }
    }
}
