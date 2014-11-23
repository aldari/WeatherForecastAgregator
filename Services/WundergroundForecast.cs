using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Services
{
    public class WundergroundForecast
    {
        public IEnumerable<ForecastDto> ForecastData(String str)
        {
            XElement xelement = XElement.Parse(str);
            var forecastNodes = xelement.Element("forecast").Element("simpleforecast").Element("forecastdays").Elements();
            return forecastNodes.Select(ParseNode).ToList();
        }

        private ForecastDto ParseNode(XElement node)
        {
            var date = new DateTime(
                int.Parse(node.Element("date").Element("year").Value),
                int.Parse(node.Element("date").Element("month").Value),
                int.Parse(node.Element("date").Element("day").Value)
            );
            var description = node.Element("conditions").Value;
            var humidity = int.Parse(node.Element("avehumidity").Value);
            var windSpeed = int.Parse(node.Element("avewind").Element("kph").Value);
            var windDirection = node.Element("avewind").Element("dir").Value;
            var minTemperature = int.Parse(node.Element("low").Element("celsius").Value);
            var maxTemperature = int.Parse(node.Element("high").Element("celsius").Value);

            return new ForecastDto
            {
                Date = date,
                Description = description,
                Humidity = humidity,
                MaxTemperature = maxTemperature,
                MinTemperature = minTemperature,
                WindDirection = windDirection,
                WindSpeed = windSpeed
            };
        }
    }
}
