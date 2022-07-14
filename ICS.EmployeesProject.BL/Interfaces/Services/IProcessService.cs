namespace ICS.EmployeesProject.BL.Interfaces.Services
{
    public interface IProcessService
    {
        bool OpenExcel(List<string[]> headerRow, List<object[]> cellData);
    }
}
