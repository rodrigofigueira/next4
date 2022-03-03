using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using next4_api.Data;
using next4_api.Interfaces;
using next4_api.Models;
using BC = BCrypt.Net.BCrypt;

namespace next4_api.Repository
{
    public class UserRepository : IUserRepository
    {
        private DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Post(User user){

            DateTime now = DateTime.Now;

            user.Password = BC.HashPassword(user.Password);
            user.CreatedAt = now;
            user.UpdatedAt = now;
            _context.Users.Add(user);
            
            await _context.SaveChangesAsync();
            
            return user;

        }

        public async Task<User> GetByEmailAndPassword(string email, string password){

            var userFromBD = await _context.Users.Where(u => u.Email.Equals(email))
                                                 .FirstOrDefaultAsync();

            if(userFromBD == null || !BC.Verify(password, userFromBD.Password)) return null;

            return userFromBD;

        }        

        public async Task<User> GetByUsernameAndPassword(string username, string password){

            var userFromBD = await _context.Users.Where(u => u.Name.Equals(username))
                                                  .FirstOrDefaultAsync();

            if(userFromBD == null || !BC.Verify(password, userFromBD.Password)) return null;

            return userFromBD;

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

        public async Task<User> GetById(int id){
            return await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();            
        }        

        public async Task<List<User>> GetListByNameStartsWith(string name){            
            return await _context.Users.Where(u => u.Name.StartsWith(name)).ToListAsync();
        }

        public async Task<List<User>> GetListByEmailStartsWith(string email){
            return await _context.Users.Where(u => u.Email.StartsWith(email)).ToListAsync();
        }

        public async Task<bool> UpdatePassword(User user){

            User _user = await _context.Users.Where(u => u.Id == user.Id).FirstOrDefaultAsync();
            _user.Password = BC.HashPassword(user.Password);
            _context.Users.Update(_user);

            int atualizou = await _context.SaveChangesAsync();

            return atualizou > 0 ? true : false;

        }

        public async Task<bool> Update(User user){

            User _user = await _context.Users.Where(u => u.Id == user.Id).FirstOrDefaultAsync();
            _user.UpdatedAt = DateTime.Now;
            _user.Name = user.Name;
            _user.Email = user.Email;
            _context.Users.Update(_user);

            int atualizou = await _context.SaveChangesAsync();

            return atualizou > 0 ? true : false;

        }

        public async Task<List<User>> GetAll(){
            return await _context.Users.ToListAsync();
        }

    }
}