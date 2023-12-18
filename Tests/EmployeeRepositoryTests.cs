using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class EmployeeRepositoryTests : IDisposable
    {
        private readonly CompanyContext _context;
        private readonly EmployeeRepository _repository;

        public EmployeeRepositoryTests()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<CompanyContext>();
            builder.UseSqlite(connection);
            var context = new CompanyContext(builder.Options);
            context.Database.EnsureCreated();

            var novoNordisk = new Company { Id = 1, Name = "Novo Nordisk" };
            var tesla = new Company { Id = 2, Name = "Tesla" };

            var employeeJorgen = new Employee {Id = 1, Company = novoNordisk,Email = "employeeJorgen@gmail.com",GivenName="employeeJorgen",Surname="Borgen",CompanyRole="Salesman",Gender=Gender.Male};
            var employeeSoren = new Employee {Id = 2, Company = tesla,Email = "employeeSoren@gmail.com",GivenName="employeeSoren",Surname="Sturen",CompanyRole="COO",Gender=Gender.Male };

            context.Employees.AddRange(employeeJorgen,employeeSoren);

            context.SaveChanges();

            _context = context;
            _repository = new EmployeeRepository(_context);
        }


        [Fact]
        public async Task Create_creates_new_employee_with_generated_id()
        {
            var repository = new EmployeeRepository(_context);

            var employee = new EmployeeCreateDTO
            {
                GivenName = "Rasmus",
                Surname = "Garbarsch",
                Company = "Tesla",
                CompanyRole = "Programmer",
                Email = "rasmus@gmail.com",
                Gender = Gender.Male,
            };

            var created = await repository.Create(employee);

            Assert.Equal(3, created.Id);
            Assert.Equal("Rasmus", created.GivenName);
            Assert.Equal("Garbarsch", created.Surname);
            Assert.Equal("Tesla", created.Company);
            Assert.Equal("rasmus@gmail.com", created.Email);
            Assert.Equal(Gender.Male, created.Gender);
        }




          public void Dispose()
        {
            _context.Dispose();
        }
    }