using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verivox
{
    public interface IProduct
    {

        string Name { get; set; }


        int CalculateAnnualCost(int consumption);


    }
}
