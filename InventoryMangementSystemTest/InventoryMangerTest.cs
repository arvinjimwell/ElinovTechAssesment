using DataLayer.Models;
using ServiceLayer;

namespace InventoryMangementSystemTest
{
    public class InventoryMangerTest
    {
        [Fact]
        public void Should_AddProduct()
        {
            InventoryManager invManager = new();
            Product product = new("Test", 10, 20.0M);

            bool result = invManager.AddProduct(product);
            Assert.True(result);
        }

        [Fact]
        public void Should_RemoveProductById()
        {
            InventoryManager inventoryManager = new();
            Product product = new("Test", 10, 20.0M);
            inventoryManager.AddProduct(product);

            bool result = inventoryManager.RemoveProduct(1);
            Assert.True(result);
        }

        // I admit I made a mistake. There is no way of testing the 
        // Update, List and GetTotalValues since they are printed in the console.
    }
}