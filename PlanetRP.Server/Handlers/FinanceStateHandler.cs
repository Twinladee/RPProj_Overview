using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.Server.Handlers
{


    internal delegate void FinanceStateHandler(object sender, FinanceEventArgs financeEventArgs);

    internal class FinanceEventArgs
    {
        public decimal _sum { get; private set; } //???

        public FinanceEventArgs(decimal sum)
        {
            _sum = sum;
        }
    }
}
