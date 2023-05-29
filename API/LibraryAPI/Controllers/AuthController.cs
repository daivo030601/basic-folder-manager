using LibraryAPI.EfCore;
using LibraryAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAccountRepository accountRepo;

        public AuthController(IAccountRepository repo)
        {
            accountRepo = repo;
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            var result = await accountRepo.SignUpAsync(model);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return Unauthorized();
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            var result = await accountRepo.SignInAsync(signInModel);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(result);
        }
    }
}
