using System;

namespace Application.DTOs
{
    public record VendorSalesDto(
        Guid VendorProfileId,
        int TotalItemsSold,
        int ItemsSoldThisMonth,
        int ItemsSoldThisYear,
        decimal TotalRevenue
    );
}
