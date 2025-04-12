using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotesApplication.Entities;
using NotesApplication.Model;
using NotesApplication.Repositories.Notes;
using NotesApplication.Repositories.Users;


namespace NotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new ();
        private readonly IUserRepository _authRepository;
        public AuthController(IUserRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {

            var existingUsername = await _authRepository.GetByUsernameAsync(request.Username);
            if (existingUsername != null)
                return BadRequest("This username is already existing in the database.");
            var hashedPassword = new PasswordHasher<User>()
                .HashPassword(user, request.Password);

            var newUser = new User
            {
                Username = request.Username,
                Password = hashedPassword
            };
            await _authRepository.AddAsync(newUser);
            return Created();
        }



        [HttpPost("login")]
        public async Task<ActionResult<string>> LogIn(UserDto request)
        {
            var exitingUser = await _authRepository.GetByUsernameAsync(request.Username);
            if (exitingUser == null)
                return BadRequest("This username does not exist in the database.");
            // if(exitingUser.Password != request.Password)
            //     return BadRequest("Password is incorrect.");
            if(new PasswordHasher<User>().VerifyHashedPassword(exitingUser, exitingUser.Password, request.Password) == PasswordVerificationResult.Failed)
                return BadRequest("Password is incorrect.");
            return Ok(new { id = exitingUser.Id, token = "authorized" });
        }
    }
}
