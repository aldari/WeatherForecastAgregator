using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Services.Weather.Dto;

namespace Services.Weather.Impl
{
    public class WorldweatheronlineService: IForecastService
    {
        private readonly IQueryLoader _queryLoader;
        public WorldweatheronlineService(IQueryLoader loader)
        {
            _queryLoader = loader;
        }

        private String RequestPath(String city)
        {
            const string magicString = "http://api.worldweatheronline.com/free/v2/weather.ashx?q={0}&format=xml&num_of_days=5&key=7a7ca84ff9b809b9731e3bb53a421";
            return String.Format(magicString, city);
        }

        public ForecastResponseDto ForecastData(String city)
        {
            try
            {
                var content = _queryLoader.LoadData(RequestPath(city));
                XElement xelement = XElement.Parse(content);

                return new ForecastResponseDto
                {
                    Success = true,
                    Items = xelement.Elements("weather").Select(ParseNode)
                };
            }
            catch (Exception ex)
            {
                return new ForecastResponseDto
                {
                    Success = true,
                    Message = ex.Message,
                    Items = new List<ForecastDto>()
                };
            }
        }

        public ForecastDto ParseNode(XElement node)
        {
            var date = DateTime.Parse(node.Element("date").Value);
            var temperature = Int32.Parse(node.Element("maxtempC").Value);
            var humadity = Int32.Parse((node.Elements("hourly").ToList()[4]).Element("humidity").Value);


            return new ForecastDto
            {
                Date = date,
                MaxTemperature = temperature,
                Humidity = humadity
            };
        }
    }
}
