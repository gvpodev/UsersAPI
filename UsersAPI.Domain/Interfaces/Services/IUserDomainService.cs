using UsersAPI.Domain.Models;

namespace UsersAPI.Domain.Interfaces.Services
{
    public interface IUserDomainService : IDisposable
    {
        void Add(User user);
        void Update(User user);
        void Delete(User user);
        User? Find(Guid id);
        User? Find(string email);
        User? Find(string email, string password);
        string Authenticate(string email, string password);
    }
}
