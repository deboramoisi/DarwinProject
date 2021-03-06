using API.DTOs;
using API.Models;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Map from -> to
            CreateMap<RegisterDto, AppUser>();
            CreateMap<Device, DeviceDto>()
                .ForMember(dest => dest.UserName, options => {
                    options.MapFrom(src => src.AppUser.UserName);
                });
            CreateMap<DeviceDto, Device>(); 
        }
    }
}