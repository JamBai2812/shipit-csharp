using System.Collections.Generic;
using System.Linq;
using ShipIt.Repositories;

namespace ShipIt.Models.ApiModels
{
    public interface ITruckFillingHandler
    {
        List<Truck> FillTrucks(List<StockAlteration> lineItems);
    }

    public class TruckFillingHandler : ITruckFillingHandler
    {
        private readonly IProductRepository _repo;

        public TruckFillingHandler(IProductRepository repo)
        {
            _repo = repo;
        }

        public List<Truck> FillTrucks(List<StockAlteration> lineItems)
        {
            var trucks = new List<Truck> {new Truck()};

            foreach (var item in lineItems)
            {
                var batch = new Batch(item);
                var truckToPack = trucks.DefaultIfEmpty(null).First(t => t.Weight + (batch.BatchWeight) < 2000);
                
                if (!truckToPack.Equals(null))
                {
                    truckToPack.StockOnTruck.Add(batch);
                    truckToPack.Weight += (batch.BatchWeight);
                }
                else
                {
                    var newTruckToPack = new Truck();
                    newTruckToPack.StockOnTruck.Add(batch);
                    newTruckToPack.Weight += batch.BatchWeight;
                    trucks.Add(newTruckToPack);
                }
               
                //Problem where products will be added to a new truck even if total weight is over 2000kg.
            }

            return trucks;
        }
    }


    public class Batch
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductWeight { get; set; }
        public int Quantity { get; set; }
        public double BatchWeight { get; set; }


        public Batch(StockAlteration stockAlteration)
        {
            var repo = new ProductRepository();
            var product = repo.GetProductById(stockAlteration.ProductId);

            ProductId = stockAlteration.ProductId;
            ProductName = product.Name;
            ProductWeight = product.Weight;
            Quantity = stockAlteration.Quantity;
            BatchWeight = ProductWeight * stockAlteration.Quantity;
        }
    }
}