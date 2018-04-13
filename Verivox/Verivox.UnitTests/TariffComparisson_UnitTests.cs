using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Verivox.UnitTests
{
    public class TariffComparisson_UnitTests
    {

        /// <summary>
        /// A factory method that returns the list of products with the calculation model from the interview task
        /// </summary>
        private List<IProduct> GetProducts()
        {
            return new List<IProduct>() {
                new BasicTariff("ProductA", baseCostsPerMonth:5,consumptionCosts: 22),
                new PackagedTariff("ProductB", fixedCost: 800, fixedCostConsumptionLimit: 4000, consumptionCosts: 30)
            };
        }

        /// <summary>
        /// This unit test will check if the comparisson is correct, for the calculation models and consumptions in the interview task
        /// </summary>
        [TestCase(3500, "ProductB")]
        [TestCase(4500, "ProductB")]
        [TestCase(6000, "ProductA")]
        public void TariffComparisson_Compare_OrderIsCorrect(int consumption, string expectedNameOfCheapestProduct)
        {
            TariffComparisson c = new TariffComparisson(GetProducts());
            var sortedProducts = c.CompareProducts(consumption);

            string nameOfCheapestProduct = sortedProducts.First().Name;

            Assert.AreEqual(nameOfCheapestProduct, expectedNameOfCheapestProduct);

        }

        /// <summary>
        /// This unit test will check if the correct error is thrown when an invalid list of products is passed
        /// </summary>
        [TestCase(0)]
        [TestCase(-10)]
        [TestCase(20)]
        public void TarifComparisson_NoProducts_MustRaiseError(int consumption)
        {
            List<IProduct> emptyList = new List<IProduct>();
            TariffComparisson c = new TariffComparisson(emptyList);
            Assert.That(() => c.CompareProducts(consumption),
                Throws.TypeOf< ArgumentException >());

            c = new TariffComparisson(null);
            Assert.That(() => c.CompareProducts(consumption),
                Throws.TypeOf<ArgumentNullException>());

        }

        /// <summary>
        /// This unit test will check if an error is thrown when an invalid consumption is passed
        /// </summary>
        [TestCase(0)]
        [TestCase(-10)]
        public void TarifComparisson_InvalidConsumption_MustRaiseError(int consumption)
        {
            TariffComparisson c = new TariffComparisson(GetProducts());

            Assert.That(() => c.CompareProducts(consumption),
                Throws.TypeOf<ArgumentOutOfRangeException>());

        }

    }
}
