using System;
using System.Linq;
using System.Xml.Linq;
using Services.Weather.Dto;

namespace Services.Weather.Impl
{
    public class WundergroundService : IForecastService
    {
        private readonly IQueryLoader _queryLoader;

        public WundergroundService(IQueryLoader loader)
        {
            _queryLoader = loader;
        }

        private String RequestPath(String city)
        {
            const string magicString = "http://api.wunderground.com/api/7b9175d0dab642ae/forecast10day/lang:RU/conditions/q/Russia/{0}.xml";
            return String.Format(magicString, city);
        }

        public ForecastResponseDto ForecastData(String city)
        {
            try
            {
                var content = _queryLoader.LoadData(RequestPath(city));
                XElement xelement = XElement.Parse(content);
                var forecastNodes = xelement.Element("forecast").Element("simpleforecast").Element("forecastdays").Elements();
                return new ForecastResponseDto
                {
                    Success = true,
                    Items = forecastNodes.Select(ParseNode)
                };
            }
            catch (Exception ex)
            {
                return new ForecastResponseDto
                {
                    Success = false,
                    Message = ex.Message
                };
            }
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
