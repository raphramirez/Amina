using Amina.Domain.Common.Models;

namespace Amina.Domain.Project.ValueObjects;

public sealed class ProjectId : ValueObject
{
    private ProjectId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; private set; }

    public static ProjectId CreateUnique()
    {
        return new ProjectId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}