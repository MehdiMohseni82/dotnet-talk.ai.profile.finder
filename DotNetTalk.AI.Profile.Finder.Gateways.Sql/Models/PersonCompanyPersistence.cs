using System;
using System.ComponentModel.DataAnnotations;

namespace DotNetTalk.AI.Profile.Finder.Gateways.Sql.Models;

public class PersonCompanyPersistence
{
    [Key]
    public int Id { get; set; }
    
    public int CompanyId { get; set; }
    
    public string? JobTitleAtCompany { get; set; }
    
    public DateTime? FromDate { get; set; }
    
    public DateTime? ToDate { get; set; }
    
    public virtual PersonPersistence Person { get; set; }
    
    public virtual CompanyPersistence Company { get; set; }
}