using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using next4_api.Data;
using next4_api.Interfaces;
using next4_api.Services;
using next4_api.Repository;
using next4_api.Models.DTO.User;
using Xunit;
using AutoBogus;

namespace next4_api_tests
{
    public class UserServiceTest  : IAsyncLifetime
    {

        private IUserService _userService; 

        public UserServiceTest(){

            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()            
                           .UseSqlServer(connectionString:@"Persist Security Info=False;server=.\SQLEXPRESS2019;database=next4;uid=sa;pwd=sql339023")
                           .Options;
            DataContext dataContext = new DataContext(options);
            this._userService = new UserService(new UserRepository(dataContext));

        }

        public async Task DisposeAsync()
        {
            await this._userService.DeleteAllThatNameStartsWith("TestUserService");
        }

        public async Task InitializeAsync(){}                

        [Fact]       
        public async Task TestUserServicePostReturnIsNotNull(){

            var autoFaker = new AutoFaker<UserPost>();
            UserPost userPost = autoFaker.Generate();
            var returnFromDB = await _userService.Post(userPost);
            Assert.NotNull(returnFromDB);

        }

        [Fact]
        public async Task TestUserServiceGetUserById()
        {

            var autoFaker = new AutoFaker<UserPost>();
            UserPost userPost = autoFaker.Generate();
            UserToken userToken = await _userService.Post(userPost);
            UserGet userGet = await _userService.GetById(userToken.Id);
            Assert.NotNull(userGet);

        }

        [Fact]
        public async Task TestUserServiceDeleteSingleUser()
        {

            var autoFaker = new AutoFaker<UserPost>();
            UserPost userPost = autoFaker.Generate();
            UserToken userToken = await _userService.Post(userPost);
            await _userService.Delete(userToken.Id);
            UserGet userGet = await _userService.GetById(userToken.Id);
            Assert.Null(userGet);

        }

        [Fact]
        public async Task TestUserServiceUpdate()
        {

            var autoFaker = new AutoFaker<UserPost>();
            UserPost userPost = autoFaker.Generate();
            UserToken userToken = await _userService.Post(userPost);

            string newEmail = "TestUserServiceUpdate@email.com.br";
            string newName = "TestUserServiceUpdate2";

            await _userService.Update(new UserPut
            {
                Id = userToken.Id,
                Email = newEmail,
                Name = newName
            });

            UserGet userGet = await _userService.GetById(userToken.Id);
            Assert.True(userGet.Name == newName && userGet.Email == newEmail);

        }

        [Fact]
        public async Task TestUserServiceLoginByName()
        {

            string _password = "1";

            var autoFaker = new AutoFaker<UserPost>();
            UserPost userPost = autoFaker.Generate();
            userPost.Password = _password;

            await _userService.Post(userPost);

            UserToken loggedUser = await _userService.LoginByName(new UserLoginByName
            {
                Name = userPost.Name,
                Password = _password
            });

            Assert.NotNull(loggedUser);


        }

        [Fact]
        public async Task TestUserServiceLoginByEmail()
        {
            string _password = "1";

            var autoFaker = new AutoFaker<UserPost>();
            UserPost userPost = autoFaker.Generate();
            userPost.Password = _password;

            await _userService.Post(userPost);

            UserToken loggedUser = await _userService.LoginByEmail(new UserLoginByEmail
            {
                Email = userPost.Email,
                Password = _password
            });

            Assert.NotNull(loggedUser);

        }

        [Fact]
        public async Task TestUserServiceGetListByUsername()
        {
            var autoFaker = new AutoFaker<UserPost>();
            UserPost userPost1 = autoFaker.Generate();
            UserPost userPost2 = autoFaker.Generate();
            UserPost userPost3 = autoFaker.Generate();

            userPost1.Name = "teste1";
            userPost2.Name = "teste2";
            userPost3.Name = "teste3";

            await _userService.Post(userPost1);
            await _userService.Post(userPost2);
            await _userService.Post(userPost3);

            List<UserGet> users = await _userService.GetByName("teste");

            Assert.True(users.Count() >= 3);

        }

        [Fact]
        public async Task TestUserServiceGetListByEmail()
        {
            var autoFaker = new AutoFaker<UserPost>();
            UserPost userPost1 = autoFaker.Generate();
            UserPost userPost2 = autoFaker.Generate();
            UserPost userPost3 = autoFaker.Generate();

            userPost1.Email = "teste1@gmail.com";
            userPost2.Email = "teste2@gmail.com";
            userPost3.Email = "teste3@gmail.com";

            await _userService.Post(userPost1);
            await _userService.Post(userPost2);
            await _userService.Post(userPost3);

            List<UserGet> users = await _userService.GetByEmail("teste");

            Assert.True(users.Count() >= 3);

        }

        [Fact]
        public async Task TestUserServiceUpdatePassword()
        {

            string password = "1";
            string newPassword = "2";

            var autoFaker = new AutoFaker<UserPost>();
            UserPost userPost = autoFaker.Generate();
            userPost.Password = password;

            UserToken userToken = await _userService.Post(userPost);

            UserPutPassword userPutPassword = new UserPutPassword
            {
                Id = userToken.Id,
                NewPassword = newPassword,
                OldPassword = password
            };

            await _userService.UpdatePassword(userPutPassword);

            UserToken loggedUser = await _userService.LoginByEmail(new UserLoginByEmail
            {
                Email = userPost.Email,
                Password = newPassword
            });

            Assert.NotNull(loggedUser);

        }

    }
}