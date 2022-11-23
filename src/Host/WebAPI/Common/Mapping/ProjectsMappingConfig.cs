using Amina.Contracts.Projects;
using Amina.Domain.Project;
using Mapster;

namespace Amina.WebAPI.Common.Mapping;

public class ProjectsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Project, ProjectResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
    }
}