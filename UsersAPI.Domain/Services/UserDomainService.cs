﻿using UsersAPI.Domain.Exceptions;
using UsersAPI.Domain.Interfaces.Messages;
using UsersAPI.Domain.Interfaces.Repositories;
using UsersAPI.Domain.Interfaces.Secutiry;
using UsersAPI.Domain.Interfaces.Services;
using UsersAPI.Domain.Models;
using UsersAPI.Domain.ValueObjects;

namespace UsersAPI.Domain.Services
{
    public class UserDomainService : IUserDomainService
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly IUserMessageProducer? _userMessageProducer;
        private readonly ITokenService? _tokenService;

        public UserDomainService(IUnitOfWork? unitOfWork, 
            IUserMessageProducer? userMessageProducer,
            ITokenService? tokenService)
        {
            _unitOfWork = unitOfWork;
            _userMessageProducer = userMessageProducer;
            _tokenService = tokenService;
        }

        public void Add(User user)
        {
            if (Find(user.Email) is not null)
                throw new EmailAlreadyExistsException(user.Email);

            _unitOfWork?.UserRepository.Add(user);
            _unitOfWork?.SaveChanges();
            
            _userMessageProducer?.Send(new UserMessageVO
            {
                Id = user.Id,
                To = user.Email,
                SendedAt = DateTime.UtcNow,
                Subject = "User account",
                Body = $@"Hello, {user.Name}! Your account has been created successfully!"
            });
        }

        public void Delete(User user)
        {
            _unitOfWork?.UserRepository.Delete(user);
            _unitOfWork?.SaveChanges();
        }

        public User? Find(Guid id) => _unitOfWork?.UserRepository.FindById(id);

        public User? Find(string email) => _unitOfWork?.UserRepository.Find(u => u.Email.Equals(email));

        public User? Find(string email, string password) => _unitOfWork?.UserRepository.Find(u => u.Email.Equals(email)
            && u.Password.Equals(password));

        public void Update(User user)
        {
            _unitOfWork?.UserRepository.Update(user);
            _unitOfWork?.SaveChanges();
        }
        
        public string Authenticate(string email, string password)
        {
            var user = Find(email, password);

            if (user is null) throw new AccessDeniedException();

            var userAuth = new UserAuthVO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = "USER_ROLE",
                SignedAt = DateTime.Now
            };

            return _tokenService?.CreateToken(userAuth);
        }

        public void Dispose() => _unitOfWork?.Dispose();
    }
}
