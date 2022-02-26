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

        [TestMethod]
        public async Task TestNameIsUnique(){

            User user1 = null;

            try{

                user1 = await userRepository.Post(new UserPost{
                                Email = "TestNameIsUnique1@gmail.com",
                                Name = "TestNameIsUnique",
                                Password = "1"                
                            });            

                var user2 = await userRepository.Post(new UserPost{
                                Email = "TestNameIsUnique2@gmail.com",
                                Name = "TestNameIsUnique",
                                Password = "1"                
                            });                            

            }catch(Exception ex){
                if(ex.InnerException != null){
                    Assert.IsTrue(ex.InnerException.Message.Contains("NameIsUnique"));
                }
            }
            finally{
                await userRepository.Delete(user1);
            }


        }
       
        [TestMethod]
        public async Task TestEmailIsUnique(){

            User user1 = null;

            try{

                user1 = await userRepository.Post(new UserPost{
                                Email = "TestEmailIsUnique@gmail.com",
                                Name = "TestEmailIsUnique1",
                                Password = "1"                
                            });            

                var user2 = await userRepository.Post(new UserPost{
                                Email = "TestEmailIsUnique@gmail.com",
                                Name = "TestEmailIsUnique2",
                                Password = "1"                
                            });                            

            }catch(Exception ex){
                if(ex.InnerException != null){
                    Assert.IsTrue(ex.InnerException.Message.Contains("EmailIsUnique"));
                }
            }
            finally{
                await userRepository.Delete(user1);
            }

        }

        [TestMethod]
        public async Task TestGetListByNameStartsWith()
        {

            List<UserPost> usersForPost = new List<UserPost>();
                        
            usersForPost.Add(new UserPost{
                Email = "usersForPost1@gmail.com",
                Name = "usersForPost1",
                Password = "1"
            });

            usersForPost.Add(new UserPost{
                Email = "usersForPost2@gmail.com",
                Name = "usersForPost2",
                Password = "1"
            });

            usersForPost.Add(new UserPost{
                Email = "usersForPost3@gmail.com",
                Name = "usersForPost3",
                Password = "1"
            });
            
            foreach (UserPost userPost in usersForPost)
            {
                await userRepository.Post(userPost);
            }
            
            string name = "usersForPost";

            List<UserGet> users = await userRepository.GetListByNameStartsWith(name);

            int total = users.Where(u => u.Name.StartsWith(name)).Count();

            userRepository.Clear();

            List<User> usersToRemove = new List<User>();
            
            foreach (UserGet userGet in users)
            {
                usersToRemove.Add(new User{Id = userGet.Id});
            }            

            await userRepository.DeleteRange(usersToRemove);

            Assert.IsTrue(total >= 3);

        }

        [TestMethod]
        public async Task TestGetListByEmailStartsWith()
        {

            List<UserPost> usersForPost = new List<UserPost>();
                        
            usersForPost.Add(new UserPost{
                Email = "TestGetListByEmailStartsWith1@gmail.com",
                Name = "TestGetListByEmailStartsWith1",
                Password = "1"
            });

            usersForPost.Add(new UserPost{
                Email = "TestGetListByEmailStartsWith2@gmail.com",
                Name = "TestGetListByEmailStartsWith2",
                Password = "1"
            });

            usersForPost.Add(new UserPost{
                Email = "TestGetListByEmailStartsWith3@gmail.com",
                Name = "TestGetListByEmailStartsWith3",
                Password = "1"
            });
            
            foreach (UserPost userPost in usersForPost)
            {
                await userRepository.Post(userPost);
            }
            
            string email = "TestGetListByEmailStartsWith";

            List<UserGet> users = await userRepository.GetListByEmailStartsWith(email);

            int total = users.Where(u => u.Email.StartsWith(email)).Count();

            userRepository.Clear();

            List<User> usersToRemove = new List<User>();
            
            foreach (UserGet userGet in users)
            {
                usersToRemove.Add(new User{Id = userGet.Id});
            }            

            await userRepository.DeleteRange(usersToRemove);

            Assert.IsTrue(total >= 3);

        }

        [TestMethod]
        public async Task TestLoginByUsernameAndPassword(){

            string password = "1";

            User user = await userRepository.Post(new UserPost{
                Email = "TestLoginByUsernameAndPassword@gmail.com",
                Name = "TestLoginByUsernameAndPassword",
                Password = password            
            });                        

            var userLogin = await userRepository.GetByUsernameAndPassword(user.Name, password);

            await userRepository.Delete(user);

            Assert.IsNotNull(userLogin);


        }

        [TestMethod]
        public async Task TestUpdatePassword(){

            string name = "TestUpdatePassword";

            User user = await userRepository.Post(new UserPost{
                Name = name,
                Email = "TestUpdatePassword@email.com",
                Password = "1"
            });            

            await userRepository.UpdatePassword(new UserPutPassword{
                Id = user.Id,
                NewPassword = "2"
            });

            UserToken userToken = await userRepository.GetByUsernameAndPassword(name, "2");
            
            userRepository.Clear();

            await userRepository.Delete(new User{
                Id = user.Id
            });

            Assert.IsNotNull(userToken);

        }

        [TestMethod]
        public async Task TestUpdate(){

            User user = await userRepository.Post(new UserPost{
                Name = "joão",
                Email = "joão@email.com",
                Password = "1"
            });            

            await userRepository.Update(new UserPut{
                Email = "TestUpdate@email.com",
                Id = user.Id,
                Name = "TestUpdate"
            });

            UserGet retorno = await userRepository.GetById(user.Id);

            bool atualizouNome = retorno.Name == "TestUpdate" ? true : false;
            bool atualizouEmail = retorno.Email == "TestUpdate@email.com" ? true : false;

            userRepository.Clear();

            await userRepository.Delete(new User{ Id = retorno.Id});

            Assert.IsTrue(atualizouEmail && atualizouNome);

        }


    }
}