
using System.ComponentModel.DataAnnotations;

public class Company{

    public int Id {get;set;}
    

    [StringLength(20)]
    [Required]
    public  string Name { get;set;}= null!;

    }