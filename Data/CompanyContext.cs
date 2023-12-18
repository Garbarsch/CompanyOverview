using Microsoft.EntityFrameworkCore;

public class CompanyContext: DbContext, ICompanyContext {

   
     public CompanyContext(DbContextOptions<CompanyContext> options) : base(options) {}

     public DbSet<Employee> Employees {get;set;} 
     public DbSet<Company> Companies {get;set;}


}