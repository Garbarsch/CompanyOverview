

using System.ComponentModel.DataAnnotations;

public record EmployeeDTO(int Id, string GivenName, string Surname, string? CompanyRole, string Email, string Company, Gender Gender);



public record EmployeeCreateDTO{
    
    [StringLength(20)]
    [Required]
    public string GivenName { get;init;}= null!;

    
    [StringLength(25)]
    [Required]
    public  string Surname { get;init;} = null!;

    [StringLength(25)]
    public  string? CompanyRole { get;init;}
    
    [Required]
    public   string Company {get;init;}= null!;
    
    [EmailAddress]
    public string? Email {get;init;}

    public Gender Gender {get;init;}
}
public record EmployeeUpdateDTO : EmployeeCreateDTO
    {
        public int Id { get; init; }
    }