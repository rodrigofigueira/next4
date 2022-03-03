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

        [Fact]
        public async Task UserControllerTestLoginByNameAndPasswordNotFound(){

            var autoFaker = new AutoFaker<User>()
                .RuleFor(o => o.Id, f => 0);
            User user = autoFaker.Generate();
            
            string password = user.Password;
            user = await userRepository.Post(user);

            UserLoginByName userLoginByName = new UserLoginByName{
                Name = user.Name,
                Password = "password_wrong"
            };

            var badRequest = await usersController.LoginByName(userLoginByName);
            var item = badRequest.Result as BadRequestObjectResult;

            var result = Assert.IsType<BadRequestObjectResult>(badRequest.Result);                        
            Assert.Equal("Usuário não encontrado", result.Value);

            usersToDelete.Add(new User{Id = user.Id});

        }


        [Fact]        
        public async Task UsersControllerTestLoginByEmailAndPassword(){

            var autoFaker = new AutoFaker<User>()
                .RuleFor(o => o.Id, f => 0);
            User user = autoFaker.Generate();
            
            string password = user.Password;
            user = await userRepository.Post(user);

            UserLoginByEmail userLoginByEmail = new UserLoginByEmail{
                Email = user.Email,
                Password = password
            };

            var okResult = await usersController.LoginByEmail(userLoginByEmail);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);                        
            Assert.IsType<UserToken>(item.Value);

            var userToken = item.Value as UserToken;
            Assert.True(userToken.Id > 0);
            Assert.False(String.IsNullOrEmpty(userToken.Token));

            usersToDelete.Add(new User{Id = user.Id});
        }

        [Fact]
        public async Task UserControllerTestLoginByEmailAndPasswordNotFound(){

            var autoFaker = new AutoFaker<User>()
                .RuleFor(o => o.Id, f => 0);
            User user = autoFaker.Generate();
            
            string password = user.Password;
            user = await userRepository.Post(user);

            UserLoginByEmail userLoginByEmail = new UserLoginByEmail{
                Email = user.Email,
                Password = "password_wrong"
            };

            var badRequest = await usersController.LoginByEmail(userLoginByEmail);
            var item = badRequest.Result as BadRequestObjectResult;

            var result = Assert.IsType<BadRequestObjectResult>(badRequest.Result);                        
            Assert.Equal("Usuário não encontrado", result.Value);

            usersToDelete.Add(new User{Id = user.Id});

        }


        [Fact]
        public async Task UserControllerTestGetByIdFound(){

            var autoFaker = new AutoFaker<User>()
                .RuleFor(o => o.Id, f => 0);
            User user = autoFaker.Generate();
            
            user = await userRepository.Post(user);

            var okResult = await usersController.GetById(user.Id);
            var item = okResult.Result as OkObjectResult;


            Assert.IsType<OkObjectResult>(okResult.Result);                        
            Assert.IsType<UserGet>(item.Value);

            var userGet = item.Value as UserGet;
            Assert.Equal(userGet.Id, user.Id);
            Assert.Equal(userGet.Name, user.Name);
            Assert.Equal(userGet.Email, user.Email);

            usersToDelete.Add(new User{Id = user.Id});

        }

        [Fact]
        public async Task UserControllerTestGetByIdNotFound(){

            var autoFaker = new AutoFaker<User>()
                .RuleFor(o => o.Id, f => 0);
            User user = autoFaker.Generate();
            
            user = await userRepository.Post(user);

            var badRequest = await usersController.GetById(-1);
            var item = badRequest.Result as BadRequestObjectResult;

            var result = Assert.IsType<BadRequestObjectResult>(badRequest.Result);                        
            Assert.Equal("Usuário não encontrado", result.Value);

            usersToDelete.Add(new User{Id = user.Id});

        }

        [Fact]
        public async Task UserControllerTestGetListByNameStartsWithFound(){

            var autoFaker = new AutoFaker<User>()
                .RuleFor(o => o.Id, f => 0);
            User user = autoFaker.Generate();
            
            user = await userRepository.Post(user);

            var okResult = await usersController.GetListByNameStartsWith(user.Name);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);                        
            Assert.IsType<List<UserGet>>(item.Value);

            var users = item.Value as List<UserGet>;
            Assert.Equal(users.Count, 1);

            usersToDelete.Add(new User{Id = user.Id});

        }

        [Fact]
        public async Task UserControllerTestGetListByNameStartsWithNotFound(){

            var autoFaker = new AutoFaker<User>()
                .RuleFor(o => o.Id, f => 0);
            User user = autoFaker.Generate();
            
            user = await userRepository.Post(user);

            var badRequest = await usersController.GetListByNameStartsWith("9999");
            var item = badRequest.Result as BadRequestObjectResult;

            var result = Assert.IsType<BadRequestObjectResult>(badRequest.Result);                        
            Assert.Equal("Nenhum usuário encontrado", result.Value);

            usersToDelete.Add(new User{Id = user.Id});

        }

        [Fact]
        public async Task UserControllerTestGetListByEmailStartsWithFound(){

            var autoFaker = new AutoFaker<User>()
                .RuleFor(o => o.Id, f => 0);
            User user = autoFaker.Generate();
            
            user = await userRepository.Post(user);

            var okResult = await usersController.GetListByEmailStartsWith(user.Email);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);                        
            Assert.IsType<List<UserGet>>(item.Value);

            var users = item.Value as List<UserGet>;
            Assert.Equal(users.Count, 1);

            usersToDelete.Add(new User{Id = user.Id});

        }

        [Fact]
        public async Task UserControllerTestGetListByEmailStartsWithNotFound(){

            var autoFaker = new AutoFaker<User>()
                .RuleFor(o => o.Id, f => 0);
            User user = autoFaker.Generate();
            
            user = await userRepository.Post(user);

            var badRequest = await usersController.GetListByEmailStartsWith("9999");
            var item = badRequest.Result as BadRequestObjectResult;

            var result = Assert.IsType<BadRequestObjectResult>(badRequest.Result);                        
            Assert.Equal("Nenhum usuário encontrado", result.Value);

            usersToDelete.Add(new User{Id = user.Id});

        }

        [Fact]
        public async Task UserControllerTestUpdatePasswordOk(){

            var autoFaker = new AutoFaker<User>()
                .RuleFor(o => o.Id, f => 0);
            User user = autoFaker.Generate();
            string oldPassword = user.Password;            

            user = await userRepository.Post(user);

            UserPutPassword userPutPassword = new UserPutPassword{
                Id = user.Id,
                NewPassword = "2",
                OldPassword = oldPassword
            };

            var okResult = await usersController.UpdatePassword(userPutPassword);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);                        
            Assert.IsType<string>(item.Value);

            var result = item.Value as string;
            Assert.Equal(result, "Senha atualizada com sucesso.");

            usersToDelete.Add(new User{Id = user.Id});

        }

       [Fact]
        public async Task UserControllerTestUpdatePasswordError(){

            var autoFaker = new AutoFaker<UserPutPassword>();
            UserPutPassword user = autoFaker.Generate();

            var badRequestResult = await usersController.UpdatePassword(user);
            var item = badRequestResult.Result as BadRequestObjectResult;

            Assert.IsType<BadRequestObjectResult>(badRequestResult.Result);                        
            Assert.IsType<string>(item.Value);

            var result = item.Value as string;
            Assert.Equal(result, "Não foi possível atualizar a senha.");

        }




    }
}