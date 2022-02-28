using System.Collections.Generic;
using System.Threading.Tasks;
using next4_api.Models.DTO.User;

namespace next4_api.Interfaces
{
    public interface IUserService
    {
        public Task<UserToken> Post(UserPost user);  
        
        public Task<UserToken> LoginByName(UserLoginByName user);

        public Task<UserToken> LoginByEmail(UserLoginByEmail user);

        public Task<UserGet> GetById(int id);

        public Task<List<UserGet>> GetByName(string name);

        public Task<List<UserGet>> GetByEmail(string email);

        public Task<bool> UpdatePassword(UserPutPassword user);

        public Task<bool> Update(UserPut user);

        public Task<bool> Delete(int id);

        public Task<bool> DeleteAllThatNameStartsWith(string name);

    }
}