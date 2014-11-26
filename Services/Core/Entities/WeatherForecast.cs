using System;

namespace Services.Core.Entities
{
    public class WeatherForecast: Entity<int>
    {
        public virtual City City { get; set; }
        public virtual ForecastServiceEntity Service { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual int DayTemperature { get; set; }
        public virtual int Humidity { get; set; }
    }
}
