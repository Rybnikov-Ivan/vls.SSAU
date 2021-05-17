using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSAU.vls.FIRST.lr.Calculation.Models
{
    public class TypeModel
    {
        /// <summary>
        /// Транзистор
        /// </summary>
        public double Type15 { get; set; }

        /// <summary>
        /// Конденсатор
        /// </summary>
        public double Type17 { get; set; }

        /// <summary>
        /// Пробой
        /// </summary>
        public double Type30 { get; set; }

        /// <summary>
        /// Комплексный тип 1
        /// </summary>
        public double Type50 { get; set; }

        /// <summary>
        /// Комплексный тип 2
        /// </summary>
        public double Type80 { get; set; }

        /// <summary>
        /// Редкий 
        /// </summary>
        public double Type200 { get; set; }
    }
}
