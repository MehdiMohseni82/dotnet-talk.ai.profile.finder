using System.ComponentModel.DataAnnotations;

namespace DotNetTalk.AI.Profile.Finder.Gateways.Pg.Models;

public class InsurancePersistence
{
    [Key]
    public int Id { get; set; }
    
    public string InsuranceType { get; set; }
    
    public string Provider { get; set; }
    
    public DateTime? ValidFrom { get; set; }
    
    public DateTime? ValidTo { get; set; }
    
    public virtual PersonPersistence Person { get; set; }
}