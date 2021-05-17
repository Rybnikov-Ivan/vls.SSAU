namespace SSAU.vls.FIRST.lr.Calculation.Models
{
    public class FailureModel
    {
        /// <summary>
        /// Отказ транзистора
        /// </summary>
        public double Q15 { get; set; }

        /// <summary>
        /// Отказ конденсатора
        /// </summary>
        public double Q17 { get; set; }

        /// <summary>
        /// Пробой
        /// </summary>
        public double Q30 { get; set; }

        /// <summary>
        /// Комплексный отказ 1
        /// </summary>
        public double Q50 { get; set; }

        /// <summary>
        /// Комплексный отказ 2
        /// </summary>
        public double Q80 { get; set; }

        /// <summary>
        /// Редкий отказ
        /// </summary>
        public double Q200 { get; set; }
    }
}
