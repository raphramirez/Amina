using Amina.Domain.Common.Models;
using Amina.Domain.Project.ValueObjects;

namespace Amina.Domain.Project;

public sealed class Project : AggregateRoot<ProjectId>
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    private Project(
        ProjectId id,
        string name,
        string description) : base(id)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public static Project Create(
        string name,
        string description)
    {
        return new(ProjectId.CreateUnique(), name, description);
    }
}