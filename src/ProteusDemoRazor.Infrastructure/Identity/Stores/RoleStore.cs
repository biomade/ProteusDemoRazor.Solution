using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proteus.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proteus.Infrastructure.Identity.Stores
{
    //TODO IDENTITY: Step 3b - Create role store
    public class RoleStore : IQueryableRoleStore<Role>
    {
        private bool disposedValue;
        private readonly IdentityDbContext _dbContext;

        
        public RoleStore(IdentityDbContext identityContext)
        {
            _dbContext = identityContext;
        }

        public IQueryable<Role> Roles
        {
            get
            {
                var roles = _dbContext.Roles.Include(r => r.UserRoles).ThenInclude(ur=>ur.User);
                return (IQueryable<Role>)roles;
            }
        }

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            _dbContext.Add(role);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(IdentityResult.Success);
        }

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            _dbContext.Remove(role);
            int i = await _dbContext.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(i == 1 ? IdentityResult.Success : IdentityResult.Failed());
        }

        public async Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (String.IsNullOrEmpty(roleId))
                throw new ArgumentNullException(nameof(roleId));
            if (int.TryParse(roleId, out int id))
            {
                //can't use Include with FindBy so changed
                var roles = await _dbContext.Roles.Include(r => r.UserRoles).ThenInclude(ur=>ur.User).FirstOrDefaultAsync(r=>r.Id == id);

                return roles;
            }
            return await Task.FromResult((Role)null);
        }

        public async Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (String.IsNullOrEmpty(normalizedRoleName))
                throw new ArgumentNullException(nameof(normalizedRoleName));

            //net core no longer evaluates linq in memory on the server, this needs to be broken into 2 parts
            //return await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name.Equals(normalizedRoleName, StringComparison.OrdinalIgnoreCase), cancellationToken);
            return await _dbContext.Roles.Include(r=>r.UserRoles).FirstOrDefaultAsync<Role>(r => r.NormalizedName == normalizedRoleName, cancellationToken);
        }

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            role.NormalizedName = normalizedName;
            return Task.FromResult((object)null);
        }

        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            _dbContext.Update(role);
            int i = await _dbContext.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(i == 1 ? IdentityResult.Success : IdentityResult.Failed());
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbContext?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~RoleStore()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
