using BookEnd.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookEnd.Areas.Identity.Data
{
    public class ApplicationRoleManager : RoleManager<AplicationRole>, IApplicationRoleManager
    {


        private readonly IdentityErrorDescriber _errors;
        private readonly ILookupNormalizer _keyNormalizer;
        private readonly ILogger<ApplicationRoleManager> _logger;
        private readonly IEnumerable<IRoleValidator<AplicationRole>> _roleValidators;
        private readonly IRoleStore<AplicationRole> _store;

        public ApplicationRoleManager(
            IRoleStore<AplicationRole> store,
            ILookupNormalizer keyNormalizer,
            ILogger<ApplicationRoleManager> logger,
            IEnumerable<IRoleValidator<AplicationRole>> roleValidators,
            IdentityErrorDescriber errors) :
            base(store, roleValidators, keyNormalizer, errors, logger)
        {
            _errors = errors;
            _keyNormalizer = keyNormalizer;
            _logger = logger;
            _store = store;
            _roleValidators = roleValidators;
        }


        public List<AplicationRole> GetAllRoles()
        {
            return Roles.ToList();
        }


        public List<RoleManagerViewModel> GetAllRolesAndUsersCount()
        {
            return Roles.Select(role =>
                             new RoleManagerViewModel
                             {
                                 IdR = role.Id,
                                 NameR = role.Name,
                                 DiscriptionR = role.Discription,
                                 UsersCount = role.Users.Count(),
                             }).ToList();
        }

    }
}
