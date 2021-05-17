using SSAU.vls.FIRST.lr.Calculation.Models;
using System;
using SSAU.vls.FIRST.lr.ExportToExcel.Models;

namespace SSAU.vls.FIRST.lr.Calculation
{
    /// <summary>
    /// Расчет отказов
    /// </summary>
    public static class Calculation
    {
        public static double k4 = 1.5;
        public static double k5 = 0.7;
        public static double k6 = 1.2;
        public static double k7 = 1.8;
        public static double k8 = 0.1;

        /// <summary>
        /// Расчет постепенного отказа
        /// </summary>
        /// <param name="model"></param>
        public static CalculationModel CalculationSuddenFailure(CalculationModel model, int count)
        {
            model.K4 = 1000 - ((9.0 / 200.0) * model.Time);
            model.K5 = 600 * Math.Atan((1.0 / model.Time - model.Time + 12000));
            model.K6 = 1000 - (Math.Sqrt(1.0 * model.Time));
            model.K7 = 1000 - (0.7 * model.Time);
            model.K8 = 1000 - (0.8 * model.Time);

            model.SuddenFailure = (1 - Math.Exp(-model.Lambda * model.Time)) * model.Power * k4 * k5 * k6 * k7 * k8;
            model.GradualFailure = (1 - Math.Exp(-model.Lambda * model.Time)) * model.Power;

            return model;
        }

        /// <summary>
        /// Расчет Com модели
        /// </summary>
        /// <param name="model"></param>
        public static ComModel CalculationComModel(CalculationModel model)
        {
            ComModel comModel = new ComModel();
            comModel.Com_4 = model.K4 / 1000.0 * 1.5;
            comModel.Com_5 = model.K5 / 1000.0 * 1.5;
            comModel.Com_6 = model.K6 / 1000.0 * 1.5;
            comModel.Com_7 = model.K7 / 1000.0 * 1.5;
            comModel.Com_8 = model.K8 / 1000.0 * 1.5;

            return comModel;
        }

        /// <summary>
        /// Расчет типов
        /// </summary>
        /// <param name="model"></param>
        public static TypeModel CalculationTypes(CalculationModel model)
        {
            // Расчет типов
            TypeModel typeModel = new TypeModel();
            typeModel.Type15 = 1.3 * k4 * k7 * k8 * 100;
            typeModel.Type17 = 1.1 * k5 * k6 * k7 * k8 * 100;
            typeModel.Type30 = 0.3 * k7 * 100;
            typeModel.Type50 = 0.1 * k8 * 100;
            typeModel.Type80 = 0.05 * k7 * k8 * 100;
            typeModel.Type200 = 0.01 * k7 * k8 * 100;

            return typeModel;
        }
    }
}
