using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pgvector;

namespace DotNetTalk.AI.Profile.Finder.Gateways.Pg.Models;

public class PersonPersistence
{
    [Key]
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public DateTime? BirthDate { get; set; }
    
    public string? Gender { get; set; }
    
    public string? JobTitle { get; set; }
    
    public string? Bio { get; set; }

    [Column(TypeName = "vector(768)")]
    public Vector? Embedding { get; set; }

    public virtual List<PersonCompanyPersistence> PersonCompanies { get; set; } = [];
    
    public virtual List<TravelPersistence> Travels { get; set; } = [];
    
    public virtual List<InsurancePersistence> Insurances { get; set; } = [];
}
