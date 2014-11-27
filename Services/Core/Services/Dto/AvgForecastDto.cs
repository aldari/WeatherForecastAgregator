using System;

namespace Services.Core.Services.Dto
{
    public class AvgForecastDto
    {
        public DateTime Date { get; set; }
        public int AvgTemperature { get; set; }
        public int AvgHumidity { get; set; }
    }
}
