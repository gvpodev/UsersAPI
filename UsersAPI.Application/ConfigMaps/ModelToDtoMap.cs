using AutoMapper;
using UsersAPI.Application.Dtos.Responses;
using UsersAPI.Domain.Models;

namespace UsersAPI.Application.ConfigMaps
{
    public class ModelToDtoMap : Profile
    {
        public ModelToDtoMap()
        {
            CreateMap<User, UserResponseDto>();
        }
    }
}
