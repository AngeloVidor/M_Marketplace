namespace M_API.Domain.ValueObjects
{
    public class Cnpj
    {
        public string Value { get; private set; }

        public Cnpj(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 14)
                throw new ArgumentException("CNPJ must have 14 digits.");

            Value = value;
        }
    }
}