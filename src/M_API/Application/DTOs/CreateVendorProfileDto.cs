namespace Application.DTOs
{
    public record CreateVendorProfileDto
    (
        Guid UserId,
        string FirstName,
        string LastName,
        string CompanyName,
        string Cnpj,
        string Phone,
        string Street,
        string Number,
        string City,
        string State,
        string Country,
        string ZipCode,
        string Complement,
        string Neighborhood
    );
}
