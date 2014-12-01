using System;
using System.Collections.Generic;

namespace WeatherWebApp.Models
{
    public class CitiesModel
    {
        public IEnumerable<CityModel> Cities { get; set; }

        public String SelectedCity { get; set; }
    }
}