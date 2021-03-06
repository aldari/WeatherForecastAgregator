﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Services.Weather.Dto;

namespace Services.Weather.Impl
{
    public class OpenweathermapService: IForecastService
    {
        private readonly IQueryLoader _queryLoader;
        public OpenweathermapService(IQueryLoader loader)
        {
            _queryLoader = loader;
        }

        private String RequestPath(String city)
        {
            const string magicString = "http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&mode=xml&units=metric&cnt=10&lang=ru";
            return String.Format(magicString, city);
        }

        public ForecastResponseDto ForecastData(string city)
        {
            try
            {
                var content = _queryLoader.LoadData(RequestPath(city));
                var xelement = XElement.Parse(content);
                return new ForecastResponseDto
                {
                    Success = true,
                    Items = xelement.Element("forecast").Elements().Select(ParseNode)
                };
            }
            catch (Exception ex)
            {
                return new ForecastResponseDto
                {
                    Success = false,
                    Message = ex.Message,
                    Items = new List<ForecastDto>()
                };
            }
            
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
