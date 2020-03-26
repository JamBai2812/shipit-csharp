using System.Collections.Generic;

namespace ShipIt.Models.ApiModels
{
    public interface IBatchGenerator
    {
        List<Batch> ConvertLineItemsToBatches(List<StockAlteration> lineItem);
    }
    
    
    public class BatchGenerator : IBatchGenerator
    {
        public BatchGenerator()
        {
            
        }

        public List<Batch> ConvertLineItemsToBatches(List<StockAlteration> lineItems)
        {
            var batches = new List<Batch>();


            foreach (var item in lineItems)
            {
                batches.Add(new Batch(item));
            }

            return batches;
        }
    }
}