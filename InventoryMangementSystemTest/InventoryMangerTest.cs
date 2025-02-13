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


        }
    }
}