using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Super.EWalletCore.PersonDataManagement.Application.Common.Helpers;
using Super.EWalletCore.PersonDataManagement.Application.Common.Interfaces;
using Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.Insert;
using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using System.Threading;

namespace Super.EWalletCore.PersonDataManagement.Application.Validations
{
    public class PersonValidator : AbstractValidator<InsertPerson>
    {
        private readonly IClientDbContext _context;

        public PersonValidator(IClientDbContext context)
        {

            _context = context;

            RuleFor(x => x.PersonNumber).NotNull().MustAsync(async (personNumber, cancellationToken) =>
            {
                return await _context.Persons.AnyAsync(x => x.PersonNumber == personNumber, cancellationToken);
            }).When(x => x.PersonNumber != 0).WithMessage(x => $"Person Number:{x.PersonNumber} does not exists.");

            RuleFor(x => x.Category).NotEqual(PersonCategory.UknownPersonCategory).WithMessage($"Only valid category values {CommonHelper.GetEnumsToString(typeof(PersonCategory))}");

            RuleFor(x => x.RoleId).NotNull().MustAsync(async (roleId, cancellationToken) =>
            {
                return await _context.Role.AnyAsync(x => x.Id == roleId, cancellationToken);
            }).WithMessage(x => $"Role:{x.RoleId} does not exists.");

            RuleFor(x => x.Role).Null().WithMessage("Object Rol, must be Null");

            When(x => x.Category == PersonCategory.Natural, () =>
            {
                RuleFor(x => x.LegalPerson).Null().WithMessage("LegalPerson is must null when Category is NaturalPerson");
                RuleFor(x => x.NaturalPerson).Cascade(CascadeMode.StopOnFirstFailure).NotNull().WithMessage("NaturalPerson is must not be empty").SetValidator(new NaturalPersonValidator(_context, CancellationToken.None));

            }).Otherwise(() =>
            {
                RuleFor(x => x.NaturalPerson).Null().WithMessage("NaturalPerson is must null when Category is LegalPerson");
                RuleFor(x => x.LegalPerson).Cascade(CascadeMode.StopOnFirstFailure).NotNull().WithMessage("LegalPerson is must not be empty").SetValidator(new LegalPersonValidator());
            });

            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is must not be empty");

            RuleFor(x => x.IdentificationDocument).NotEmpty().WithMessage("IdentificationDocument is must not be empty");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is must not be empty");

            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone is must not be empty");

            RuleForEach(x => x.Address).NotNull().SetValidator(new AddressValidator());
            RuleForEach(x => x.IdentificationDocument).SetValidator(new IdentificationDocumentValidator(_context, CancellationToken.None));
            RuleForEach(x => x.Email).NotNull().SetValidator(new EmailValidator());
            RuleForEach(x => x.Phone).NotNull().SetValidator(new PhoneValidator());
        }
    }
}
