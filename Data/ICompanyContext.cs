
using Microsoft.EntityFrameworkCore;

public interface ICompanyContext : IDisposable{

    DbSet<Company> Companies {get;set;}
    DbSet<Employee> Employees {get;set;}

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}