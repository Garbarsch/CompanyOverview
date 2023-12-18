using System.ComponentModel.DataAnnotations;

public class Employee {
    public int Id {get; set;}

    
    [StringLength(20)]
    [Required]
    public  string GivenName { get;set;}= null!;

    [StringLength(25)]
    [Required]
    public  string Surname { get;set;} = null!;

    [StringLength(25)]
    public  string? CompanyRole { get;set;}
    
    [Required]
    public   Company Company {get;set;}= null!;
    
    [EmailAddress]
    [Required]
    public  string Email {get;set;}= null!;

    public Gender Gender {get;set;}

}