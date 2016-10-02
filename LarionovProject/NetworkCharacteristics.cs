using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LarionovProject
{
    class NetworkCharacteristics
    {
        /// <summary>
        /// Подсчёт коэффициента загрузки СМО
        /// </summary>
        /// <param name="lambda">Интенсивность входного потока (лямбда)</param>
        /// <param name="mu">Интенсивность обслуживания (мю)</param>
        public static double LoadFactor(double lambda, double mu)
        {
            return lambda / mu;
        }

        /// <summary>
        /// Средняя длина очереди (Q)
        /// </summary>
        /// <param name="loadFactor">Коэффициент загрузки СМО</param>
        /// <param name="squareCoefVariationInput">Квадрат коэффициента вариации интервалов между заявками входного потока (ню)</param>
        /// <param name="squareCoefVariationService">Квадрат коэффициента вариации интервалов времени обслуживания заявок (ню)</param>
        public static double CalculateQ(double loadFactor, double squareCoefVariationInput, double squareCoefVariationService)
        {
            return Math.Pow(loadFactor, 2) * (squareCoefVariationInput + squareCoefVariationService) / (2 * (1 - loadFactor));
        }

        /// <summary>
        /// Cреднее число заявок в СМО (L)
        /// </summary>
        /// <param name="queueLength">Средняя длина очереди (Q)</param>
        /// <param name="loadFactor">Коэффициент загрузки СМО (ро)</param>
        public static double CalculateL(double queueLength, double loadFactor)
        {
            return queueLength + loadFactor;
        }

        /// <summary>
        /// Среднее время нахождения заявок в очереди
        /// </summary>
        public static double CalculateW(double queueLength, double lambda)
        {
            return queueLength / lambda;
        }

        /// <summary>
        /// Среднее время нахождения заявок в СМО
        /// </summary>
        /// <param name="numberOfRequests">Cреднее число заявок в СМО (L)</param>
        public static double CalculateT(double numberOfRequests, double lambda)
        {
            return numberOfRequests / lambda;
        }
    }
}
