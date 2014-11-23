using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace Services
{
    public class OpenweathermapService
    {

        public IEnumerable<ForecastDto> ForecastData(string content)
        {
            var xelement = XElement.Parse(content);
            return xelement.Element("forecast").Elements().Select(ParseNode).ToList();
        }

        private ForecastDto ParseNode(XElement node)
        {
            return new ForecastDto
            {
                Date = DateTime.Parse(node.Attribute("day").Value, CultureInfo.CurrentCulture),
                Description = node.Element("symbol").Attribute("name").Value,
                Humidity = Int32.Parse(node.Element("humidity").Attribute("value").Value),
                MaxTemperature = (int)Single.Parse(node.Element("temperature").Attribute("max").Value, CultureInfo.InvariantCulture),
                MinTemperature = (int)Single.Parse(node.Element("temperature").Attribute("min").Value, CultureInfo.InvariantCulture),
                WindDirection = node.Element("windDirection").Attribute("code").Value.ToRussianWind(),
                WindSpeed = (Int32)Math.Round(Single.Parse(node.Element("windSpeed").Attribute("mps").Value, CultureInfo.InvariantCulture))
            };
        }
    }
}
