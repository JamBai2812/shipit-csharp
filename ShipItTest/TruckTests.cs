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

        private readonly ProductDataModel TestProduct = new ProductDataModel
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
            var lineItems = new List<StockAlteration>
            {
                new StockAlteration(17, 3),
            };

            //Act
            var trucks = _truckFillingHandler.FillTrucks(lineItems);
            var truckList = trucks.ToList();


            //Assert
            
            Assert.AreEqual(truckList.Count, 1);
            Assert.AreEqual(truckList[0].StockOnTruck.Count, 1);
            Assert.AreEqual(truckList[0].StockOnTruck[0].Quantity, 3);
            Assert.AreEqual(truckList[0].Weight, 300);



        }
    }
}