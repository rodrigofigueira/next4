using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoBogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using next4_api.Controllers;
using next4_api.Data;
using next4_api.Interfaces;
using next4_api.Models;
using next4_api.Models.DTO.User;
using next4_api.Repository;
using Xunit;

namespace next4_api_tests
{
    public class UserControllerTest : IAsyncLifetime
    {

        List<User> usersToDelete;
        IUserRepository userRepository;
        UsersController usersController;

        public UserControllerTest(){

            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()            
                           .UseSqlServer(connectionString:@"Persist Security Info=False;server=.\SQLEXPRESS2019;database=next4;uid=sa;pwd=sql339023")
                           .Options;

            DataContext dataContext = new DataContext(options);

            this.userRepository = new UserRepository(dataContext);
            this.usersToDelete = new List<User>();
            this.usersController = new UsersController(userRepository);
        }

        public async Task DisposeAsync()
        {
            userRepository.Clear();
            await userRepository.DeleteRange(usersToDelete);
        }

        public async Task InitializeAsync(){}        

        [Fact]        
        public async Task UsersControllerTestPostReturnok(){
            var autoFaker = new AutoFaker<UserPost>();
            UserPost userPost = autoFaker.Generate();

            var okResult = await new UsersController(userRepository).Post(userPost);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);                        
            Assert.IsType<UserToken>(item.Value);

            var user = item.Value as UserToken;
            Assert.Equal(user.Name, userPost.Name);
            Assert.True(user.Id > 0);
            Assert.False(String.IsNullOrEmpty(user.Token));

            usersToDelete.Add(new User{Id = user.Id});
        }

        [Fact]        
        public async Task UsersControllerTestLoginByNameAndPassword(){

            var autoFaker = new AutoFaker<User>()
                .RuleFor(o => o.Id, f => 0);
            User user = autoFaker.Generate();
            
            string password = user.Password;
            user = await userRepository.Post(user);

            UserLoginByName userLoginByName = new UserLoginByName{
                Name = user.Name,
                Password = password
            };

            var okResult = await usersController.LoginByName(userLoginByName);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);                        
            Assert.IsType<UserToken>(item.Value);

            var userToken = item.Value as UserToken;
            Assert.True(userToken.Id > 0);
            Assert.False(String.IsNullOrEmpty(userToken.Token));

            usersToDelete.Add(new User{Id = user.Id});
        }

    }
}