using SSAU.vls.FIRST.lr.Calculation.Models;
using System;
using SSAU.vls.FIRST.lr.ExportToExcel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSAU.vls.FIRST.lr.Calculation
{
    /// <summary>
    /// Расчет отказов
    /// </summary>
    public static class Calculation
    {
        /// <summary>
        /// Расчет рандомных числе для коэффициентов К
        /// </summary>
        /// <returns></returns>
        public static double RandomK()
        {
            double minValue = 1.0;
            double maxValue = 1.5;

            Random random = new Random();
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }

        /// <summary>
        /// Расчет постепенного отказа
        /// </summary>
        /// <param name="model"></param>
        public static void CalculationSuddenFailure(CalculationModel model, int count)
        {
            
            model.K4 = RandomK();
            model.K5 = RandomK();
            model.K6 = RandomK();
            model.K7 = RandomK();
            model.K8 = RandomK();

            model.SuddenFailure = (1 - Math.Exp(-model.Lambda * model.Time)) * model.K4 * model.K5 * model.K6 * model.K7 * model.K8;

            var export = new ExportExcel();

            export.DataToExport(model, count);
        }
    }
}
