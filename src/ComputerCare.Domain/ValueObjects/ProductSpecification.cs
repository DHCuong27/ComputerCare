namespace ComputerCare.Domain.ValueObjects;

public class ProductSpecification
{
    public Dictionary<string, string> Specifications { get; private set; }

    private ProductSpecification()
    {
        Specifications = new Dictionary<string, string>();
    }

    public ProductSpecification(Dictionary<string, string> specifications)
    {
        Specifications = specifications ?? new Dictionary<string, string>();
    }

    public void AddSpecification(string key, string value)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Key cannot be empty", nameof(key));
        
        Specifications[key] = value;
    }

    public string? GetSpecification(string key)
    {
        return Specifications.TryGetValue(key, out var value) ? value : null;
    }

    public bool HasSpecification(string key)
    {
        return Specifications.ContainsKey(key);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not ProductSpecification other) return false;
        if (Specifications.Count != other.Specifications.Count) return false;

        return Specifications.All(kvp =>
            other.Specifications.TryGetValue(kvp.Key, out var value) && value == kvp.Value);
    }

    public override int GetHashCode()
    {
        return Specifications.Aggregate(0, (hash, kvp) => 
            HashCode.Combine(hash, kvp.Key, kvp.Value));
    }
}
