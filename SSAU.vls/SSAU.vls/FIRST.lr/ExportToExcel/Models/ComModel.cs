using System.Collections.Generic;

namespace SSAU.vls.FIRST.lr.ExportToExcel.Models
{
    public class ComModel
    {
        /// <summary>
        ///  Тип л.р.
        /// </summary>
        public string Com_1 { get; set; }

        /// <summary>
        /// Отказы, т.е. число, которое характеризует тип отказа
        /// </summary>
        public int Com_2 { get; set; }

        /// <summary>
        /// Мощность от 0 до 1000
        /// </summary>
        public double Com_3 { get; set; }

        /// <summary>
        /// Уровень А от 0 до 1000
        /// </summary>
        public double Com_4 { get; set; }

        /// <summary>
        /// Уровень B от 0 до 1000
        /// </summary>
        public double Com_5 { get; set; }

        /// <summary>
        /// Уровень C от 0 до 1000
        /// </summary>
        public double Com_6 { get; set; }

        /// <summary>
        /// Уровень D от 0 до 1000
        /// </summary>
        public double Com_7 { get; set; }

        /// <summary>
        /// Уровень E от 0 до 1000
        /// </summary>
        public double Com_8 { get; set; }

        public static List<ComModel> ExportList = new List<ComModel>();
    }
}
