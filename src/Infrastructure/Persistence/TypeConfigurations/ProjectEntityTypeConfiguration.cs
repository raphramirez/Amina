using Amina.Domain.Project;
using Amina.Domain.Project.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Amina.Infrastructure.Persistence.TypeConfigurations;

public class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
        };

        builder.Property(e => e.Id)
            .HasConversion(
                    v => JsonConvert.SerializeObject(v, settings),
                    v => JsonConvert.DeserializeObject<ProjectId>(v, settings));
    }
}