namespace WebApi.Helpers;

using AutoMapper;
using ProjectManagerApi.Entities;
using ProjectManagerApi.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // CreateRequest -> Project
        CreateMap<CreateProjectRequest, Project>();
        CreateMap<CreateTaskRequest, Task>();
        CreateMap<CreateHistoryRequest, History>();

        // UpdateRequest -> Project
        CreateMap<UpdateProjectRequest, Project>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore both null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));

        CreateMap<UpdateTaskRequest, Task>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore both null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));
    }
}