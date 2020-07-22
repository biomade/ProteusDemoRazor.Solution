using Microsoft.AspNetCore.Identity;
using Proteus.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proteus.Core.Interfaces.Identity
{
    public interface IUserRoleStore
    {
        Task<UserRole> FindByIdAsync(int userRoleId, CancellationToken cancellationToken = default(CancellationToken));
        Task<UserRole> FindByIdAsync(int userId, int roleId, CancellationToken cancellationToken = default(CancellationToken));

        Task<IList<UserRole>> GetUserRolesForRoleAsync(int id, CancellationToken cancellationToken = default(CancellationToken));

        Task<IList<UserRole>> GetUserRolesForUserAsync(int id, CancellationToken cancellationToken = default(CancellationToken));

        Task<IdentityResult> DeleteAsync(UserRole userrole, CancellationToken cancellationToken = default(CancellationToken));

        Task<IList<UserRole>> GetUserRolesAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<IList<User>> GetUsersAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<IList<Role>> GetRolesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
