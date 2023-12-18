

using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyRepository _repository;

    public CompanyController(ICompanyRepository repository){

        _repository = repository;
    }
    [HttpGet]
    public async Task<IEnumerable<CompanyDTO>> Get()
    {
        return await _repository.Read();

    }
    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyDTO>> Get(int id)
    {
        var company = await _repository.Read(id);
        return company == null? NotFound() : company;

    }
    
    [HttpPost]
    public async Task<(Response,CompanyDTO)> Post (CompanyCreateDTO company){
        return await _repository.Create(company);

    }

    [HttpPut]
    public async Task<IActionResult> Put(CompanyUpdateDTO company)
    {
        Console.WriteLine(company.Id);
        var response = await _repository.Update(company);

        if(response == global::Response.Updated){
            return NoContent();
        }
        return NotFound();
    }  
[HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id){
        Console.WriteLine(id);
        var response = await _repository.Delete(id);

        if(response == global::Response.Deleted){
            return NoContent();
        }
        return NotFound();
    }
}
