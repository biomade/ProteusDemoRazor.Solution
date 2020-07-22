using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proteus.Core.Entities.Identity;
using Proteus.Core.Interfaces.Identity;
using Proteus.Infrastructure.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proteus.Infrastructure.Identity.Stores
{
    //100% custom since IUserRoleStore mostly works with a user object 
    //and the IUserStore interface is part of the UserStore
    public class UserRoleStore  : IUserRoleStore
    {
        private bool disposedValue;
        private readonly IdentityDbContext _dbContext;


        public UserRoleStore(IdentityDbContext dbContext )
        {
            _dbContext = dbContext;
        }


        public async Task<IList<UserRole>> GetUserRolesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            var userRoles = await _dbContext.UserRoles
              .Include(u => u.Role)
              .Include(u => u.User).ToListAsync();
            return userRoles;
        }

        public async Task<IList<UserRole>> GetUserRolesForRoleAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (id == 0)
                throw new ArgumentNullException(nameof(id));

            var userRoles = await _dbContext.UserRoles
               .Include(u => u.Role)
               .Include(u => u.User).Where(ur => ur.RoleId == id).ToListAsync();
            return userRoles;
        }

        public async Task<IList<UserRole>> GetUserRolesForUserAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (id == 0)
                throw new ArgumentNullException(nameof(id));

            var userRoles = await _dbContext.UserRoles
               .Include(u => u.Role)
               .Include(u => u.User).Where(ur => ur.UserId == id).ToListAsync();
            return userRoles;
        }

        public async Task<UserRole> FindByIdAsync(int userRoleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (userRoleId == 0)
                throw new ArgumentNullException(nameof(userRoleId));

            var userRole = await _dbContext.UserRoles
                 .Include(u => u.Role)
                 .Include(u => u.User).FirstOrDefaultAsync(m => m.Id == userRoleId);

            return userRole;
        }

        public async Task<UserRole> FindByIdAsync(int userId, int roleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (userId == 0)
                throw new ArgumentNullException(nameof(userId));

            var userRole = await  _dbContext.UserRoles.FirstOrDefaultAsync(ur => ur.RoleId == roleId && ur.UserId == userId);

            return userRole;
        }

        public async Task<IdentityResult> CreateAsync(UserRole userrole, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (userrole == null)
                throw new ArgumentNullException(nameof(userrole));

            _dbContext.Add(userrole);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(IdentityResult.Success);
        }

        public async Task<IdentityResult> DeleteAsync(UserRole userrole, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (userrole == null)
                throw new ArgumentNullException(nameof(userrole));

            _dbContext.UserRoles.Add(userrole);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(IdentityResult.Success);
        }


        public async Task<IList<User>> GetUsersAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            var userRoles = await _dbContext.Users
             .ToListAsync();
            return userRoles;
        }

        public async Task<IList<Role>> GetRolesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var userRoles = await _dbContext.Roles
             .ToListAsync();
            return userRoles;
        }


        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbContext?.Dispose();
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }
    }
}
