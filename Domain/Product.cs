namespace Domain.Entities;

public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }

    // Rich domain: regras de negócio e métodos
    public Product(string name, decimal price)
    {
        SetName(name);
        SetPrice(price);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty");
        Name = name;
    }

    public void SetPrice(decimal price)
    {
        if (price < 0)
            throw new ArgumentException("Price must be non-negative");
        Price = price;
    }

    // Construtor para EF
    private Product() { }
}
