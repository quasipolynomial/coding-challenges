using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSA.Model
{
    public class PNL
    {
        public Guid Id { get; set;  }

        public Guid StrategyId { get; set; }

        public DateTime Date { get; set; }

        public int Value { get; set; }  

    }
}
