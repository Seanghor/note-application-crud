using Microsoft.AspNetCore.Mvc;
using NotesApplication.Entities;
using NotesApplication.Model;
using NotesApplication.Repositories.Users;

namespace NotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //-- Get by Id
        [HttpGet("{id}", Name = "GetOneById")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound("This user does not exist in the database.");
            return Ok(user);
        }
    }
}
