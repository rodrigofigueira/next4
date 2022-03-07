using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Interfaces
{
    public interface IUserRepository
    {

        public Task<User> Post(User user);

        public Task<User> GetByEmailAndPassword(string email, string password);

        public Task<User> GetByUsernameAndPassword(string username, string password);

        public Task<bool> Delete(User user);

        public Task<bool> DeleteRange(List<User> users);

        public void Clear();
        public Task<User> GetById(int id);

        public Task<List<User>> GetListByNameStartsWith(string name);

        public Task<List<User>> GetListByEmailStartsWith(string email);

        public Task<bool> UpdatePassword(User user);
        public Task<bool> Update(User user);

        public Task<List<User>> GetAll();

        public Task<bool> NameExists(string name);       

    }
}