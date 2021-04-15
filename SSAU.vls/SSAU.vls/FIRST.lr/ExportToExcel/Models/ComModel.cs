using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSAU.vls.FIRST.lr.ExportToExcel.Models
{
    public class ComModel
    {
        public string[,] ExportData{ get; set; }

        /// <summary>
        ///  Тип л.р.
        /// </summary>
        public string Com_1 { get; set; }

        /// <summary>
        /// Отказы, т.е. число, которое характеризует тип отказа, например: 15 - тип 1; 17 - тип 2, 30 - тип 3; 50 - тип 4; 80 - тип 5;
        /// 94 - тип 1 + тип 2; 106 - тип 3 + тип 4; 130 - тип 2 + тип 3; 150 - тип 2 + тип 5 + тип 5; 200 - тип 10; 300 - отказа нет.
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
    }
}
