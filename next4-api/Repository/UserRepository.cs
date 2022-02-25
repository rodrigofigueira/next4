using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using next4_api.Data;
using next4_api.Models;
using next4_api.Models.DTO.User;
using BC = BCrypt.Net.BCrypt;
using next4_api.Services;

namespace next4_api.Repository
{
    public class UserRepository
    {
        private DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        //ver como gravar passando uma lista
        public async Task<User> Post(UserPost userPost){

            DateTime now = DateTime.Now;

            User user = new User{
                Name = userPost.Name,
                Email = userPost.Email,
                Password = BC.HashPassword(userPost.Password),
                CreatedAt = now,
                UpdatedAt = now
            };            

            _context.Users.Add(user);
            
            try{
                await _context.SaveChangesAsync();
            }catch(Exception ex){
                this.Clear();
                throw ex;
            }
            
            return user;

        }

        public async Task<UserToken> GetByEmailAndPassword(string email, string password){

            var userFromBD = await _context.Users.Where(u => u.Email.Equals(email))
                                                  .FirstOrDefaultAsync();

            if(userFromBD == null 
               || !BC.Verify(password, userFromBD.Password)
            ) 
            return null;

            return new UserToken{
                Name = userFromBD.Name,
                Token = new TokenService().CreateToken(userFromBD.Name)
            };

        }        

        public async Task<bool> Delete(User user){

            _context.Users.Remove(user);            
            int changes = await _context.SaveChangesAsync();
            return changes > 0 ? true : false;

        }

        public async Task<bool> DeleteRange(List<User> users){
            _context.Users.RemoveRange(users);            
            int changes = await _context.SaveChangesAsync();
            return changes > 0 ? true : false;
        }

        public void Clear(){
            _context.Users.Local.Clear();
        }

        public async Task<UserGet> GetById(int id){

            var user = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

            if(user == null) return null;

            return new UserGet{
                Id = user.Id,
                CreatedAt = user.CreatedAt,
                Email = user.Email,
                Name = user.Name,
                UpdatedAt = user.UpdatedAt
            };

        }        

        public async Task<List<UserGet>> GetListByNameStartsWith(string name){
            
            var users = await _context.Users.Where(u => u.Name.StartsWith(name)).ToListAsync();

            List<UserGet> userGets = new List<UserGet>();

            foreach (User user in users)
            {
                userGets.Add(new UserGet{
                    Id = user.Id,
                    CreatedAt = user.CreatedAt,
                    Email = user.Email,
                    Name = user.Name,
                    UpdatedAt = user.UpdatedAt
                });
            }

            return userGets;

        }





        //public async Task<UserGet> GetByUsernameAndPassword(string name, string password)

        //public async Task<bool> UpdatePassword(UserPutPassword user)

        //public async Task<bool> Update(UserPut user)

        //public async Task<List<UserGet>> GetListByEmailStartsWith(string email)


    }
}