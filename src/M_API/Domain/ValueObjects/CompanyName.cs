namespace Domain.ValueObjects
{
    public class CompanyName
    {
        public string Value { get; private set; }

        public CompanyName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Company name cannot be empty.");

            Value = value;
        }
    }
}
