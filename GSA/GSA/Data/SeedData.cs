using ChoETL;
using GSA.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GSA.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            string path = Directory.GetCurrentDirectory(); 
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();
            if (!context.Strategies.Any())
            { 
                // Load strategies
                foreach (dynamic e in new ChoCSVReader(path + "\\DataStore\\properties.csv").WithFirstLineHeader())
                {
                    var strategy = new Strategy() { Name = e.StratName, Region = e.Region };
                    context.Strategies.Add(strategy); 
                }  
                context.SaveChanges();

                var strategies = context.Strategies.ToList();

                // Load pnl  
                using (var parser = new ChoCSVReader(path + "\\DataStore\\pnl.csv").WithFirstLineHeader(true))
                {
                    foreach (var t in parser)
                    {
                        var count = t.Count;
                        var date = DateTime.ParseExact(t[0], "yyyy-MM-dd", null);

                        for(var i = 1; i<count; i++)
                        { 
                            var strategyId = strategies.Find(s => s.Name.Equals("Strategy"+i)).Id; 
                            var pnl = new PNL() { StrategyId = strategyId, Value = Int32.Parse(t[i]), Date = date}; 
                            context.PNLs.Add(pnl); 
                        }
                    }
                } 
                context.SaveChanges();

                // Load pnl  
                using (var parser = new ChoCSVReader(path + "\\DataStore\\capital.csv").WithFirstLineHeader(true))
                {
                    foreach (var t in parser)
                    {
                        var count = t.Count;
                        var date = DateTime.ParseExact(t[0], "yyyy-MM-dd", null);

                        for (var i = 1; i < count; i++)
                        {
                            var strategyId = strategies.Find(s => s.Name.Equals("Strategy" + i)).Id;
                            var capital = new Capital() { StrategyId = strategyId, Value = Int32.Parse(t[i]), Date = date };
                            context.Capitals.Add(capital);
                        }
                    }
                }
                context.SaveChanges();

            }
        }
    }
}
