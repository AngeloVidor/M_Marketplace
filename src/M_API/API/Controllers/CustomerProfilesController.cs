using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.UseCases;
using Application.Security;

[ApiController]
[Route("api/customer-profiles")]
[Authorize]
public class CustomerProfilesController : ControllerBase
{
    private readonly CreateCustomerProfileUseCase _createProfile;

    public CustomerProfilesController(CreateCustomerProfileUseCase createProfile)
    {
        _createProfile = createProfile;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerProfileDto dto)
    {
        var userId = ClaimsHelper.GetUserId(User);
        var profileDto = dto with { UserId = userId }; 
        
        await _createProfile.ExecuteAsync(profileDto);

        return Ok(new { message = "Customer profile created successfully." });
    }
}
