
using System.ComponentModel.DataAnnotations;



    public record CompanyCreateDTO{
        [Required, StringLength(20)] 
        public string Name { get;init;} = null!;
    }

    public record CompanyDTO(int Id, [Required, StringLength(20)] string Name);
    
    public record CompanyUpdateDTO : CompanyCreateDTO
    {
        public int Id { get; init; }
    }