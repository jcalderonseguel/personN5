using System;

namespace Super.EWalletCore.PersonDataManagement.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
             : base($"Object \"{name}\" ({key}) was not found.")
        {
        }
    }
}
