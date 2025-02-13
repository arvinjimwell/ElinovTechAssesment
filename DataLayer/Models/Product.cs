namespace DataLayer.Models;


public class Product
{
    public int ProductId { get; set; }
    public required string Name { get; set; }
    public uint QuantityInStock { get; set; }
    public decimal Price { get; private set; }


    [System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
    public Product(string name, uint quantity, decimal price)
    {
        Name = name;
        QuantityInStock = quantity;
        SetPrice(price);
    }


    /// <summary>
    /// Set value of the product price.
    /// </summary>
    public void SetPrice(decimal price)
    {
        // Will throw an exception if the price is less than zero.
        if(price < 0)
            throw new ArgumentException("Non-negative real numbers are not allowed.");

        Price = price;
    }
}
