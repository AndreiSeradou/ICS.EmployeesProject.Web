using ICS.EmployeesProject.BL.Interfaces.Services;
using OfficeOpenXml;
using System.Diagnostics;

namespace ICS.EmployeesProject.BL.Services
{
    public class ProcessService : IProcessService
    {
        public bool OpenExcel(List<string[]> headerRow, List<object[]> cellData)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            var fileName = DateTime.UtcNow.ToString().Replace(':', '.').Replace(' ', '.');

            using (var excel = new ExcelPackage(new FileInfo($@"reports\{fileName}.xlsx")))
            {
                excel.Workbook.Worksheets.Add("Worksheet1");

                string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";

                var worksheet = excel.Workbook.Worksheets["Worksheet1"];

                worksheet.Cells[headerRange].LoadFromArrays(headerRow);
                worksheet.Cells[2, 1].LoadFromArrays(cellData);

                worksheet.Cells[headerRange].Style.Font.Bold = true;
                worksheet.Cells[headerRange].Style.Font.Size = 14;
                worksheet.Cells[headerRange].Style.Font.Color.SetColor(System.Drawing.Color.Blue);

                excel.Save();

                bool isExcelInstalled = Type.GetTypeFromProgID("Excel.Application") != null;

                if (isExcelInstalled)
                {
                    string myPath = $@"reports\{fileName}.xlsx";
                    ProcessStartInfo ps = new ProcessStartInfo();
                    ps.FileName = "excel"; // "EXCEL.EXE" also works
                    ps.Arguments = myPath;
                    ps.UseShellExecute = true;
                    Process.Start(ps);
                }

                return isExcelInstalled;
            }
        }
    }
}
