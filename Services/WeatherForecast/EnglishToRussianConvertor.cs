using System;
using System.Text;

namespace Services
{
    public static class EnglishToRussianConvertor
    {
        public static String ToRussianWind(this String input)
        {
            var sb = new StringBuilder();
            foreach (var ch in input)
            {
                char newch = '\0';
                switch (ch)
                {
                    case 'N':
                        newch = 'С';
                        break;
                    case 'E':
                        newch = 'В';
                        break;
                    case 'S':
                        newch = 'Ю';
                        break;
                    case 'W':
                        newch = 'З';
                        break;
                }
                sb.Append(newch);
            }
            return sb.ToString();
        }
    }
}
