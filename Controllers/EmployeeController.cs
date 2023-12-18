

using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _repository;

    public EmployeeController(IEmployeeRepository repository){

        _repository = repository;
    }
    [HttpGet]
    public async Task<IEnumerable<EmployeeDTO>> Get()
    {
        return await _repository.Read();

    }
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDTO>> Get(int id)
    {
        var employee = await _repository.Read(id);
        return employee == null? NotFound() : employee;

    }
     [HttpGet("company/{companyId}")]
    public async Task<IEnumerable<EmployeeDTO>> GetAllEmployeesFromCompany(int companyid)
    {   
        var employees = await _repository.ReadEmployeesFromCompany(companyid);
        return   employees;

    }
    
    [HttpPost]
    public async Task<EmployeeDTO> Post (EmployeeCreateDTO employee){
        return await _repository.Create(employee);

    }

    [HttpPut]
    public async Task<IActionResult> Put( EmployeeUpdateDTO employee)
    {
        var response = await _repository.Update(employee);

        if(response == global::Response.Updated){
            return NoContent();
        }
        return NotFound();
    }  
[HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id){
        var response = await _repository.Delete(id);

        if(response == global::Response.Deleted){
            return NoContent();
        }
        return NotFound();
    }
}
