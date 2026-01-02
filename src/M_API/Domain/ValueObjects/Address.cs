namespace Domain.ValueObjects
{
    public class Address
    {
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; } = "";
        public string Neighborhood { get; private set; } = "";
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }

        public Address(
            string street,
            string number,
            string city,
            string state,
            string country,
            string zipCode,
            string complement = "",
            string neighborhood = "")
        {
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentException("Street cannot be empty.");
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Number cannot be empty.");
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City cannot be empty.");
            if (string.IsNullOrWhiteSpace(state))
                throw new ArgumentException("State cannot be empty.");
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country cannot be empty.");
            if (string.IsNullOrWhiteSpace(zipCode))
                throw new ArgumentException("Zip code cannot be empty.");

            Street = street;
            Number = number;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        public override string ToString()
        {
            return $"{Street}, {Number}" +
                   $"{(string.IsNullOrWhiteSpace(Complement) ? "" : $", {Complement}")}" +
                   $"{(string.IsNullOrWhiteSpace(Neighborhood) ? "" : $", {Neighborhood}")}, " +
                   $"{City} - {State}, {Country}, {ZipCode}";
        }
    }
}
