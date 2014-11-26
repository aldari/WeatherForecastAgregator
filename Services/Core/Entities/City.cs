using System;
using System.Collections.Generic;

namespace Services.Core.Entities
{
    public class City : Entity<int>
    {
        public String Name { get; set; }

        public ICollection<WeatherForecast> Forecasts { get; protected set; }

        public City()
        {
            Forecasts = new List<WeatherForecast>();
        }
    }
}
