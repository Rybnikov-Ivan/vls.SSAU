using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSAU.vls.FIRST.lr.ExportToExcel.Models
{
    public class WorkBookExcelItem
    {
        /// <summary>
        /// Книга эксель
        /// </summary>
        public IWorkbook Workbook { get; set; }

        /// <summary>
        /// Наименование книги
        /// </summary>
        public string Name { get; set; }
    }
}
