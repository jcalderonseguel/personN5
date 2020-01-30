using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Super.EWalletCore.PersonDataManagement.API.Model
{
    public class ExistDto
    {
        public ExistDto(bool exist)
        {
            Exist = exist;
        }
        public bool Exist { get; set; }
    }
}
