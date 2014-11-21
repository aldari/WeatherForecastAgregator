using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Services
{
    public class WundergroundForecast
    {
        public WundergroundForecast()
        {
        }

        public IEnumerable<ForecastDto> ForecastData(String str)
        {
            XElement xelement = XElement.Parse(str);
            var forecastNodes = xelement.Element("forecast").Element("simpleforecast").Element("forecastdays").Elements();
            var list = new List<ForecastDto>();
            foreach (var forecastNode in forecastNodes)
            {
                list.Add(new ForecastDto());
            }
            return list;
        }
    }
}
