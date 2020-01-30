using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Super.EWalletCore.PersonDataManagement.Application.Common.Helpers;
using Super.EWalletCore.PersonDataManagement.Application.Common.Interfaces;
using Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.Insert;
using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using System;
using System.Threading;

namespace Super.EWalletCore.PersonDataManagement.Application.Validations
{
    public class NaturalPersonValidator : AbstractValidator<NaturalPerson>
    {
        private readonly IClientDbContext _context;

        public NaturalPersonValidator(IClientDbContext context, CancellationToken cancellationToken)
        {
            _context = context;
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required");
            RuleFor(x => x.FirstName).NotNull().Must((firstName) =>
            {
                return (firstName.Trim().Equals("")) ? true : CommonHelper.OnlyLetters(firstName);
            }).WithMessage("FirstName must only contain letters");
            
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required");
            RuleFor(x => x.LastName).NotNull().Must((lastName) =>
            {
                return (lastName.Trim().Equals("")) ? true : CommonHelper.OnlyLetters(lastName);
            }).WithMessage("LastName must only contain letters");

            RuleFor(x => x.LastNamePrefix).NotNull().Must((lastNamePrefix) =>
            {
                return (lastNamePrefix.Trim().Equals("")) ? true : CommonHelper.OnlyLetters(lastNamePrefix);
            }).WithMessage("LastNamePrefix must only contain letters");

            RuleFor(x => x.FullName).NotNull().Must((fullName) =>
            {
                return (fullName.Trim().Equals("")) ? true : CommonHelper.OnlyLetters(fullName);
            }).WithMessage("FullName must only contain letters");

            RuleFor(x => x.BirthDate).NotEmpty().WithMessage("BirthDate is required");
            RuleFor(x => x.GenderId).NotEmpty().WithMessage("Gender is required");
            RuleFor(x => x.GenderId).NotNull().MustAsync(async (genderId, cancellation) =>
            {
                cancellation = cancellationToken;
                return await _context.Gender.AnyAsync(x => x.Id == genderId);
            }).WithMessage(x => $"Gender with Id:{x.GenderId} does not exists.");

            RuleFor(x => x.MaritalStatus).NotEqual(MaritalStatus.UknownMaritalStatus).WithMessage($"Only valid Martial Status values {CommonHelper.GetEnumsToString(typeof(MaritalStatus))}");
          
            RuleFor(x => x.Nationality).NotEqual(Nationality.UknownNationality).WithMessage($"Only valid Nationality values {CommonHelper.GetEnumsToString(typeof(Nationality))}");

            RuleFor(x => x.BirthDate).NotNull().Must((birtDate) =>
            {
                return (birtDate.Equals(null)) ? true : CommonHelper.DateFormat(birtDate.ToString());
            }).WithMessage("BirthDate: Date Format must be dd/mm/yyyy hh:mm or dd-mm-yyyyy hh:mm or yyyy/mm/dd hh:mm or yyyy-mm-dd hh:mm");

            RuleFor(x => x.DateOfDeath).NotNull().Must((DateOfDeath) =>
            {
                return (DateOfDeath.Equals(null)) ? true : CommonHelper.DateFormat(DateOfDeath.ToString());
            }).WithMessage("DateOfDeath: Date Format must be dd/mm/yyyy hh:mm or dd-mm-yyyyy hh:mm or yyyy/mm/dd hh:mm or yyyy-mm-dd hh:mm");

            RuleFor(x => x).Must((naturalPerson) =>
            {
                int result = DateTime.Compare(Convert.ToDateTime(naturalPerson.BirthDate), Convert.ToDateTime(naturalPerson.DateOfDeath));
                return (result > 0) ? false : true;

            }).WithMessage("DateOfDeath must be >= BirthDate")
            .When(x => CommonHelper.DateFormat(x.BirthDate.ToString()) && CommonHelper.DateFormat(x.DateOfDeath.ToString()));

            RuleFor(x => x.Income).NotEmpty().WithMessage("Income is must not be empty");

            RuleForEach(x => x.Income).SetValidator(new IncomeValidator());

        }
    }
}
