
using Azure;
using Microsoft.EntityFrameworkCore;

public class CompanyRepository : ICompanyRepository
{

    private readonly ICompanyContext _context;
    public CompanyRepository(ICompanyContext context){
        _context = context;
    }
    public async Task<(Response,CompanyDTO)> Create(CompanyCreateDTO company)
    {
        //look if there is another company with same name
        var conflict =
             await _context.Companies
                            .Where(c => c.Name == company.Name)
                            .Select(c => new CompanyDTO(c.Id, c.Name))
                            .FirstOrDefaultAsync();

        if (conflict != null)
         {
            return (Response.Conflict,conflict);
         }

        var entity = new Company {Name = company.Name};
        _context.Companies.Add(entity);

       await _context.SaveChangesAsync();

        return (Response.Created, new CompanyDTO(entity.Id, entity.Name));
    }

    public async Task<Response> Delete(int companyId)
    {
        //Dont remove company if there are Employees assoicated with it
        var employees = await _context.Employees.FirstOrDefaultAsync(e => e.Company.Id ==  companyId);
        var company = await _context.Companies.FirstOrDefaultAsync(c => c.Id ==  companyId);
        
        if(company == null){
            return Response.NotFound;
        }
        if(employees != null)
        {
            return Response.Conflict;
        }
        _context.Companies.Remove(company);
       await _context.SaveChangesAsync();
        return Response.Deleted;
    
    }

    public async  Task<CompanyDTO?> Read(int companyId)
    {
        var companies = from c in _context.Companies
                        where c.Id == companyId
                        select new CompanyDTO(c.Id,c.Name);
        return await companies.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<CompanyDTO>> Read() =>
            (await _context.Companies
                           .Select(c => new CompanyDTO(c.Id, c.Name))
                           .ToListAsync())
                           .AsReadOnly();


    public async  Task<Response> Update(CompanyUpdateDTO company)
    {
        
        var entity = await _context.Companies.FindAsync(company.Id);
        if (entity == null){
            return Response.NotFound;
        }
        entity.Name = company.Name;
       await _context.SaveChangesAsync();
        return Response.Updated;
    }
}