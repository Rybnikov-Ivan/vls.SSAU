using System;
using System.Collections.Generic;
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
        public void SaveEndOfBase(List<ComModel> comModels, string name, string group)
        {
            int j = 0;
            ComModel [] arr = comModels.ToArray();
            // Загрузить Excel, затем создать новую пустую рабочую книгу
            Application excelApp = new Application();

            // Сделать приложение Excel видимым
            excelApp.Visible = true;
            excelApp.Workbooks.Add();
            _Worksheet workSheet = excelApp.ActiveSheet;
            // Установить заголовки столбцов в ячейках 
            workSheet.Cells[1, 1] = "com_1";
            workSheet.Cells[1, 2] = "com_2";
            workSheet.Cells[1, 3] = "com_3";
            workSheet.Cells[1, 4] = "com_4";
            workSheet.Cells[1, 5] = "com_5";
            workSheet.Cells[1, 6] = "com_6";
            workSheet.Cells[1, 7] = "com_7";
            workSheet.Cells[1, 8] = "com_8";

            for (int i = 2; i < comModels.Count; i++)
            {
                workSheet.Cells[i, 1] = arr[j].Com_1;
                workSheet.Cells[i, 2] = arr[j].Com_2.ToString();
                workSheet.Cells[i, 3] = arr[j].Com_3.ToString();
                workSheet.Cells[i, 4] = arr[j].Com_4.ToString();
                workSheet.Cells[i, 5] = arr[j].Com_5.ToString();
                workSheet.Cells[i, 6] = arr[j].Com_6.ToString();
                workSheet.Cells[i, 7] = arr[j].Com_7.ToString();
                workSheet.Cells[i, 8] = arr[j].Com_8.ToString();
                j++;
            }
            excelApp.DisplayAlerts = false;
            workSheet.SaveAs(string.Format(@"{0}\" + $"{name}-" + $"{group}" + ".xlsx", Environment.CurrentDirectory));
        }
    }
}
