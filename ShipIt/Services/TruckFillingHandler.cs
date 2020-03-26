using System.Collections.Generic;
using System.Linq;
using ShipIt.Repositories;

namespace ShipIt.Models.ApiModels
{
    public interface ITruckFillingHandler
    {
        List<Truck> FillTrucks(List<Batch> batches);
    }

    public class TruckFillingHandler : ITruckFillingHandler
    {
        private readonly IProductRepository _repo;

        public TruckFillingHandler(IProductRepository repo)
        {
            _repo = repo;
        }

        public List<Truck> FillTrucks(List<Batch> batches)
        {
            var trucks = new List<Truck> {new Truck()};

            foreach (var batch in batches)
            {
                var truckToPack = trucks.FirstOrDefault(t => t.Weight + (batch.BatchWeight) <= 2000);

                if (truckToPack == null)
                {
                    var newTruckToPack = new Truck();

                    newTruckToPack.StockOnTruck.Add(batch);
                    newTruckToPack.Weight += batch.BatchWeight;
                    trucks.Add(newTruckToPack);
                }

                else
                {
                    truckToPack.StockOnTruck.Add(batch);
                    truckToPack.Weight += (batch.BatchWeight);
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

        public Batch(int id, string name, double weight, int quantity)
        {
            ProductId = id;
            ProductName = name;
            ProductWeight = weight;
            Quantity = quantity;
            BatchWeight = weight * quantity;
        }


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