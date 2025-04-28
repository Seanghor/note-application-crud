using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Entities;
using WebApplication2.Models;
using WebApplication2.Repository.UserRepository;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserRepository _authRepository;
        public static UserEntity user = new();
        public AuthController(IUserRepository authRepository)
        {
            _authRepository = authRepository;
        }

        // -- Register
        [HttpPost("register", Name ="Register")]
        public async Task<ActionResult<UserEntity>> Register(RegisterUserDto request)
        {
            var existingUser = await _authRepository.GetByUsernameAsync(request.Username);
            if(existingUser != null)
                return BadRequest("This username is already existing in the database.");
            var hasedPassword = new PasswordHasher<UserEntity>().HashPassword(user, request.Password);
            var newUser = new UserEntity
            {
                Username = request.Username,
                Password = hasedPassword
            };
            await _authRepository.AddAsync(newUser);
            return Created();
        }

        // -- Login
        [HttpPost("login", Name = "Login")]
        public async Task<ActionResult<AuthResponse>> login(LoginUserDto request)
        {
            var user = await _authRepository.GetByUsernameAsync(request.Username);
            if(user == null)
                return BadRequest("User not found in the database.");

            bool isInValidPassword = new PasswordHasher<UserEntity>().VerifyHashedPassword(user, user.Password, request.Password) == PasswordVerificationResult.Failed;
            if(isInValidPassword)
                return BadRequest("Password is incorrect.");
            return Ok(new {id = user.Id, token = "authorized" });
        }
    }
}
