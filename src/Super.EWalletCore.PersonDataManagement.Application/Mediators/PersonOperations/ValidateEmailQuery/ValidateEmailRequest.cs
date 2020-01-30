using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Super.EWalletCore.PersonDataManagement.Application.Notifications;
using Super.EWalletCore.PersonDataManagement.Application.Queries;

namespace Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.ValidateEmail
{
    public class ValidateEmailRequest : Notifiable, IRequest<EntityResult<ValidateEmailDto>>
    {
        public ValidateEmailRequest(string email)
        {
            Email = email;
            Validate();
        }

        public string Email { get; set; }

        private void Validate() => AddNotifications(new Contract()
                .IsNotNullOrEmpty(Email, nameof(Email), "Email is required.")
                .IsEmail(Email, nameof(Email), "Email format is not valid.")
            );
    }
}