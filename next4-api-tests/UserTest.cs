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

        DbContextOptions<DataContext> options = null; 

        [TestInitialize]
        public void Setup(){
            this.options = new DbContextOptionsBuilder<DataContext>()            
                           .UseSqlServer(connectionString:@"Persist Security Info=False;server=.\SQLEXPRESS2019;database=next4;uid=sa;pwd=sql339023")
                           .Options;
        }

        [TestMethod]
        public async Task TestInsertNewUser(){

            DataContext dataContext = new DataContext(options);

            UserRepository userRepository = new UserRepository(dataContext);

            User user = await userRepository.Post(new UserPost{
                Email = "rodrigo@gmail.com",
                Name = "Rodrigo",
                Password = "1"                
            });            

            Assert.IsTrue(user.Id > 0 ? true : false);

        }        

    }
}