using DataLayer.Models;
using ConsoleTables; // Nugget Package to write a good table to the console.

namespace ServiceLayer;

public class InventoryManager
{
    private int _idMax = 0; /// The highest id number given to the product.
    private int _productsCount = 0; // The number of product in the inventory.
    private ICollection<Product> _products = []; /// The container for the products.

    /// <summary>
    /// Add the product to the inventory.
    /// Will return true if the product is succesfully added, else return false.
    /// </summary>
    public bool AddProduct(Product product)
    {
        // This will not allow a product with the same name in the inventory.
        string[] productNames = _products.Select(x => x.Name).ToArray();
        if(productNames.Contains(product.Name))
        {
            // I wan't to throw it as an exception but I think it will be
            // expensive to do that since a lot of try-catch will be written.
            Console.WriteLine("Product name is already exist in the inventory.");
            return false;
        }

        // We increment the _idMax and set it as
        // the value in the new product.
        product.ProductId = ++_idMax;
        _products.Add(product);

        // Checked if the product is succesfuly Added to the inventory.
        // This code is useless, but it just simulate if the _products is
        // added in a real database.
        return IsProductAddedOrRemoved();
    }


    /// <summary>
    /// Remove the product to the inventory.
    /// </summary>
    public bool RemoveProduct(int productId)
    {
        // Query the product in the inventory.
        Product? product = _products.FirstOrDefault(x => x.ProductId == productId);
        if(product == null) 
            return true;

        bool result = _products.Remove(product);
        return IsProductAddedOrRemoved() && result; // Checked if the product is removed.
    }


    /// <summary>
    /// Checked if the product is successfully removed or added in the inventory.
    /// This code is useless again since it has no way if the remove or added product
    /// was the one the user selected.
    /// </summary>
    private bool IsProductAddedOrRemoved()
    {
        if(_productsCount != _products.Count())
        {
            _productsCount = _products.Count();
            return true;
        }

        return false;
    }


    /// <summary>
    /// Update the value of the quantity in stock of the product.
    /// </summary>
    public bool UpdateProduct(int productId, int newQuantity)
    {
        // Get the product in the inventory and throw an exception if the product is not found.
        Product? product = _products.FirstOrDefault(x => x.ProductId == productId);
        if(product is null)
        {
            // I wan't to throw it as an exception but I think it will be
            // expensive to do that since a lot of try-catch will be written.
            Console.WriteLine("Product is not found in the inventory.");
            return false;
        }

        product.QuantityInStock = (uint)newQuantity;
        return true;
    }


    /// <summary>
    /// List the table in the console. 
    /// </summary>
    public void ListProducts()
    {
        // Create a new Console Table class that create a table to write in the console.
        var table = new ConsoleTable("ID", "Name", "Quantity in Stock", "Price");
        foreach(var product in _products)
        {
            int productId = product.ProductId;
            string productName = product.Name;
            uint qtyInStock = product.QuantityInStock;
            string price = product.Price.ToString("0.00");
            table.AddRow(productId, productName, qtyInStock, price);
        }

        table.Write();
    }


    /// <summary>
    /// Calculate the total value of the inventory.
    /// </summary>
    public void GetTotalValue()
    {
        // Using the .Select to calculate the total price of each product.
        // Then call the .Sum to get the Summazation of the products.
        decimal total = _products.Select(p => p.Price * p.QuantityInStock).Sum();
        Console.WriteLine($"The total value of the inventory is ${total.ToString("00.00")}");
    }
}
