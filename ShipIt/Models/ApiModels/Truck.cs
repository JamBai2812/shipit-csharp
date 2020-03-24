using System.Collections.Generic;

namespace ShipIt.Models.ApiModels
{
    public class Truck
    {
        public int Weight { get; set; }
        public List<Product> ProductList { get; set; }


        public Truck()
        {
            Weight = 0;
        }


        public int NumberOfItemsOnTruck()
        {
            return ProductList.Count;
        }
    }
}