using System;
using Services.Weather;

namespace TestConsoleApplication
{
    public class TestDifferentImplementationsOfTheSameInterface
    {
        private IForecastService[] _a;

        public TestDifferentImplementationsOfTheSameInterface(IForecastService[] a)
        {
            _a = a;
        }

        public void Proceed()
        {
            foreach (var forecastService in _a)
            {
                var data = forecastService.ForecastData("Chelyabinsk");
                Console.WriteLine(forecastService.GetType().ToString());
                foreach (var dto in data.Items)
                {
                    Console.WriteLine(dto.Date);
                    Console.WriteLine(dto.MaxTemperature);
                    Console.WriteLine(dto.Humidity);
                    Console.WriteLine();
                }
            }
        }
    }
}
