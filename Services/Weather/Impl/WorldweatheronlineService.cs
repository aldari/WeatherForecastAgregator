using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Services.Weather.Dto;

namespace Services.Weather.Impl
{
    public class WorldweatheronlineService
    {
        public IEnumerable<ForecastDto> ForecastData(String content)
        {
            XElement xelement = XElement.Parse(content);
            var forecastNodes = xelement.Element("forecast").Element("simpleforecast").Element("forecastdays").Elements();
            return forecastNodes.Select(ParseNode).ToList();
        }

        public ForecastDto ParseNode(XElement node)
        {
            return new ForecastDto();
        }
    }
}
