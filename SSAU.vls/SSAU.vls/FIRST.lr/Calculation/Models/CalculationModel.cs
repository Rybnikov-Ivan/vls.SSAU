using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSAU.vls.FIRST.lr.Calculation.Models
{
    public class CalculationModel
    {
        /// <summary>
        /// Внезапный отказ (вычисляется)
        /// </summary>
        public double SuddenFailure { get; set; }

        /// <summary>
        /// Постепенный отказ (вычисляется)
        /// </summary>
        public double GradualFailure { get; set; }

        /// <summary>
        /// Время (берется из формы)
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// Время (берется из формы)
        /// </summary>
        public double Lambda { get; set; }

        /// <summary>
        /// Мощность (берется из формы)
        /// </summary>
        public int Power { get; set; }

        /// <summary>
        /// Коэффициент К4
        /// </summary>
        public double K4 { get; set; }

        /// <summary>
        /// Коэффицинет К5
        /// </summary>
        public double K5 { get; set; }

        /// <summary>
        /// Коэффицинет К6
        /// </summary>
        public double K6 { get; set; }

        /// <summary>
        /// Коэффицинет К7
        /// </summary>
        public double K7 { get; set; }

        /// <summary>
        /// Коэффицинет К8
        /// </summary>
        public double K8 { get; set; }
    }
}
