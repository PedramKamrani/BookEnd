using BookEnd.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookEnd.Areas.Identity.Data
{
    public interface IApplicationRoleManager
    {
        #region BaseClass
        IQueryable<AplicationRole> Roles { get; }
        ILookupNormalizer KeyNormalizer { get; set; }
        IdentityErrorDescriber ErrorDescriber { get; set; }
        IList<IRoleValidator<AplicationRole>> RoleValidators { get; }
        bool SupportsQueryableRoles { get; }
        bool SupportsRoleClaims { get; }
        Task<IdentityResult> CreateAsync(AplicationRole role);
        Task<IdentityResult> DeleteAsync(AplicationRole role);
        Task<AplicationRole> FindByIdAsync(string roleId);
        Task<AplicationRole> FindByNameAsync(string roleName);
        string NormalizeKey(string key);
        Task<bool> RoleExistsAsync(string roleName);
        Task<IdentityResult> UpdateAsync(AplicationRole role);
        Task UpdateNormalizedRoleNameAsync(AplicationRole role);
        Task<string> GetRoleNameAsync(AplicationRole role);
        Task<IdentityResult> SetRoleNameAsync(AplicationRole role, string name);
        #endregion

        #region custmer
        List<AplicationRole> GetAllRoles();
        List<RoleManagerViewModel> GetAllRolesAndUsersCount();
        #endregion
    }
}
