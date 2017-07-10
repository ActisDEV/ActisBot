using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bot
{
    class Functions
    {
        public static string Result(int randomNum)
        {
            string result = "Ошибка";

            if (randomNum == 0)
            {
                result = "Абсолютно неверно";
            }
            else if ((randomNum >= 1) && (randomNum <= 10))
            {
                result = "Вероятность крайне низка";
            }
            else if ((randomNum >= 11) && (randomNum <= 20))
            {
                result = "Очень маловероятно";
            }
            else if ((randomNum >= 21) && (randomNum <= 30))
            {
                result = "Практически невероятно";
            }
            else if ((randomNum >= 31) && (randomNum <= 40))
            {
                result = "Маловероятно";
            }
            else if ((randomNum >= 41) && (randomNum <= 49))
            {
                result = "Скорее нет";
            }
            else if (randomNum == 50)
            {
                result = "Равновероятно";
            }
            else if ((randomNum >= 51) && (randomNum <= 60))
            {
                result = "Скорее да";
            }
            else if ((randomNum >= 61) && (randomNum <= 70))
            {
                result = "Вероятно";
            }
            else if ((randomNum >= 71) && (randomNum <= 80))
            {
                result = "Высоковероятно";
            }
            else if ((randomNum >= 81) && (randomNum <= 90))
            {
                result = "Очень вероятно";
            }
            else if ((randomNum >= 91) && (randomNum <= 99))
            {
                result = "Крайне вероятно";
            }
            else if (randomNum == 100)
            {
                result = "Абсолютно точно";
            }

            return result;
        }
    }
}
