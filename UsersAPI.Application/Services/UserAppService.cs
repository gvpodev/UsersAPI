using AutoMapper;
using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;
using UsersAPI.Application.Interfaces.Application;
using UsersAPI.Domain.Interfaces.Services;
using UsersAPI.Domain.Models;

namespace UsersAPI.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserDomainService? _userDomainService;
        private readonly IMapper? _mapper;

        public UserAppService(IUserDomainService? userDomainService, IMapper? mapper)
        {
            _userDomainService = userDomainService;
            _mapper = mapper;
        }

        public UserResponseDto? Add(UserAddRequestDto request)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                CreatedAt = DateTime.UtcNow
            };

            _userDomainService?.Add(user);

            return _mapper?.Map<UserResponseDto>(user);
        }

        public UserResponseDto Delete(Guid id)
        {
            var user = _userDomainService?.Find(id);

            _userDomainService.Delete(user);

            return _mapper.Map<UserResponseDto>(user);
        }

        public UserResponseDto Get(Guid id)
        {
            var user = _userDomainService?.Find(id);

            return _mapper.Map<UserResponseDto>(user);
        }

        public UserResponseDto Update(Guid id, UserUpdateRequestDto request)
        {
            var user = _userDomainService?.Find(id);
            user.Name = request.Name;

            _userDomainService?.Update(user);

            return _mapper.Map<UserResponseDto>(user);
        }

        public void Dispose() => _userDomainService?.Dispose();
    }
}
