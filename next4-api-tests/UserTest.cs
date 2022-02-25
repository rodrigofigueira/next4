using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using next4_api.Data;
using next4_api.Models.DTO.User;
using System.Linq;
using next4_api.Models;
using next4_api.Repository;
using Microsoft.EntityFrameworkCore;

namespace next4_api_tests
{

    [TestClass]
    public class UserTest
    {
        UserRepository userRepository = null; 

        [TestInitialize]
        public void Setup(){
            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()            
                           .UseSqlServer(connectionString:@"Persist Security Info=False;server=.\SQLEXPRESS2019;database=next4;uid=sa;pwd=sql339023")
                           .Options;

            DataContext dataContext = new DataContext(options);

            this.userRepository = new UserRepository(dataContext);
        }

        [TestMethod]
        public async Task TestInsertNewUser(){

            User user = await userRepository.Post(new UserPost{
                Email = "rodrigo@gmail.com",
                Name = "Rodrigo",
                Password = "1"                
            });            

            await userRepository.Delete(user);

            Assert.IsTrue(user.Id > 0 ? true : false);

        }        

        [TestMethod]
        public async Task TestLoginByEmailAndPassword(){

            string password = "1";

            User user = await userRepository.Post(new UserPost{
                Email = "TestLoginByEmailAndPassword@gmail.com",
                Name = "TestLoginByEmailAndPassword",
                Password = password            
            });                        

            var userLogin = await userRepository.GetByEmailAndPassword(user.Email, password);

            await userRepository.Delete(user);

            Assert.IsNotNull(userLogin);

        }

        [TestMethod]
        public async Task TestDeletePassingUserObject(){            

            var user = await userRepository.Post(new UserPost{
                Email = "TestDeletePassingUserObject@gmail.com",
                Name = "TestDeletePassingUserObject",
                Password = "1"                
            });

            bool deletou = await userRepository.Delete(user);

            Assert.IsTrue(deletou);           

        }        

        [TestMethod]
        public async Task TestGetById(){

            var user = await userRepository.Post(new UserPost{
                Email = "TestGetById@gmail.com",
                Name = "TestGetById",
                Password = "1"                
            });            

            var userFromBD = await userRepository.GetById(user.Id);

            await userRepository.Delete(user);
            
            Assert.IsNotNull(userFromBD);

        }

    }
}