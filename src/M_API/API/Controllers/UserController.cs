using Application.DTOs;
using Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UsersController : ControllerBase
    {
        private readonly CreateUserUseCase _createUserUseCase;

        public UsersController(CreateUserUseCase createUserUseCase)
        {
            _createUserUseCase = createUserUseCase;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            await _createUserUseCase.ExecuteAsync(dto);
            return Created(string.Empty, null);
        }
    }
}
