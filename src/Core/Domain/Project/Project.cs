using Amina.Domain.Common.Models;
using Amina.Domain.Project.ValueObjects;

namespace Amina.Domain.Project;

public sealed class Project : AggregateRoot<ProjectId>
{
    private Project(
        ProjectId id,
        string name,
        string description)
        : base(id)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public static Project Create(
        string name,
        string description)
    {
        return new Project(ProjectId.CreateUnique(), name, description);
    }
}