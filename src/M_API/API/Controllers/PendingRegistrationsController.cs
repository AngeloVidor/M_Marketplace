using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.UseCases;
using M_API.Application.UseCases;

namespace API.Controllers
{
    [ApiController]
    [Route("api/pending-registrations")]
    public class PendingRegistrationsController : ControllerBase
    {
        private readonly RegisterPendingUserUseCase _registerUseCase;
        private readonly ActivateUserUseCase _activateUseCase;

        public PendingRegistrationsController(
            RegisterPendingUserUseCase registerUseCase,
            ActivateUserUseCase activateUseCase)
        {
            _registerUseCase = registerUseCase;
            _activateUseCase = activateUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterPendingUserDto dto)
        {
            await _registerUseCase.ExecuteAsync(dto);
            return Ok(new { message = "Activation code sent via email" });
        }

        [HttpPost("activate")]
        public async Task<IActionResult> Activate([FromBody] ActivateUserDto dto)
        {
            await _activateUseCase.ExecuteAsync(dto);
            return Ok(new { message = "User successfully activated" });
        }
    }
}
