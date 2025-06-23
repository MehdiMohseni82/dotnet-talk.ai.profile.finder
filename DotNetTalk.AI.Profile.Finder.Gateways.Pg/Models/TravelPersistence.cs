using System.ComponentModel.DataAnnotations;

namespace DotNetTalk.AI.Profile.Finder.Gateways.Pg.Models;

public class TravelPersistence
{
    [Key]
    public int Id { get; set; }
    
    public string DestinationCountry { get; set; }
    
    public string? DestinationCity { get; set; }
    
    public DateTime? StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    public string? Purpose { get; set; }
    
    public PersonPersistence Person { get; set; }
}
