using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.UseCases;
using Application.Security;
using M_API.Application.UseCases;

[ApiController]
[Route("api/vendor-profiles")]
[Authorize]
public class VendorProfilesController : ControllerBase
{
    private readonly CreateVendorProfileUseCase _createVendor;
    private readonly GetAllVendorProfilesUseCase _getAllUseCase;
    private readonly UpdateVendorProfileUseCase _updateUseCase;
    private readonly DeleteVendorProfileUseCase _deleteUseCase;
    private readonly UpdateVendorProfileUseCase _updateVendor;

    public VendorProfilesController(
        CreateVendorProfileUseCase createVendor,
        GetAllVendorProfilesUseCase getAllUseCase,
        UpdateVendorProfileUseCase updateUseCase,
        DeleteVendorProfileUseCase deleteUseCase
,
        UpdateVendorProfileUseCase updateVendor)
    {
        _createVendor = createVendor;
        _getAllUseCase = getAllUseCase;
        _updateUseCase = updateUseCase;
        _deleteUseCase = deleteUseCase;
        _updateVendor = updateVendor;
    }

    [HttpPost]
    [Authorize(Roles = "Vendor")]
    public async Task<IActionResult> Create([FromBody] CreateVendorProfileDto dto)
    {
        var userId = ClaimsHelper.GetUserId(User);
        var dtoWithUser = dto with { UserId = userId };
        var onboardingUrl = await _createVendor.ExecuteAsync(dtoWithUser);

        return Ok(new { message = "Vendor profile created successfully.", onboardingUrl });
    }

    [HttpGet]
    [Authorize(Roles = "Admin, Vendor, Customer")]
    public async Task<IActionResult> GetAll()
    {
        var vendors = await _getAllUseCase.ExecuteAsync();
        return Ok(vendors);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Vendor")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateVendorProfileDto dto)
    {
        var userId = ClaimsHelper.GetUserId(User);
        if (userId != id) return Forbid(); // só o dono pode atualizar

        await _updateVendor.ExecuteAsync(id, dto);
        return Ok(new { message = "Vendor profile updated successfully." });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Vendor, Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _deleteUseCase.ExecuteAsync(id);
        return Ok(new { message = "Vendor profile deleted successfully." });
    }

}
