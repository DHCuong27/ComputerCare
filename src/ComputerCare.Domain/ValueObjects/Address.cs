namespace ComputerCare.Domain.ValueObjects;

public class Address
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string PostalCode { get; private set; }
    public string Country { get; private set; }

    private Address()
    {
        Street = string.Empty;
        City = string.Empty;
        State = string.Empty;
        PostalCode = string.Empty;
        Country = string.Empty;
    }

    public Address(string street, string city, string state, string postalCode, string country)
    {
        Street = street ?? throw new ArgumentNullException(nameof(street));
        City = city ?? throw new ArgumentNullException(nameof(city));
        State = state ?? throw new ArgumentNullException(nameof(state));
        PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
        Country = country ?? throw new ArgumentNullException(nameof(country));
    }

    public string GetFullAddress()
    {
        return $"{Street}, {City}, {State} {PostalCode}, {Country}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Address other) return false;
        return Street == other.Street &&
               City == other.City &&
               State == other.State &&
               PostalCode == other.PostalCode &&
               Country == other.Country;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Street, City, State, PostalCode, Country);
    }
}
