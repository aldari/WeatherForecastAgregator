using System;

namespace Services.Dto
{
    public class ForecastDto
    {
        public DateTime Date { get; set; }

        public String Description { get; set; }

        public int Humidity { get; set; }

        public int WindSpeed { get; set; }

        public string WindDirection { get; set; }

        public int MinTemperature { get; set; }

        public int MaxTemperature { get; set; }
    }
}
