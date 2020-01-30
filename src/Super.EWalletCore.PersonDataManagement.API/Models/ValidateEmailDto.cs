namespace Super.EWalletCore.PersonDataManagement.API.Model
{
    public class ValidateEmailDto
    {
        public ValidateEmailDto(long personId)
        {
            PersonID = personId;
        }

        public long PersonID { get; set; }
    }
}