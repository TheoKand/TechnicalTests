using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verivox
{

    public class TariffComparisson
    {

        private List<IProduct> products;

        public TariffComparisson(List<IProduct> products)
        {
            this.products = products;
        }


        public IEnumerable<ComparedProduct> CompareProducts(int consumption)
        {
            if (products ==null )
            {
                throw new ArgumentNullException("You must provide a list of products");
            }
            if (products.Count < 2)
            {
                throw new ArgumentException("You must provide two or more products");
            }
            else if (consumption < 1)
            {
                throw new ArgumentOutOfRangeException("The consumption must be a positive number");
            }

            var result = products.OrderBy(p => p.CalculateAnnualCost(consumption)).Select(p => new ComparedProduct() { Name = p.Name, AnnualCosts = p.CalculateAnnualCost(consumption) });
            return result;
        }
    }

    public struct ComparedProduct
    {
        public string Name;
        public int AnnualCosts;
    }
}
