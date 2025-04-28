using Microsoft.AspNetCore.Mvc;
using WebApplication2.Entities;
using WebApplication2.Repository.UserRepository;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet("{id}", Name= "GetOneUserById")]
        public async Task<ActionResult<UserEntity>> GetbyId(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpGet(Name = "GetAllUser")]
        public async Task<ActionResult<List<UserEntity>>> GetAll()
        {
            var users = await _userRepository.GetAllAsync();
            if (users == null || users.Count == 0)
                return Ok(new List<UserEntity>());
            return Ok(users);
        }
    }
}
