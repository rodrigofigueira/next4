using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using next4_api.Data;
using next4_api.Interfaces;
using next4_api.Services;
using next4_api.Repository;
using next4_api.Models.DTO.User;

namespace next4_api_tests
{
    [TestClass]
    public class TestUserService
    {

        private IUserService _userService; 

        [TestInitialize]
        public void Setup(){

            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()            
                           .UseSqlServer(connectionString:@"Persist Security Info=False;server=.\SQLEXPRESS2019;database=next4;uid=sa;pwd=sql339023")
                           .Options;
            DataContext dataContext = new DataContext(options);
            this._userService = new UserService(new UserRepository(dataContext));

        }

        [TestCleanup]
        public void TearDown(){
            this._userService.DeleteAllThatNameStartsWith("TestUserService");
        }
       
        [TestMethod]
        public async Task TestUserServicePostReturnIsNotNull(){
            
            UserPost userPost = new UserPost{
                Email = "TestUserServicePostReturnIsNotNull@email.com",
                Name = "TestUserServicePostReturnIsNotNull",
                Password = "1"
            };
            
            var returnFromDB = await _userService.Post(userPost);

            Assert.IsNotNull(returnFromDB);

        }

        [TestMethod]
        public async Task TestUserServiceGetUserById(){

            UserPost userPost = new UserPost{
                Email = "TestUserServiceGetUserById@email.com",
                Name = "TestUserServiceGetUserById",
                Password = "1"
            };

            UserToken userToken = await _userService.Post(userPost);
            UserGet userGet = await _userService.GetById(userToken.Id);
            Assert.IsNotNull(userGet);

        }

        [TestMethod]
        public async Task TestUserServiceDeleteSingleUser(){

            UserPost userPost = new UserPost{
                Email = "TestUserServiceDeleteSingleUser@email.com",
                Name = "TestUserServiceDeleteSingleUser",
                Password = "1"
            };

            UserToken userToken = await _userService.Post(userPost);
            await _userService.Delete(userToken.Id);
            UserGet userGet = await _userService.GetById(userToken.Id);
            Assert.IsNull(userGet);

        }

        [TestMethod]
        public async Task TestUserServiceUpdate(){

            UserPost userPost = new UserPost{
                Email = "TestUserServiceUpdate@email.com",
                Name = "TestUserServiceUpdate",
                Password = "1"
            };

            UserToken userToken = await _userService.Post(userPost);

            string newEmail = "TestUserServiceUpdate@email.com.br";
            string newName = "TestUserServiceUpdate2";

            await _userService.Update(new UserPut{
                Id = userToken.Id,
                Email = newEmail,
                Name = newName                
            });

            UserGet userGet = await _userService.GetById(userToken.Id);

            Assert.IsTrue(userGet.Name == newName && userGet.Email == newEmail);


        }

        [TestMethod]
        public async Task TestUserServiceLoginByName(){

            string _password = "1";
            string _name = "TestUserServiceLoginByName";

            UserPost userPost = new UserPost{
                Email = "TestUserServiceLoginByName@email.com",
                Name = _name,
                Password = _password
            };

            UserToken userToken = await _userService.Post(userPost);

            UserToken loggedUser = await _userService.LoginByName(new UserLoginByName{
                Name = _name,
                Password = _password
            });

            Assert.IsNotNull(loggedUser);


        }

        [TestMethod]
        public async Task TestUserServiceLoginByEmail(){

            string _password = "1";
            string _email = "TestUserServiceLoginByEmail@gmail.com";

            UserPost userPost = new UserPost{
                Email = _email,
                Name = "TestUserServiceLoginByName@email.com",
                Password = _password
            };

            UserToken userToken = await _userService.Post(userPost);

            UserToken loggedUser = await _userService.LoginByEmail(new UserLoginByEmail{
                Email = _email,
                Password = _password
            });

            Assert.IsNotNull(loggedUser);

        }

        [TestMethod]
        public async Task TestUserServiceGetListByUsername(){

            UserPost userPost1 = new UserPost{
                Email = "TestUserServiceGetListByUsername1@email.com",
                Name = "TestUserServiceGetListByUsername1",
                Password = "1"
            };

            await _userService.Post(userPost1);

            UserPost userPost2 = new UserPost{
                Email = "TestUserServiceGetListByUsername2@email.com",
                Name = "TestUserServiceGetListByUsername2",
                Password = "1"
            };

            await _userService.Post(userPost2);

            UserPost userPost3 = new UserPost{
                Email = "TestUserServiceGetListByUsername3@email.com",
                Name = "TestUserServiceGetListByUsername3",
                Password = "1"
            };

            await _userService.Post(userPost3);

            List<UserGet> users = await _userService.GetByName("TestUserServiceGetListByUsername");

            Assert.IsTrue(users.Count() >= 3);

        }

        [TestMethod]
        public async Task TestUserServiceGetListByEmail(){

            UserPost userPost1 = new UserPost{
                Email = "TestUserServiceGetListByEmail1@email.com",
                Name = "TestUserServiceGetListByEmail1",
                Password = "1"
            };

            await _userService.Post(userPost1);

            UserPost userPost2 = new UserPost{
                Email = "TestUserServiceGetListByEmail2@email.com",
                Name = "TestUserServiceGetListByEmail2",
                Password = "1"
            };

            await _userService.Post(userPost2);

            UserPost userPost3 = new UserPost{
                Email = "TestUserServiceGetListByEmail3@email.com",
                Name = "TestUserServiceGetListByEmail3",
                Password = "1"
            };

            await _userService.Post(userPost3);

            List<UserGet> users = await _userService.GetByEmail("TestUserServiceGetListByEmail");

            Assert.IsTrue(users.Count() >= 3);

        }

        [TestMethod]
        public async Task TestUserServiceUpdatePassword(){

            string password = "1";
            string newPassword = "2";

            UserPost userPost = new UserPost{
                Email = "TestUserServiceGetListByEmail1@email.com",
                Name = "TestUserServiceGetListByEmail1",
                Password = password
            };

            UserToken userToken = await _userService.Post(userPost);

            UserPutPassword userPutPassword = new UserPutPassword{
                Id = userToken.Id,
                NewPassword = newPassword,
                OldPassword = password
            };

            await _userService.UpdatePassword(userPutPassword);

            UserToken loggedUser = await _userService.LoginByEmail(new UserLoginByEmail{
                Email = userPost.Email,
                Password = newPassword
            });

            Assert.IsNotNull(loggedUser);

        }

    }
}