using Azure;

public interface IEmployeeRepository 
{

    Task<EmployeeDTO> Create(EmployeeCreateDTO employee);
    Task<EmployeeDTO> Read(int employeeid);
    Task<IReadOnlyCollection<EmployeeDTO>> Read();
    Task<Response> Update(EmployeeUpdateDTO employee);
    Task<Response> Delete(int employeId);
    Task<IReadOnlyCollection<EmployeeDTO>> ReadEmployeesFromCompany( int companyId);
    }