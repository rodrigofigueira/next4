using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using next4_api.Data;
using next4_api.Models.DTO.User;
using System.Linq;
using next4_api.Models;
using next4_api.Repository;
using Microsoft.EntityFrameworkCore;
using System;

namespace next4_api_tests
{

    [TestClass]
    public class UserTest
    {
        private UserRepository userRepository; 

        [TestInitialize]
        public void Setup(){
            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()            
                           .UseSqlServer(connectionString:@"Persist Security Info=False;server=.\SQLEXPRESS2019;database=next4;uid=sa;pwd=sql339023")
                           .Options;

            DataContext dataContext = new DataContext(options);

            this.userRepository = new UserRepository(dataContext);
        }

        [TestCleanup]
        public async Task TearDown(){
            var allUsersFromBD = await userRepository.GetListByNameStartsWith("Test");            
            await userRepository.DeleteRange(allUsersFromBD);
        }

        [TestMethod]
        public async Task TestInsertNewUser(){

            User user = await userRepository.Post(new User{
                Email = "TestInsertNewUser@gmail.com",
                Name = "TestInsertNewUser",
                Password = "1"                
            });            

            Assert.IsTrue(user.Id > 0 ? true : false);

        }        

        [TestMethod]
        public async Task TestLoginByEmailAndPassword(){

            string password = "1";

            User user = await userRepository.Post(new User{
                Email = "TestLoginByEmailAndPassword@gmail.com",
                Name = "TestLoginByEmailAndPassword",
                Password = password            
            });                        

            var userLogin = await userRepository.GetByEmailAndPassword(user.Email, password);

            Assert.IsNotNull(userLogin);

        }

        [TestMethod]
        public async Task TestDeletePassingUserObject(){            

            var user = await userRepository.Post(new User{
                Email = "TestDeletePassingUserObject@gmail.com",
                Name = "TestDeletePassingUserObject",
                Password = "1"                
            });

            bool deletou = await userRepository.Delete(user);

            Assert.IsTrue(deletou);           

        }        

        [TestMethod]
        public async Task TestGetById(){

            var user = await userRepository.Post(new User{
                Email = "TestGetById@gmail.com",
                Name = "TestGetById",
                Password = "1"                
            });            

            User userFromBD = await userRepository.GetById(user.Id);

            Assert.IsNotNull(userFromBD);

        }

        [TestMethod]
        public async Task TestNameIsUnique(){

            User user1 = null;

            try{

                user1 = await userRepository.Post(new User{
                                Email = "TestNameIsUnique1@gmail.com",
                                Name = "TestNameIsUnique",
                                Password = "1"                
                            });            

                var user2 = await userRepository.Post(new User{
                                Email = "TestNameIsUnique2@gmail.com",
                                Name = "TestNameIsUnique",
                                Password = "1"                
                            });                            

            }catch(Exception ex){
                if(ex.InnerException != null){
                    Assert.IsTrue(ex.InnerException.Message.Contains("NameIsUnique"));
                }
            }

        }
       
        [TestMethod]
        public async Task TestEmailIsUnique(){

            User user1 = null;

            try{

                user1 = await userRepository.Post(new User{
                                Email = "TestEmailIsUnique@gmail.com",
                                Name = "TestEmailIsUnique1",
                                Password = "1"                
                            });            

                var user2 = await userRepository.Post(new User{
                                Email = "TestEmailIsUnique@gmail.com",
                                Name = "TestEmailIsUnique2",
                                Password = "1"                
                            });                            

            }catch(Exception ex){
                if(ex.InnerException != null){
                    Assert.IsTrue(ex.InnerException.Message.Contains("EmailIsUnique"));
                }
            }

        }

        [TestMethod]
        public async Task TestGetListByNameStartsWith()
        {

            List<User> usersForPost = new List<User>();
                        
            usersForPost.Add(new User{
                Email = "TestGetListByNameStartsWith1@gmail.com",
                Name = "TestGetListByNameStartsWith1",
                Password = "1"
            });

            usersForPost.Add(new User{
                Email = "TestGetListByNameStartsWith2@gmail.com",
                Name = "TestGetListByNameStartsWith2",
                Password = "1"
            });

            usersForPost.Add(new User{
                Email = "TestGetListByNameStartsWith3@gmail.com",
                Name = "TestGetListByNameStartsWith3",
                Password = "1"
            });
            
            foreach (User userPost in usersForPost)
            {
                await userRepository.Post(userPost);
            }
            
            string name = "TestGetListByNameStartsWith";

            List<User> users = await userRepository.GetListByNameStartsWith(name);

            int total = users.Where(u => u.Name.StartsWith(name)).Count();

            Assert.IsTrue(total >= 3);

        }

        [TestMethod]
        public async Task TestGetListByEmailStartsWith()
        {

            List<User> usersForPost = new List<User>();
                        
            usersForPost.Add(new User{
                Email = "TestGetListByEmailStartsWith1@gmail.com",
                Name = "TestGetListByEmailStartsWith1",
                Password = "1"
            });

            usersForPost.Add(new User{
                Email = "TestGetListByEmailStartsWith2@gmail.com",
                Name = "TestGetListByEmailStartsWith2",
                Password = "1"
            });

            usersForPost.Add(new User{
                Email = "TestGetListByEmailStartsWith3@gmail.com",
                Name = "TestGetListByEmailStartsWith3",
                Password = "1"
            });
            
            foreach (User userPost in usersForPost)
            {
                await userRepository.Post(userPost);
            }
            
            string email = "TestGetListByEmailStartsWith";

            List<User> users = await userRepository.GetListByEmailStartsWith(email);

            int total = users.Where(u => u.Email.StartsWith(email)).Count();

            Assert.IsTrue(total >= 3);

        }

        [TestMethod]
        public async Task TestLoginByUsernameAndPassword(){

            string password = "1";

            User user = await userRepository.Post(new User{
                Email = "TestLoginByUsernameAndPassword@gmail.com",
                Name = "TestLoginByUsernameAndPassword",
                Password = password            
            });                        

            var userLogin = await userRepository.GetByUsernameAndPassword(user.Name, password);

            Assert.IsNotNull(userLogin);

        }

        [TestMethod]
        public async Task TestUpdatePassword(){

            string name = "TestUpdatePassword";

            User user = await userRepository.Post(new User{
                Name = name,
                Email = "TestUpdatePassword@email.com",
                Password = "1"
            });            

            await userRepository.UpdatePassword(new User{
                Id = user.Id,
                Password = "2"
            });

            User userToken = await userRepository.GetByUsernameAndPassword(name, "2");
            
            Assert.IsNotNull(userToken);

        }

        [TestMethod]
        public async Task TestUpdate(){

            User user = await userRepository.Post(new User{
                Name = "joão",
                Email = "joão@email.com",
                Password = "1"
            });            

            await userRepository.Update(new User{
                Email = "TestUpdate@email.com",
                Id = user.Id,
                Name = "TestUpdate"
            });

            User retorno = await userRepository.GetById(user.Id);

            bool atualizouNome = retorno.Name == "TestUpdate" ? true : false;
            bool atualizouEmail = retorno.Email == "TestUpdate@email.com" ? true : false;

            Assert.IsTrue(atualizouEmail && atualizouNome);

        }

    }
}