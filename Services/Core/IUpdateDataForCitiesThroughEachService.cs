using Services.Core.Entities;
using Services.Weather;

namespace Services.Core
{
    public interface IUpdateDataForCitiesThroughEachService
    {
        void Execute();
        void ProceedCity(IForecastService service, City city);
    }
}