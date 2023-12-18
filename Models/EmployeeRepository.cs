using Microsoft.EntityFrameworkCore;

public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ICompanyContext _context;

        public EmployeeRepository(ICompanyContext context)
        {
            _context = context;
        }

        public async Task<EmployeeDTO> Create(EmployeeCreateDTO employee)
        {
            var entity = new Employee
            {
                GivenName = employee.GivenName!,
                Surname = employee.Surname!,
                CompanyRole = employee.CompanyRole,
                Gender = employee.Gender,
                Company = await GetCompany(employee.Company!),
                Email = employee.Email!,
            };

            _context.Employees.Add(entity);

            await _context.SaveChangesAsync();

            return await Read(entity.Id);
        }

        public async Task<EmployeeDTO> Read(int employeeId)
        {
            var employees = from e in _context.Employees
                             where e.Id == employeeId
                             select new EmployeeDTO(
                                 e.Id,
                                 e.GivenName,
                                 e.Surname,
                                 e.CompanyRole,
                                 e.Email,
                                 e.Company.Name,
                                 e.Gender
                             );
                            
            
            return await employees.FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<EmployeeDTO>> Read() =>
            (await _context.Employees
                    .Select(c => new EmployeeDTO(c.Id, c.GivenName, c.Surname, c.CompanyRole,c.Email,c.Company.Name,c.Gender))
                    .ToListAsync()).AsReadOnly();
         public async Task<IReadOnlyCollection<EmployeeDTO>> ReadEmployeesFromCompany( int companyId) {
    Console.WriteLine(companyId);
    

             var employees = from e in _context.Employees
                             where e.Company.Id == companyId
                             select new EmployeeDTO(
                                 e.Id,
                                 e.GivenName,
                                 e.Surname,
                                 e.CompanyRole,
                                 e.Email,
                                 e.Company.Name,
                                 e.Gender
                             );
            return (await employees.ToListAsync()).AsReadOnly();
         }
           

                    
        public async Task<Response> Update(EmployeeUpdateDTO employee)
        {
            var entity = await _context.Employees.FindAsync(employee.Id);

            if (entity == null)
            {
                return Response.NotFound;
            }

            entity.GivenName = employee.GivenName!;
            entity.Surname = employee.Surname!;
            entity.CompanyRole = employee.CompanyRole;
            entity.Company = await GetCompany(employee.Company!);
            entity.Email = employee.Email!;
            entity.Gender = employee.Gender;

            await _context.SaveChangesAsync();

            return Response.Updated;
        }

        public async Task<Response> Delete(int employeeId)
        {
            var entity = await _context.Employees.FindAsync(employeeId);

            if (entity == null)
            {
                return Response.NotFound;
            }

            _context.Employees.Remove(entity);
            await _context.SaveChangesAsync();

            return Response.Deleted;
        }

        private async Task<Company> GetCompany(string name) =>
            await _context.Companies.FirstOrDefaultAsync(c => c.Name == name) ??
            new Company { Name = name };

 
}
