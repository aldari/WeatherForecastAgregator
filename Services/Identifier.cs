using System;
using System.Collections;
using Services.Weather.Impl;

namespace Services
{
    
    // Назначение класса сопоставить каждому сервису прогноза погоды уникальное число
    // Данные прогнозов погоды должны ссылаться на внешний ключ сервиса.

    public class Identifier : IIdentifier
    {
        private static readonly Hashtable priorities = new Hashtable();

        static Identifier()
        {
            // не меняйте значения уже существующих классов
            priorities.Add(typeof (OpenweathermapService), 1);
            priorities.Add(typeof (WundergroundService), 2);
        }

        public int IdentifierFor(Type type)
        {
            if (priorities.Contains(type))
                return (int) priorities[type];
            return 0;
        }
    }
}
