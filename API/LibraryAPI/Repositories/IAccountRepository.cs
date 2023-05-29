
using LibraryAPI.EfCore;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Repositories
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<string> SignInAsync(SignInModel model);
    }
}
