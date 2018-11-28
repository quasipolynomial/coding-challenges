using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GSA.Data;
using GSA.Model;
using Microsoft.AspNetCore.Mvc; 

namespace GSA.Controllers
{
    [Route("api")]
    public class GSAController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GSAController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("monthly-capital")]
        public IEnumerable<CapitalDTO> GetMonthlyCapital([FromQuery] string[] strategies)
        {
            var strats = _context.Strategies.Where(s => strategies.Contains(s.Name)).ToList();
            var monthlyCapitals = new List<CapitalDTO>();

            foreach (var strat in strats)
            {
                var capitals = _context.Capitals.Where(c => strats.Exists(s => s.Id == c.StrategyId)).ToList();

                capitals.ForEach(c =>
                {
                    monthlyCapitals.Add(new CapitalDTO() { Capital = c.Value, Date = c.Date, Strategy = strat.Name });
                });

            }

            return monthlyCapitals;
        }

        [HttpGet("cumulative-pnl")]
        public IEnumerable<PNLDTO> GetCumalativePNL([FromQuery] string startDate, [FromQuery] string region)
        {
            var date = DateTime.ParseExact(startDate, "yyyy-MM-dd", null);
            var strats = _context.Strategies.Where(s => region.Contains(s.Region)).ToList();
            var cumulativePnls = new List<PNLDTO>();
            var dates = new List<DateTime>();

            foreach (var strat in strats)
            {

                var pnls = _context.PNLs.Where(p => strats.Exists(s => s.Id == p.StrategyId) && p.Date >= date).ToList();

                pnls.ForEach(c => dates.Add(c.Date));

                dates = dates.Distinct().ToList();

                dates.ForEach(d =>
                {
                    var agg = pnls.Where(p => p.Date == d).Aggregate(0, (acc, x) => acc + x.Value);
                    cumulativePnls.Add(new PNLDTO() { CumulativePnl = agg, Date = d, Region = strat.Region });
                });

            }

            return cumulativePnls;
        }

        [HttpGet("compound-daily-returns/{strategy}")]
        public IEnumerable<CompoundDTO> GetCompoundDailyReturns(string strategy)
        {
            var strat = _context.Strategies.FirstOrDefault(s => s.Name.Equals(strategy));
            var compounds = new List<CompoundDTO>();

            // Unsure how to calculate

            var capitals = _context.Capitals.Where(c => c.StrategyId == strat.Id);
            var pnls = _context.PNLs.Where(p => p.StrategyId == strat.Id).OrderBy(p => p.Date);

            foreach (var capital in capitals)
            {
                var startDate = capital.Date;
                var endDate = startDate.AddMonths(1);
                var startPNL = pnls.First(p => p.Date >= startDate).Value;

                var returns = pnls
                    .Where(p => startDate <= p.Date && p.Date < endDate)
                    .Select<PNL, decimal>(p => (decimal)(p.Value / startPNL))
                    .ToList();

                var monthlyCompound = returns.Aggregate(1m, (acc, x) => acc * (1 + x))-1;
                var monthlyCompoundRounded = Math.Round((decimal) monthlyCompound, 5);

                compounds.Add(new CompoundDTO() { CompoundReturn = monthlyCompoundRounded, Date = startDate, Strategy = strat.Name });

            }

            return compounds;
        }
    }
}
