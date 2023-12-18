using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Moq;
using Xunit;

public class EmployeeControllerTests{

    [Fact]
    public async Task Get_returns_Employees_from_repo()
    {
        var expected = new EmployeeDTO[0];
        var repository = new Mock<IEmployeeRepository>();
        repository.Setup(m=>m.Read()).ReturnsAsync(expected);
        var controller = new EmployeeController(repository.Object);

        var actual = await controller.Get();

        Assert.Equal(expected,actual);
 
 
    }
    

     [Fact]
    public async Task Put_updates_Employee()
    {
        var employee = new EmployeeUpdateDTO();
        var repository = new Mock<IEmployeeRepository>();
        repository.Setup(m=>m.Update(employee)).ReturnsAsync(Response.Updated);
        var controller = new EmployeeController(repository.Object);

        var result = await controller.Put(employee);

        Assert.IsType<NoContentResult>(result);
    }
    
}