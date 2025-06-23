using System.ComponentModel.DataAnnotations;

namespace DotNetTalk.AI.Profile.Finder.Gateways.Sql.Models;

public class CompanyPersistence
{
    [Key]
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string? Industry { get; set; }
    
    public string? Address { get; set; }
    
    public virtual List<PersonCompanyPersistence> PersonCompanies { get; set; } = [];
}