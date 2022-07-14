using ICS.EmployeesProject.BL.Interfaces.Services;
using ICS.EmployeesProject.Configuration;
using OfficeOpenXml;
using System.Diagnostics;

namespace ICS.EmployeesProject.BL.Services
{
    public class ProcessService : IProcessService
    {
        public bool OpenExcel(List<string[]> headerRow, List<object[]> cellData)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            var filePath = string.Format(ApplicationConfiguration.FilePath, DateTime.UtcNow.ToString().Replace(':', '.').Replace(' ', '.'));

            using (var excel = new ExcelPackage(new FileInfo(filePath)))
            {
                excel.Workbook.Worksheets.Add(ApplicationConfiguration.WorksheetName);

                string headerRange = ApplicationConfiguration.TableCell + Char.ConvertFromUtf32(headerRow[0].Length + 64) + ApplicationConfiguration.TableNumberOfLength;

                var worksheet = excel.Workbook.Worksheets[ApplicationConfiguration.WorksheetName];

                worksheet.Cells[headerRange].LoadFromArrays(headerRow);
                worksheet.Cells[2, 1].LoadFromArrays(cellData);

                worksheet.Cells[headerRange].Style.Font.Bold = true;
                worksheet.Cells[headerRange].Style.Font.Size = 14;
                worksheet.Cells[headerRange].Style.Font.Color.SetColor(System.Drawing.Color.Blue);

                excel.Save();

                bool isExcelInstalled = Type.GetTypeFromProgID(ApplicationConfiguration.ExcelSystemName) != null;

                if (isExcelInstalled)
                {
                    string myPath = filePath;

                    ProcessStartInfo ps = new ProcessStartInfo();

                    ps.FileName = ApplicationConfiguration.ExcelFileName; 
                    ps.Arguments = myPath;
                    ps.UseShellExecute = true;

                    Process.Start(ps);
                }

                return isExcelInstalled;
            }
        }
    }
}
