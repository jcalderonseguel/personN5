using GenFu;
using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super.EWalletCore.PersonDataManagement.MockService.Builders
{
    public class RoleMock
    {

        public IEnumerable<Role> CreateRoleList()
        {
            return new List<Role> {
                new Role
                {
                     Id = 1,
                    RoleType = RoleType.Administrator,
                    ValidFrom = DateTime.Now,
                    ValidTo = DateTime.Now.AddYears(1)
                },
                new Role
                {
                     Id = 2,
                    RoleType = RoleType.Client,
                    ValidFrom = DateTime.Now,
                    ValidTo = DateTime.Now.AddYears(1)
                },
                new Role
                {
                     Id = 3,
                    RoleType = RoleType.Employee,
                    ValidFrom = DateTime.Now,
                    ValidTo = DateTime.Now.AddYears(1)
                },
                new Role
                {
                     Id = 4,
                    RoleType = RoleType.Guest,
                    ValidFrom = DateTime.Now,
                    ValidTo = DateTime.Now.AddYears(1)
                }
            };
        }
    }
}
