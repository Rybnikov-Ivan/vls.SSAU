using System;
using System.IO;
using System.Text;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Office.Interop.Excel;
using SSAU.vls.AssistingsWindows.Models;
using SSAU.vls.FIRST.lr.Calculation.Models;
using SSAU.vls.FIRST.lr.ExportToExcel.Models;
using Excel = Microsoft.Office.Interop.Excel;

namespace SSAU.vls.FIRST.lr.ExportToExcel
{
    public class ExportExcel
    {
        ModelLoginForm modelLoginForm = new ModelLoginForm();
        ComModel comModel = new ComModel();
        string[,] resultArray = new string[1000, 8];
        public void SaveEndOfBase()
        {

            // Загрузить Excel, затем создать новую пустую рабочую книгу
            Excel.Application excelApp = new Excel.Application();

            // Сделать приложение Excel видимым
            excelApp.Visible = true;
            excelApp.Workbooks.Add();
            Excel._Worksheet workSheet = excelApp.ActiveSheet;
            // Установить заголовки столбцов в ячейках 
            workSheet.Cells[1, 1] = "com_1";
            workSheet.Cells[1, 2] = "com_2";
            workSheet.Cells[1, 3] = "com_3";
            workSheet.Cells[1, 4] = "com_4";
            workSheet.Cells[1, 5] = "com_5";
            workSheet.Cells[1, 6] = "com_6";
            workSheet.Cells[1, 7] = "com_7";
            workSheet.Cells[1, 8] = "com_8";
            

            excelApp.DisplayAlerts = false;
            workSheet.SaveAs(string.Format(@"{0}\lab1" + modelLoginForm.Name + modelLoginForm.Group + ".xlsx", Environment.CurrentDirectory));

            excelApp.Quit();
        }

        public void DataToExport(CalculationModel model, int count)
        {
            comModel.Com_1 = "1 Лр";
            comModel.Com_2 = 10;
            comModel.Com_3 = model.Power;
            comModel.Com_4 = model.K4;
            comModel.Com_5 = model.K5;
            comModel.Com_6 = model.K6;
            comModel.Com_7 = model.K7;
            comModel.Com_8 = model.K8;

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWb = xlApp.Workbooks.Open(string.Format(@"{0}\lab1" + modelLoginForm.Name + modelLoginForm.Group + ".xlsx", Environment.CurrentDirectory)); //открываем Excel файл
            Excel.Worksheet xlSht = xlWb.Sheets[1]; //первый лист в файле

            xlSht.Cells[count, 1] = comModel.Com_1;
            xlSht.Cells[count, 2] = comModel.Com_2.ToString();
            xlSht.Cells[count, 3] = comModel.Com_3.ToString();
            xlSht.Cells[count, 4] = comModel.Com_4.ToString();
            xlSht.Cells[count, 5] = comModel.Com_5.ToString();
            xlSht.Cells[count, 6] = comModel.Com_6.ToString();
            xlSht.Cells[count, 7] = comModel.Com_7.ToString();
            xlSht.Cells[count, 8] = comModel.Com_8.ToString();

            xlWb.Close(true); //закрыть и сохранить книгу
            xlApp.Quit();

        }
    }
}
