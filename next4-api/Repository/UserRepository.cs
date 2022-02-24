using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using next4_api.Data;
using next4_api.Models;
using next4_api.Models.DTO.User;

namespace next4_api.Repository
{
    public class UserRepository
    {
        private DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Post(UserPost userPost){

            DateTime now = DateTime.Now;

            User user = new User{
                Name = userPost.Name,
                Email = userPost.Email,
                Password = userPost.Password,
                CreatedAt = now,
                UpdatedAt = now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            return user;

        }

    }
}