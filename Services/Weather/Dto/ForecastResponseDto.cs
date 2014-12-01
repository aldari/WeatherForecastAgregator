using System;
using System.Collections.Generic;

namespace Services.Weather.Dto
{
    public class ForecastResponseDto
    {
        public String Message { get; set; }
        public Boolean Success { get; set; }
        public IEnumerable<ForecastDto> Items { get; set; }
    }
}
