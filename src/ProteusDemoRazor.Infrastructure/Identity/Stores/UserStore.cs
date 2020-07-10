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
    //TODO IDENTITY: Step 3a - Create User store 
    public class UserStore : IUserStore<User>, IUserPasswordStore<User>, IUserRoleStore<User>,IUserEmailStore<User>
    {
        private bool disposedValue;
        private readonly IdentityDbContext _dbContext;
        public UserStore(IdentityDbContext identityContext)
        {
            _dbContext = identityContext;
        }

        #region users
        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _dbContext.Add(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(IdentityResult.Success);
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            //What about removing roles? 
            _dbContext.Remove(user);
            int i = await _dbContext.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(i == 1 ? IdentityResult.Success : IdentityResult.Failed());
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (String.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));

            if (int.TryParse(userId, out int id))
            {
                var users = await _dbContext.Users.FindAsync(id);
                //return =await users.FindAsync(id);
                return users;
            }
            return await Task.FromResult((User)null);
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (String.IsNullOrEmpty(normalizedUserName))
                throw new ArgumentNullException(nameof(normalizedUserName));
            var user =   await _dbContext.Users.SingleOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName, cancellationToken);
            return user;
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.NormalizedUserName = normalizedName;
            return Task.FromResult((object)null);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _dbContext.Update(user);
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
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UserStore()
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

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.PasswordHash = passwordHash;
            return Task.FromResult((object)null);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        #endregion users
        
        #region userRoles

        public Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            //get role based upon roleName
            var role = _dbContext.Roles.FirstOrDefault(r => r.NormalizedName == roleName.ToUpper());
            //add user and role id's to userRole table
            var userRole = new UserRole
            {
                RoleId = role.Id,
                UserId = user.Id,
                CreatedDate = System.DateTime.Now
            };
            _dbContext.UserRoles.Add(userRole);
            _dbContext.SaveChangesAsync();

            return Task.FromResult((User)null);
        }

        public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            //get role based upon roleName
            var role = _dbContext.Roles.FirstOrDefault(r => r.Name == roleName);
            //add user and role id's to userRole table
            var userRole = new UserRole
            {
                RoleId = role.Id,
                UserId = user.Id
            };
            _dbContext.UserRoles.Remove(userRole);
            _dbContext.SaveChangesAsync();

            return Task.FromResult((User)null);
        }

        public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            List<UserRole> usersRoles;
            if (user.UserRoles == null)
            {
                //get the user roles and include the roles so we can get the names

                usersRoles = _dbContext.UserRoles.Include(ur => ur.Role).Where(ur => ur.UserId == user.Id).ToList();
            }
            else
            {
                //get roles for the user
                usersRoles = user.UserRoles.ToList();
            }


            //now get the role Names for the role attached to the userrole
            IList<string> roleNames = usersRoles.Select(ur => ur.Role.Name).ToList();

            return Task.FromResult(roleNames);
        }

        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            //first find role with roleName
            var role = _dbContext.Roles.Include(r => r.UserRoles).ToList().Find(r => r.NormalizedName == roleName.ToUpper());
            //now see if userid is in the list
            bool found = role.UserRoles.ToList().Find(u => u.UserId == user.Id) == null ? false : true;
            return Task.FromResult(found);
        }

        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion userRoles

        #region UserEmail

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email); 
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (String.IsNullOrEmpty(normalizedEmail))
                throw new ArgumentNullException(nameof(normalizedEmail));
            var user =  _dbContext.Users.SingleOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail, cancellationToken);
            return user;
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
           // throw new NotImplementedException();
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            // throw new NotImplementedException();
            return Task.FromResult((User)null);
        }

        #endregion UserEmail
    }
}
