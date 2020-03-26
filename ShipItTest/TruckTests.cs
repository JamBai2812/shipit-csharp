using System.Collections.Generic;
using System.Linq;
using System.Net;
using FakeItEasy;
using NUnit.Framework;
using ShipIt.Models.ApiModels;
using ShipIt.Models.DataModels;
using ShipIt.Repositories;

namespace ShipItTest
{
    [TestFixture]
    public class TruckTests
    {
        private TruckFillingHandler _truckFillingHandler;
        private IProductRepository _repo;

        private readonly ProductDataModel TestProduct = new ProductDataModel()
        {
            Id = 17,
            Weight = 100,
            Name = "Test Product",
            Gtin = "021931",
        };


        [SetUp]
        public void SetUp()
        {
            _repo = A.Fake<IProductRepository>();
            _truckFillingHandler = new TruckFillingHandler(_repo);
            A.CallTo(() => _repo.GetProductById(17)).Returns(TestProduct);

        }
        
        
        [Test]
        public void SmallOrderGoesOnOneTruck()
        {
            //Arrange
            var batches = new List<Batch>
            {
                new Batch(17, "TV", 10, 3)
            };

            //Act
            var trucks = _truckFillingHandler.FillTrucks(batches);
            var truckList = trucks.ToList();

            //Assert
            
            Assert.AreEqual(truckList.Count, 1);
            Assert.AreEqual(truckList[0].StockOnTruck.Count, 1);
            Assert.AreEqual(truckList[0].StockOnTruck[0].Quantity, 3);
            Assert.AreEqual(truckList[0].Weight, 30);
        }
        
        [Test]
        public void OrdersOver2000kgGoOnMultipleTrucks()
        {
            //Arrange
            var batches = new List<Batch>
            {
                new Batch(17, "Fridge", 100, 18),
                new Batch(16, "TV", 20, 40)
            };

            //Act
            var trucks = _truckFillingHandler.FillTrucks(batches);
            var truckList = trucks.ToList();

            //Assert
            
            Assert.AreEqual(truckList.Count, 2);
            // Assert.AreEqual(truckList[0].StockOnTruck.Count, 1);
            // Assert.AreEqual(truckList[0].StockOnTruck[0].Quantity, 3);
            // Assert.AreEqual(truckList[0].Weight, 30);
        }

        [Test]

        public void LargeOrderGetsSplitBetweenTrucks()
        {
            //Arrange
            var batches = new List<Batch>
            {
                new Batch(17, "Fridge", 100, 50)
            };

            //Act
            var trucks = _truckFillingHandler.FillTrucks(batches);
            var truckList = trucks.ToList();

            //Assert
            
            Assert.AreEqual(3, truckList.Count);
            // Assert.AreEqual(truckList[0].StockOnTruck.Count, 1);
            // Assert.AreEqual(truckList[0].StockOnTruck[0].Quantity, 3);
            // Assert.AreEqual(truckList[0].Weight, 30);
        }
    }
}