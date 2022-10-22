using AutoMapper;
using ShoppingWebsite.Model;
using ShoppingWebsite.Model.DTO;

namespace ShoppingWebsite.Services.Mapper;

public class MappingProfile : Profile
{
    
    public MappingProfile() {
        // Mapping (Account, CreateAccountDTO)
        CreateMap<Account, CreateAccountDTO>()
            .ReverseMap();

        // Mapping (Account, ProfileDTO)
        CreateMap<Account, ProfileDTO>()
            .ReverseMap();
    }

}
