using UsersAPI.Domain.Interfaces.Messages;
using UsersAPI.Domain.ValueObjects;

namespace UsersAPI.Infra.Messages.Producers
{
    public class UserMessageProducer : IUserMessageProducer
    {
        public void Send(UserMessageVO userMessage)
        {
            throw new NotImplementedException();
        }
    }
}
