using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Interfaces;
using Api.Models.DTO.User;
using Api.Models;
using BC = BCrypt.Net.BCrypt;

namespace Api.Services
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
            if (user == null) return false;
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
            var _user = await _userRepository.GetById(user.Id);
            if (_user == null) return false;

            bool atualizou = true;

            try
            {
                atualizou =  await _userRepository.Update(new User
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name
                });

            }
            catch(Microsoft.EntityFrameworkCore.DbUpdateException dbException)
            {
                string innerMessage = dbException.InnerException.Message;

                if (innerMessage.Contains("NameIsUnique")) throw new Exception("Nome j� existe");
                if (innerMessage.Contains("EmailIsUnique")) throw new Exception("Email j� existe");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return atualizou;
        }

        public async Task<bool> UpdatePassword(UserPutPassword user)
        {
            User userFromDB = await _userRepository.GetById(user.Id);

            if (userFromDB == null) return false;

            string oldPasswordFromDB = userFromDB.Password;            

            if(!BC.Verify(user.OldPassword, oldPasswordFromDB)) return false;

            userFromDB.Password = user.NewPassword;

            return await _userRepository.UpdatePassword(userFromDB);

        }
 
        public async Task<bool> NameExists(string name){
            return await _userRepository.NameExists(name);
        }
 
        public async Task<bool> EmailExists(string email){
            return await _userRepository.EmailExists(email);
        }

        public async Task<bool> NameExistsUpdate(string name, int id){
            return await _userRepository.NameExistsToDifferentId(name, id);
        }

        public async Task<bool> EmailExistsUpdate(string name, int id){
            return await _userRepository.EmailExistsToDifferentId(name, id);
        }

    }
}