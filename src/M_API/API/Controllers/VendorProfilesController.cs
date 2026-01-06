using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.UseCases;
using Application.Security;

[ApiController]
[Route("api/vendor-profiles")]
[Authorize]
public class VendorProfilesController : ControllerBase
{
    private readonly CreateVendorProfileUseCase _createVendor;

    public VendorProfilesController(CreateVendorProfileUseCase createVendor)
    {
        _createVendor = createVendor;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Vendor")]
    public async Task<IActionResult> Create([FromBody] CreateVendorProfileDto dto)
    {
        var userId = ClaimsHelper.GetUserId(User);

        var dtoWithUser = dto with { UserId = userId };
        var response = await _createVendor.ExecuteAsync(dtoWithUser);

        return Ok(new { message = "Vendor profile created successfully.", onboardingUrl = response });
    }

}
