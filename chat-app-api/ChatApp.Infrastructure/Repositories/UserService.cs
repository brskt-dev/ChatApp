using ChatApp.Domain.Entities;
using ChatApp.Infrastructure.Repositories;

namespace ChatApp.Application.Services
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetUsers() => await _userRepository.GetAll();
    }
}