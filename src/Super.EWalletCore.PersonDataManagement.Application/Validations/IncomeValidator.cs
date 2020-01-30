using FluentValidation;
using Super.EWalletCore.PersonDataManagement.Application.Common.Helpers;
using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using System;

namespace Super.EWalletCore.PersonDataManagement.Application.Validations
{
    public class IncomeValidator : AbstractValidator<Income>
    {
        public IncomeValidator()
        {

            RuleFor(x => x.ValidFrom).NotEmpty().WithMessage("ValidFrom is required");

            RuleFor(x => x.Currency).NotEqual(Currency.UknownCurrency).WithMessage($"Only valid Currency values {CommonHelper.GetEnumsToString(typeof(Currency))}");

            RuleFor(x => x.Periodicity).NotEqual(Periodicity.UknownPeriodicity).WithMessage($"Only valid periodicity values {CommonHelper.GetEnumsToString(typeof(Periodicity))}");

            RuleFor(x => x.ValidFrom).NotNull().Must((ValidFrom) =>
            {
                return (ValidFrom.Equals("")) ? true : CommonHelper.DateFormat(ValidFrom.ToString());
            }).WithMessage("ValidFrom: Date Format must be dd/mm/yyyy hh:mm or dd-mm-yyyyy hh:mm or yyyy/mm/dd hh:mm or yyyy-mm-dd hh:mm");

            RuleFor(x => x.ValidTo).NotNull().Must((ValidTo) =>
            {
                return (ValidTo.Equals("")) ? true : CommonHelper.DateFormat(ValidTo.ToString());
            }).WithMessage("ValidTo: Date Format must be dd/mm/yyyy hh:mm or dd-mm-yyyyy hh:mm or yyyy/mm/dd hh:mm or yyyy-mm-dd hh:mm");

            RuleFor(x => x).Must((Income) =>
            {
                int result = DateTime.Compare(Convert.ToDateTime(Income.ValidFrom), Convert.ToDateTime(Income.ValidTo));
                return (result > 0) ? false : true;

            }).WithMessage("ValidTo must be >= ValidFrom")
            .When(x => CommonHelper.DateFormat(x.ValidFrom.ToString()) && CommonHelper.DateFormat(x.ValidTo.ToString()));
        }
    }
}
