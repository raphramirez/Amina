using Amina.Domain.Common.Models;

namespace Amina.Domain.Project.ValueObjects;

public sealed class ProjectId : ValueObject
{
    public Guid Value { get; private set; }

    private ProjectId(Guid value)
    {
        Value = value;
    }

    public static ProjectId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}