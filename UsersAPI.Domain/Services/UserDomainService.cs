using UsersAPI.Domain.Interfaces.Repositories;
using UsersAPI.Domain.Interfaces.Services;
using UsersAPI.Domain.Models;

namespace UsersAPI.Domain.Services
{
    public class UserDomainService : IUserDomainService
    {
        private readonly IUnitOfWork? _unitOfWork;

        public UserDomainService(IUnitOfWork? unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(User user)
        {
            _unitOfWork?.UserRepository.Add(user);
            _unitOfWork?.SaveChanges();
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

        public void Dispose() => _unitOfWork?.Dispose();
    }
}
