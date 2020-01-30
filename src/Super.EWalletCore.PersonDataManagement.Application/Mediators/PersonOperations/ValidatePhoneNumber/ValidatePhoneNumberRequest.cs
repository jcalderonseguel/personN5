using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Super.EWalletCore.PersonDataManagement.Application.Notifications;


namespace Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.ValidatePhoneNumber
{
    public class ValidatePhoneNumberRequest : Notifiable, IRequest<EntityResult<ValidatePhoneNumberDto>>
    {
        public string CountryCode { get; set; }
        public string AreaCode { get; set; }
        public string PhoneNumber { get; set; }

        public ValidatePhoneNumberRequest(string countryCode, string areaCode, string phoneNumber)
        {
            this.CountryCode = countryCode;
            this.AreaCode = areaCode;
            this.PhoneNumber = phoneNumber;
            Validate();
        }

        private void Validate() => AddNotifications(new Contract()
                .IsNotNullOrEmpty(CountryCode, nameof(CountryCode), "Contry Code is required")
                .IsNotNullOrEmpty(AreaCode, nameof(AreaCode), "Area Code is required.")
                .IsNotNullOrEmpty(PhoneNumber, nameof(PhoneNumber), "Phone number is required.")
            );
    }
}
