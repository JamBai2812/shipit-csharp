using System.Collections.Generic;

namespace ShipIt.Models.ApiModels
{
    public class Truck
    {
        public double Weight { get; set; }
        public List<Batch> StockOnTruck { get; set; }


        public Truck()
        {
            StockOnTruck = new List<Batch>();
            Weight = 0;
        }


        public int NumberOfItemsOnTruck()
        {  
            var total = 0;
            foreach (var batch in StockOnTruck)
            {
                total += batch.Quantity;
            }

            return total;
        }
    }
}