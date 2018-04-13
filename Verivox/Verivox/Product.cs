using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verivox
{
    public class BasicTariff : IProduct
    {
        public string Name { get; set; }

        private int baseCostsPerMonth;
        private int consumptionCosts;

        public BasicTariff(string name,int baseCostsPerMonth,int consumptionCosts)
        {
            this.Name = name;
            this.baseCostsPerMonth = baseCostsPerMonth;
            this.consumptionCosts = consumptionCosts;
        }

        public int CalculateAnnualCost(int consumption)
        {
            int result = baseCostsPerMonth * 12;
            result += consumption * consumptionCosts/100;
            return result;
        }

    }

    public class PackagedTariff : IProduct
    {

        public string Name { get; set; }

        private int fixedCost;
        private int fixedCostConsumptionLimit;
        private int consumptionCosts;

        public PackagedTariff(string name, int fixedCost,int fixedCostConsumptionLimit,int consumptionCosts)
        {
            this.Name = name;
            this.fixedCost = fixedCost;
            this.fixedCostConsumptionLimit = fixedCostConsumptionLimit;
            this.consumptionCosts = consumptionCosts;
        }

        public int CalculateAnnualCost(int consumption)
        {
            int result = fixedCost;

            if (consumption>fixedCostConsumptionLimit)
            {
                result += (consumption - fixedCostConsumptionLimit) * consumptionCosts / 100;
            }
            
            return result;
        }

    }
}
