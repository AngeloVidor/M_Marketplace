namespace Application.DTOs
{
    public record CreateCustomerProfileDto(
        Guid UserId,
        string FirstName,
        string LastName,
        string Phone,
        string Street,
        string Number,
        string City,
        string State,
        string Country,
        string ZipCode,
        string Complement = "",
        string Neighborhood = ""
    );
}
