using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Super.EWalletCore.PersonDataManagement.Application.Common.Helpers;
using Super.EWalletCore.PersonDataManagement.Application.Common.Interfaces;
using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using System;
using System.Threading;

namespace Super.EWalletCore.PersonDataManagement.Application.Validations
{
    public class IdentificationDocumentValidator : AbstractValidator<IdentificationDocument>
    {
        private readonly IClientDbContext _context;

        public IdentificationDocumentValidator(IClientDbContext context, CancellationToken cancellationToken)
        {
            _context = context;

            RuleFor(x => x.DocumentNumber).NotEmpty().WithMessage("DocumentNumber is required");
            RuleFor(x => x.IdentificationDocumentTypeId).NotEmpty().WithMessage("IdentificationDocumentType is required");
            RuleFor(x => x.ValidFrom).NotEmpty().WithMessage("ValidFrom is required");

            RuleFor(x => x.DocumentNumber).NotNull().MustAsync(async (documentNumber, cancellation) =>
            {
                cancellation = cancellationToken;
                return await _context.IdentificationDocument.AllAsync(x => x.DocumentNumber != documentNumber);
            }).WithMessage(x => $"DocumentNumber:{x.DocumentNumber} is exists.");

            RuleFor(x => x.IdentificationDocumentTypeId).NotNull().MustAsync(async (identificationDocumentTypeId, cancellation) =>
            {
                cancellation = cancellationToken;
                return await _context.IdentificationDocumentType.AnyAsync(x => x.Id == identificationDocumentTypeId);
            }).WithMessage(x => $"IdentitficationDocumentType with Id:{x.IdentificationDocumentTypeId} does not exists.");

            RuleFor(x => x.IssuingDate).NotNull().Must((IssuingDate) =>
            {
                return (IssuingDate.Equals("")) ? true : CommonHelper.DateFormat(IssuingDate.ToString());
            }).WithMessage("IssuingDate: Date Format must be dd/mm/yyyy hh:mm or dd-mm-yyyyy hh:mm or yyyy/mm/dd hh:mm or yyyy-mm-dd hh:mm");

            RuleFor(x => x.ExpiryDate).NotNull().Must((ExpiryDate) =>
            {
                return (ExpiryDate.Equals("")) ? true : CommonHelper.DateFormat(ExpiryDate.ToString());
            }).WithMessage("ExpiryDate: Date Format must be dd/mm/yyyy hh:mm or dd-mm-yyyyy hh:mm or yyyy/mm/dd hh:mm or yyyy-mm-dd hh:mm");

            RuleFor(x => x.ValidFrom).NotNull().Must((ValidFrom) =>
            {
                return (ValidFrom.Equals("")) ? true : CommonHelper.DateFormat(ValidFrom.ToString());
            }).WithMessage("ValidFrom: Date Format must be dd/mm/yyyy hh:mm or dd-mm-yyyyy hh:mm or yyyy/mm/dd hh:mm or yyyy-mm-dd hh:mm");

            RuleFor(x => x.ValidTo).NotNull().Must((ValidTo) =>
            {
                return (ValidTo.Equals("")) ? true : CommonHelper.DateFormat(ValidTo.ToString());
            }).WithMessage("ValidTo: Date Format must be dd/mm/yyyy hh:mm or dd-mm-yyyyy hh:mm or yyyy/mm/dd hh:mm or yyyy-mm-dd hh:mm");

            RuleFor(x => x).Must((identificationDocument) =>
            {
                int result = DateTime.Compare(Convert.ToDateTime(identificationDocument.IssuingDate), Convert.ToDateTime(identificationDocument.ExpiryDate));
                return (result > 0) ? false : true;

            }).WithMessage("ExpireDate must be >= IssuingDate")
            .When(x => CommonHelper.DateFormat(x.IssuingDate.ToString()) && CommonHelper.DateFormat(x.ExpiryDate.ToString()));

            RuleFor(x => x).Must((identificationDocument) =>
            {
                int result = DateTime.Compare(Convert.ToDateTime(identificationDocument.ValidFrom), Convert.ToDateTime(identificationDocument.ValidTo));
                return (result > 0) ? false : true;

            }).WithMessage("ValidTo must be >= ValidFrom")
            .When(x => CommonHelper.DateFormat(x.ValidFrom.ToString()) && CommonHelper.DateFormat(x.ValidTo.ToString()));

        }
    }
}
