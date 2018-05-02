using MonitoringSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringSystem.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any temperatures.
            if (context.Temperatures.Any())
            {
                return;   // DB has been seeded
            }

            var temperatures = new Temperature[]
            {
                new Temperature{Value = 80},
                new Temperature{Value = 70},
                new Temperature{Value = 75}
            };
            foreach (var temperature in temperatures)
            {
                context.Temperatures.Add(temperature);
            }
            context.SaveChanges();

            var humidities = new Humidity[]
            {
                new Humidity{Value=20 },
                new Humidity{Value=26},
                new Humidity{Value=15}
            };
            foreach (var humidity in humidities)
            {
                context.Humidities.Add(humidity);
            }
            context.SaveChanges();
        }
    }
}
