using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Xunit;

public class CompanyRepositoryTests : IDisposable{


    private readonly ICompanyContext _context;
    private readonly CompanyRepository _repo;
    public CompanyRepositoryTests(){
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<CompanyContext>();
        builder.UseSqlite(connection);
        var context = new CompanyContext(builder.Options);
        context.Database.EnsureCreated();
        context.Companies.Add(new Company {Name = "Comply Today"});
        context.SaveChanges();

        _context = context;
        _repo = new CompanyRepository(_context);


    }

    [Fact]
    public async Task Create_company_returns_company_with_id(){
        

        var company = new CompanyCreateDTO{Name= "Comply Today"};
        var created = await _repo.Create(company);

        Assert.Equal((Response.Created,new CompanyDTO(2, "Comply Today")), created);
    }


    [Fact]
    public async Task Read_company_returns_company_with_id(){
      
        var company = await _repo.Read(1);

        Assert.Equal(new CompanyDTO(1, "Comply Today"), company);
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}