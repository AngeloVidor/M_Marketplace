namespace Application.DTOs
{
    public record UpdateCustomerProfileDto(
           Guid Id,
           string FullName,
           string Address,
           string Phone
       );
}