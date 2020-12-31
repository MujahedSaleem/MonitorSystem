using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.JSInterop;
using MonitorSystem.Entity;
using MonitorSystem.Extention;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace MonitorSystem.Service
{
    public class ExcelService: IExcelService
    {
        private readonly IJSRuntime jsRuntime;
                private readonly IWebHostEnvironment _hostingEnvironment;
                private readonly string rootPath;

        public ExcelService(IJSRuntime jsRuntime, IWebHostEnvironment hostingEnvironment)
        {
            this.jsRuntime = jsRuntime;
            _hostingEnvironment = hostingEnvironment;
            rootPath = _hostingEnvironment.WebRootPath;
        }

        public void Export(List<Movement> m)
        {
            byte[] fileContent = null;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage();
            var workSheet = package.Workbook.Worksheets.Add("Sheet1");
            m = m.OrderBy(a => a.MovementDate).ToList();
            #region Headr Row

            var ColumnName = new List<string>(){"תעודה",   "תאריך",   "אסמכתא",  "מס' רכב",	"שם מוביל","	שם לקוח	","מיקום/הערות","תאור חומר	","ברוטו","	טרה","נטו"};

            workSheet.Cells[1, 2, 1, 3].Merge = true;
            workSheet.Cells[2, 2, 2, 3].Merge = true;
            workSheet.Cells[3, 2, 3, 3].Merge = true;
            workSheet.Cells[4, 2, 4, 3].Merge = true;
            workSheet.Cells[5, 2, 5, 6].Merge = true;
            workSheet.Cells[1, 2, 1, 3].Value = "ר.ע.אופק בע\"מ";
            workSheet.Cells[2, 2, 2, 3].Value = "דוח שקילות כללי ";
            workSheet.Cells[3, 2, 3, 3].Value = $"{m.First().Reference} רמת בית שמש";
            workSheet.Cells[4, 2, 4, 3].Value = "(מפורט :ממוין לפי מספר תעודה )";
            workSheet.Cells[5, 2, 5, 6].Value = "כניסת ויציאת חומר : סוגי תעודות :רגילות (מהמשקל ), ידניות (מעדכנות)";
            workSheet.Cells[7, 2].Value = "נכון לתקופה:";
            workSheet.Cells[7, 3].Value = m.First().MovementDate.ToString("d");
            workSheet.Cells[7, 4].Value = m.Last().MovementDate.ToString("d");
            workSheet.Cells[7, 2, 7, 12].Style.Border.BorderAround( ExcelBorderStyle.Medium);
            ColumnName.WithIndex().ToList().ForEach(tuple => workSheet.Cells[7, tuple.index+2].Value = tuple.item);
            Image img = Image.FromFile($"{rootPath}\\logo.png");
            var resizedImage = img.ResizeImage(new Size(100,100));
            var excelImage = workSheet.Drawings.AddPicture("My Logo", resizedImage);

            //add the image to row 20, column E
            excelImage.SetPosition(1, 0, 12, 0);

            #endregion

            #region Body
            m.WithIndex().ToList().ForEach(t =>
            {
                workSheet.Cells[8 + t.index, 2].Value = t.item.CertificateNumber.ToString();
                workSheet.Cells[8 + t.index, 3].Value = t.item.MovementDate.ToString("d");
                workSheet.Cells[8 + t.index, 4].Value = t.item.Reference;
                workSheet.Cells[8 + t.index, 5].Value = t.item.VehicleNo;
                workSheet.Cells[8 + t.index, 6].Value = t.item.CompanyName;
                workSheet.Cells[8 + t.index, 7].Value = t.item.Contractor.Name;
                workSheet.Cells[8 + t.index, 8].Value = t.item.Place;
                workSheet.Cells[8 + t.index, 9].Value = t.item.MaterialTypes;
                workSheet.Cells[8 + t.index, 10].Value = t.item.Total;
                workSheet.Cells[8 + t.index, 11].Value = t.item.AddedValue;
                workSheet.Cells[8 + t.index, 12].Value = t.item.Net;
            });
            workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
            fileContent = package.GetAsByteArray();
            #endregion

             jsRuntime.InvokeAsync<Movement>(
                "saveAsFile",
                m.First().Reference,
                Convert.ToBase64String(fileContent)
            );
        }
        private int Pixel2MTU(int pixels)
        {
            int mtus = pixels * 9525;
            return mtus;
        }
    }
}
