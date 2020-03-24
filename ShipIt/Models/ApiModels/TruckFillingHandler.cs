using System.Collections.Generic;

namespace ShipIt.Models.ApiModels
{
    public class TruckFillingHandler
    {
        public List<Truck> TrucksRequired { get; set; }

        public TruckFillingHandler()
        {
            TrucksRequired.Add(new Truck());
        }

        public void FillTrucks(OutboundOrderRequestModel orderRequest)
        {
            var loaded = false;
            foreach (var truck in TrucksRequired)
            {
                if (truck.Weight + (product.Weight * orderLine.quantity) < 2000)
                {
                    truck.StockOnTruck.Add(product, orderLine.quantity);
                    truck.Weight += (product.Weight * orderLine.quantity);
                    loaded = true;
                    break;
                }
            }

            if (!loaded)
            {
                var newTruck = new Truck();
                newTruck.StockOnTruck.Add(product, orderLine.quantity);
                newTruck.Weight += (product.Weight * orderLine.quantity);
                TrucksRequired.Add(newTruck);
                //Problem where products will be added to a new truck even if total weight is over 2000kg.
            }
        }
    }
}