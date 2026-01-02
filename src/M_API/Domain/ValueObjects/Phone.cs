public class Phone
{
    public string Number { get; private set; }

    public Phone(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Phone cannot be empty.");
        Number = number;
    }
}