namespace BackendApp.Helpers;

using AutoMapper;
using BackendApp.Entities;
using BackendApp.Models.Users;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // CreateRequest -> User
        CreateMap<CreateRequest, Users>();

        // UpdateRequest -> User
        CreateMap<UpdateRequest, Users>()
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