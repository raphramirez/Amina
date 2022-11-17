namespace Amina.Domain.Entities;

public class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Lead { get; set; }
}