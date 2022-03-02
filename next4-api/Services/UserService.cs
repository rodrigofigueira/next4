using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using next4_api.Interfaces;
using next4_api.Models.DTO.User;
using next4_api.Models;
using BC = BCrypt.Net.BCrypt;

namespace next4_api.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;

        public UserService(IUserRepository userRepository){
            this._userRepository = userRepository;
            this._tokenService = new TokenService();
        }

        public async Task<bool> Delete(int id)
        {
            User user = await _userRepository.GetById(id);
            bool deleted = await _userRepository.Delete(user);            
            return deleted;
        }

        public async Task<bool> DeleteAllThatNameStartsWith(string name){
            var allUsersFromBD = await _userRepository.GetListByNameStartsWith(name);            
            await _userRepository.DeleteRange(allUsersFromBD);
            return true;
        }

        public async Task<List<UserGet>> GetByEmail(string email)
        {
            List<User> users = await _userRepository.GetListByEmailStartsWith(email);
            List<UserGet> usersToReturn = new List<UserGet>();
            
            foreach(User _user in users){
                usersToReturn.Add(new UserGet{
                    CreatedAt = _user.CreatedAt,
                    Email = _user.Email,
                    Id = _user.Id,
                    Name = _user.Name,
                    UpdatedAt = _user.UpdatedAt
                });
            }

            return usersToReturn;

        }

        public async Task<UserGet> GetById(int id)
        {
            User user = await _userRepository.GetById(id);

            if(user == null) return null;

            return new UserGet{
                CreatedAt = user.CreatedAt,
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                UpdatedAt = user.UpdatedAt
            };

        }

        public async Task<List<UserGet>> GetByName(string name)
        {
            List<User> users = await _userRepository.GetListByNameStartsWith(name);
            List<UserGet> usersToReturn = new List<UserGet>();
            
            foreach(User _user in users){
                usersToReturn.Add(new UserGet{
                    CreatedAt = _user.CreatedAt,
                    Email = _user.Email,
                    Id = _user.Id,
                    Name = _user.Name,
                    UpdatedAt = _user.UpdatedAt
                });
            }

            return usersToReturn;
        }

        public async Task<UserToken> LoginByEmail(UserLoginByEmail user)
        {
            User userFromDB = await _userRepository.GetByEmailAndPassword(user.Email, user.Password);

            if(userFromDB == null) return null;

            return new UserToken{
                Id = userFromDB.Id,
                Name = userFromDB.Name,
                Token = _tokenService.CreateToken(userFromDB.Name)
            };
        }

        public async Task<UserToken> LoginByName(UserLoginByName user)
        {
            User userFromDB = await _userRepository.GetByUsernameAndPassword(user.Name, user.Password);

            if(userFromDB == null) return null;

            return new UserToken{
                Id = userFromDB.Id,
                Name = userFromDB.Name,
                Token = _tokenService.CreateToken(userFromDB.Name)
            };
        }

        public async Task<UserToken> Post(UserPost user)
        {
            User insertedUser = await _userRepository.Post(new User{
                Email = user.Email,
                Name = user.Name,
                Password = user.Password
            });

            if(insertedUser == null) throw new Exception("Ocorreu um erro ao tentar inserir");

            return new UserToken{
                Id = insertedUser.Id,                
                Name = user.Name,
                Token = _tokenService.CreateToken(user.Name)
            };

        }

        public async Task<bool> Update(UserPut user)
        {
            return await _userRepository.Update(new User{
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            });
        }

        public async Task<bool> UpdatePassword(UserPutPassword user)
        {
            User userFromDB = await _userRepository.GetById(user.Id);

            string oldPasswordFromDB = userFromDB.Password;            

            if(!BC.Verify(user.OldPassword, oldPasswordFromDB)) return false;

            userFromDB.Password = user.NewPassword;

            return await _userRepository.UpdatePassword(userFromDB);

        }
    }
}