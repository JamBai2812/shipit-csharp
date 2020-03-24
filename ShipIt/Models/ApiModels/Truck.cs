using System.Collections.Generic;

namespace ShipIt.Models.ApiModels
{
    public class Truck
    {
        public float Weight { get; set; }
        public Dictionary<Product, int> StockOnTruck { get; set; }


        public Truck()
        {
            Weight = 0;
        }


        public int NumberOfItemsOnTruck()
        {  
            var total = 0;
            foreach (var product in StockOnTruck)
            {
                total += product.Value;
            }

            return total;
        }
    }
}