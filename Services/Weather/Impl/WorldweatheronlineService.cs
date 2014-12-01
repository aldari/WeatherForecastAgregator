using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Services.Weather.Dto;

namespace Services.Weather.Impl
{
    public class WorldweatheronlineService
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

        public IEnumerable<ForecastDto> ForecastData(String city)
        {
            var content = _queryLoader.LoadData(RequestPath(city));
            XElement xelement = XElement.Parse(content);
            
            return xelement.Elements("weather").Select(ParseNode).ToList();
        }

        public ForecastDto ParseNode(XElement node)
        {
            return new ForecastDto();
        }
    }
}
