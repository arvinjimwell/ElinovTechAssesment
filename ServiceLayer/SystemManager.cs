using static System.Console;
using ConsoleTables;
using System.Diagnostics;

namespace ServiceLayer;

public static class SystemManager
{
    private static InventoryManager _inventoryManager;
    private static int _selected;

    /// <summary>
    /// Run the program.
    /// </summary>
    public static void Run()
    {
        _selected = 0;
        _inventoryManager = new InventoryManager();
        while(_selected != 6)
        {
            try
            {
                // Clear the screen.
                Clear();

                // Print out the menu.
                PrintSelection();

                // Get the user to input the selected function.
                _selected = Convert.ToInt32(ReadLine());

                // clear the screen again.
                Clear();

                // Manages which function to used.
                SelectionManager();
            }
            catch
            {
                WriteLine("Invalid input.");
            }
            finally
            {
                // Stop the screen to show the information first.
                WriteLine();
                WriteLine("Press Enter Key to continue..");
                ReadLine();
            }
        }
    }


    /// <summary>
    /// Print out the selection on what the user wish to continue.
    /// </summary>
    private static void PrintSelection()
    {
        ConsoleTable table = new("Number", "Information");
        WriteLine("Please select a number to continue.");
        table.AddRow("1", "Add Product");
        table.AddRow("2", "Remove Product");
        table.AddRow("3", "Update Product");
        table.AddRow("4", "List Products");
        table.AddRow("5", "Get Inventory Total Value");
        table.AddRow("6", "Exit Console");

        table.Options.EnableCount = false;
        table.Write();
        WriteLine();
        Write("input: ");
    }


    /// <summary>
    /// Switch statement for the _selected property,
    /// that store what the user wants to do in the system.
    /// 
    /// Value:
    /// 1 = Add Product
    /// 2 = Remove Product
    /// 3 = Update Product
    /// 4 = List Product
    /// 5 = Get the total value
    /// </summary>
    private static void SelectionManager()
    {
        switch(_selected)
        {
            case 1:
                AddProduct();
                break;
            case 2:
                RemoveProduct();
                break;
            case 3:
                UpdateProduct();
                break;
            case 4:
                _inventoryManager.ListProducts();
                break;
            case 5:
                _inventoryManager.GetTotalValue();
                break;
        }
    }


    /// <summary>
    /// Create a product and add it to the inventory.
    /// </summary>
    private static void AddProduct()
    {
        try
        {
            Write("Product Name: ");
            string? name = ReadLine();
            if(string.IsNullOrWhiteSpace(name))
            {
                WriteLine("Invalid name.");
                return;
            }

            // Used checked statement to check for overflow.
            checked
            {
                Write("Product Price: ");
                decimal price = Convert.ToDecimal(ReadLine());

                Write("Product Quantity: ");
                uint qty = Convert.ToUInt32(ReadLine());

                var result = _inventoryManager.AddProduct(new(name, qty, price));
                Clear();

                if(result)
                    WriteLine("Product is succesfully added.");
            }
        }
        catch(Exception ex) 
        {
            // Giving the exception error for the user is not ideal
            // but it is much better than showing what is not wrong in their input.
            WriteLine(ex.Message);
        }
    }


    /// <summary>
    /// Remove the selected product in the inventory.
    /// </summary>
    private static void RemoveProduct()
    {
        try
        {
            Write("Enter Product ID: ");
            int productId = Convert.ToInt32(ReadLine());

            var result = _inventoryManager.RemoveProduct(productId);
            Clear();
            if(result)
                WriteLine("Succesfully remove the product.");
        }
        catch(Exception ex)
        {
            WriteLine(ex.Message);
        }
    }


    /// <summary>
    /// Update the quantity of the product.
    /// </summary>
    private static void UpdateProduct()
    {
        try
        {
            checked
            {
                Write("Enter Product ID: ");
                int productId = Convert.ToInt32(ReadLine());

                Write("Enter Quantity: ");
                int quantity = Convert.ToInt32(ReadLine());

                var result = _inventoryManager.UpdateProduct(productId, quantity);

                Clear();
                if(result)
                    WriteLine("Successfully update the product");
            }
        }
        catch(Exception ex)
        {
            WriteLine(ex.Message);
        }
    }
}
