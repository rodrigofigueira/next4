using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoBogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Controllers;
using Api.Data;
using Api.Interfaces;
using Api.Models;
using Api.Models.DTO.User;
using Api.Repository;
using Api.Services;
using Xunit;

namespace Teste
{
    public class UserControllerTest : IAsyncLifetime
    {

        List<User> usersToDelete;
        IUserRepository userRepository;
        UsersController usersController;
        IUserService userService;

        public UserControllerTest()
        {

            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()
                           .UseSqlServer(connectionString: @"Persist Security Info=False;server=.\SQLEXPRESS2019;database=next4;uid=sa;pwd=sql339023")
                           .Options;

            DataContext dataContext = new DataContext(options);

            this.userRepository = new UserRepository(dataContext);
            this.usersToDelete = new List<User>();
            this.userService = new UserService(userRepository);
            this.usersController = new UsersController(userRepository, userService);

        }

        public async Task DisposeAsync()
        {
            if (usersToDelete.Count > 0)
            {
                userRepository.Clear();
                await userRepository.DeleteRange(usersToDelete);
            }
        }

        public async Task InitializeAsync() { }

        [Fact]
        public async Task UsersControllerTestPostReturnok()
        {
            var autoFaker = new AutoFaker<UserPost>();
            UserPost userPost = autoFaker.Generate();

            var okResult = await usersController.Post(userPost);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.IsType<UserToken>(item.Value);

            var user = item.Value as UserToken;
            Assert.Equal(user.Name, userPost.Name);
            Assert.True(user.Id > 0);
            Assert.False(String.IsNullOrEmpty(user.Token));

            usersToDelete.Add(new User { Id = user.Id });
        }

        [Fact]
        public async Task UsersControllerTestLoginByNameAndPassword()
        {

            var autoFaker = new AutoFaker<UserPost>();
            UserPost user = autoFaker.Generate();

            string password = user.Password;
            await userService.Post(user);

            UserLoginByName userLoginByName = new UserLoginByName
            {
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

            usersToDelete.Add(new User { Id = userToken.Id });
        }

        [Fact]
        public async Task UserControllerTestLoginByNameAndPasswordNotFound()
        {

            var autoFaker = new AutoFaker<UserPost>();
            UserPost user = autoFaker.Generate();

            UserToken userToken = await userService.Post(user);

            UserLoginByName userLoginByName = new UserLoginByName
            {
                Name = user.Name,
                Password = "password_wrong"
            };

            var badRequest = await usersController.LoginByName(userLoginByName);
            var item = badRequest.Result as BadRequestObjectResult;

            var result = Assert.IsType<BadRequestObjectResult>(badRequest.Result);
            Assert.Equal("Usuário não encontrado", result.Value);

            usersToDelete.Add(new User { Id = userToken.Id });

        }


        [Fact]
        public async Task UsersControllerTestLoginByEmailAndPassword()
        {

            var autoFaker = new AutoFaker<UserPost>();
            UserPost userPost = autoFaker.Generate();

            string password = userPost.Password;
            UserToken userToken = await userService.Post(userPost);

            UserLoginByEmail userLoginByEmail = new UserLoginByEmail
            {
                Email = userPost.Email,
                Password = password
            };

            var okResult = await usersController.LoginByEmail(userLoginByEmail);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.IsType<UserToken>(item.Value);

            var userTokenFromController = item.Value as UserToken;
            Assert.True(userTokenFromController.Id > 0);
            Assert.False(String.IsNullOrEmpty(userTokenFromController.Token));

            usersToDelete.Add(new User { Id = userTokenFromController.Id });
        }

        [Fact]
        public async Task UserControllerTestLoginByEmailAndPasswordNotFound()
        {

            var autoFaker = new AutoFaker<UserPost>();
            UserPost userPost = autoFaker.Generate();

            string password = userPost.Password;
            UserToken userToken = await userService.Post(userPost);

            UserLoginByEmail userLoginByEmail = new UserLoginByEmail
            {
                Email = userPost.Email,
                Password = "password_wrong"
            };

            var badRequest = await usersController.LoginByEmail(userLoginByEmail);
            var item = badRequest.Result as BadRequestObjectResult;

            var result = Assert.IsType<BadRequestObjectResult>(badRequest.Result);
            Assert.Equal("Usuário não encontrado", result.Value);

            usersToDelete.Add(new User { Id = userToken.Id });

        }

        [Fact]
        public async Task UserControllerTestGetByIdFound()
        {

            var autoFaker = new AutoFaker<UserPost>();
            UserPost userPost = autoFaker.Generate();

            UserToken userToken = await userService.Post(userPost);

            var okResult = await usersController.GetById(userToken.Id);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.IsType<UserGet>(item.Value);

            var userGet = item.Value as UserGet;
            Assert.Equal(userGet.Id, userToken.Id);
            Assert.Equal(userGet.Name, userPost.Name);
            Assert.Equal(userGet.Email, userPost.Email);

            usersToDelete.Add(new User { Id = userToken.Id });

        }

        [Fact]
        public async Task UserControllerTestGetByIdNotFound()
        {

            var autoFaker = new AutoFaker<UserPost>();
            UserPost userPost = autoFaker.Generate();

            UserToken userToken = await userService.Post(userPost);

            var badRequest = await usersController.GetById(-1);
            var item = badRequest.Result as BadRequestObjectResult;

            var result = Assert.IsType<BadRequestObjectResult>(badRequest.Result);
            Assert.Equal("Usuário não encontrado", result.Value);

            usersToDelete.Add(new User { Id = userToken.Id });

        }

        [Fact]
        public async Task UserControllerTestGetListByNameStartsWithFound()
        {

            var autoFaker = new AutoFaker<UserPost>();
            UserPost userPost = autoFaker.Generate();

            UserToken userToken = await userService.Post(userPost);

            var okResult = await usersController.GetListByNameStartsWith(userPost.Name);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.IsType<List<UserGet>>(item.Value);

            var users = item.Value as List<UserGet>;
            Assert.Single<UserGet>(users);

            usersToDelete.Add(new User { Id = userToken.Id });

        }

        [Fact]
        public async Task UserControllerTestGetListByNameStartsWithNotFound()
        {

            var autoFaker = new AutoFaker<UserPost>();
            UserPost userPost = autoFaker.Generate();

            UserToken userToken = await userService.Post(userPost);

            var badRequest = await usersController.GetListByNameStartsWith("9999");
            var item = badRequest.Result as BadRequestObjectResult;

            var result = Assert.IsType<BadRequestObjectResult>(badRequest.Result);
            Assert.Equal("Nenhum usuário encontrado", result.Value);

            usersToDelete.Add(new User { Id = userToken.Id });

        }

        [Fact]
        public async Task UserControllerTestGetListByEmailStartsWithFound()
        {

            var autoFaker = new AutoFaker<UserPost>();
            UserPost userPost = autoFaker.Generate();

            UserToken userToken = await userService.Post(userPost);

            var okResult = await usersController.GetListByEmailStartsWith(userPost.Email);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.IsType<List<UserGet>>(item.Value);

            var users = item.Value as List<UserGet>;
            Assert.Single<UserGet>(users);

            usersToDelete.Add(new User { Id = userToken.Id });

        }

        [Fact]
        public async Task UserControllerTestGetListByEmailStartsWithNotFound()
        {

            var autoFaker = new AutoFaker<UserPost>();
            UserPost userPost = autoFaker.Generate();

            UserToken userToken = await userService.Post(userPost);

            var badRequest = await usersController.GetListByEmailStartsWith("9999");
            var item = badRequest.Result as BadRequestObjectResult;

            var result = Assert.IsType<BadRequestObjectResult>(badRequest.Result);
            Assert.Equal("Nenhum usuário encontrado", result.Value);

            usersToDelete.Add(new User { Id = userToken.Id });

        }

        [Fact]
        public async Task UserControllerTestUpdatePasswordOk()
        {

            var autoFaker = new AutoFaker<UserPost>();
            UserPost userPost = autoFaker.Generate();
            string oldPassword = userPost.Password;

            UserToken userToken = await userService.Post(userPost);

            UserPutPassword userPutPassword = new UserPutPassword
            {
                Id = userToken.Id,
                NewPassword = "2",
                OldPassword = oldPassword
            };

            var okResult = await usersController.UpdatePassword(userPutPassword);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.IsType<string>(item.Value);

            var result = item.Value as string;
            Assert.Equal("Senha atualizada com sucesso.", result);

            usersToDelete.Add(new User { Id = userToken.Id });

        }


        [Fact]
        public async Task UserControllerTestUpdatePasswordError()
        {

            var autoFaker = new AutoFaker<UserPutPassword>();
            UserPutPassword user = autoFaker.Generate();

            var badRequestResult = await usersController.UpdatePassword(user);
            var item = badRequestResult.Result as BadRequestObjectResult;

            Assert.IsType<BadRequestObjectResult>(badRequestResult.Result);
            Assert.IsType<string>(item.Value);

            var result = item.Value as string;
            Assert.Equal("Não foi possível atualizar a senha.", result);

        }

        [Fact]
        public async Task UserControllerTestDeleteOk()
        {

            var autoFaker = new AutoFaker<UserPost>();
            UserPost userPost = autoFaker.Generate();

            UserToken userToken = await userService.Post(userPost);

            userRepository.Clear();

            var okResult = await usersController.Delete(userToken.Id);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.IsType<string>(item.Value);

            var result = item.Value as string;
            Assert.Equal("Usuário deletado.", result);

        }

        [Fact]
        public async Task UserControllerTestDeleteFail()
        {

            var autoFaker = new AutoFaker<User>();
            User user = autoFaker.Generate();

            var badRequestResult = await usersController.Delete(user.Id);
            var item = badRequestResult.Result as BadRequestObjectResult;

            Assert.IsType<BadRequestObjectResult>(badRequestResult.Result);
            Assert.IsType<string>(item.Value);

            var result = item.Value as string;
            Assert.Equal("Não foi possível realizar a deleção", result);

        }


        [Fact]
        public async Task UserControllerUpdateOk()
        {

            var autoFaker = new AutoFaker<UserPost>()
                .RuleFor(o => o.Email, f => f.Internet.Email());

            var autoFakerUpdate = new AutoFaker<UserPut>()
                .RuleFor(o => o.Id, f => 0)
                .RuleFor(o => o.Email, f => f.Internet.Email());

            UserPost userPost = autoFaker.Generate();

            UserToken userInserted = await userService.Post(userPost);
            UserPut userToUpdate = autoFakerUpdate.Generate();

            userToUpdate.Id = userInserted.Id;

            var okResult = await usersController.Update(userToUpdate);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.IsType<string>(item.Value);

            var result = item.Value as string;
            Assert.Equal("Usuário atualizado com sucesso.", result);

            usersToDelete.Add(new User { Id = userInserted.Id });

        }

        [Fact]
        public async Task UserControllerUpdateFail()
        {

            var autoFakerUpdate = new AutoFaker<UserPut>()
                .RuleFor(o => o.Id, f => -999)
                .RuleFor(o => o.Email, f => f.Internet.Email());

            UserPut userToUpdate = autoFakerUpdate.Generate();

            var badRequestResult = await usersController.Update(userToUpdate);
            var item = badRequestResult.Result as BadRequestObjectResult;

            Assert.IsType<BadRequestObjectResult>(badRequestResult.Result);
            Assert.IsType<string>(item.Value);

            var result = item.Value as string;
            Assert.Equal("Não foi possível realizar a atualização", result);

        }

        [Fact]
        public async Task UserControllerUpdateFailBecauseNameExists()
        {

            var autoFaker = new AutoFaker<UserPost>()
                .RuleFor(o => o.Email, f => f.Internet.Email());

            UserPost firstUserToInsert = autoFaker.Generate();
            UserPost secondUserToInsert = autoFaker.Generate();

            var firstUserInserted = await userService.Post(firstUserToInsert);
            var secondUserInserted = await userService.Post(secondUserToInsert);

            UserPut userToUpdate = new UserPut
            {
                Id = firstUserInserted.Id,
                Email = firstUserToInsert.Email,
                Name = secondUserInserted.Name
            };

            var badRequestResult = await usersController.Update(userToUpdate);
            var item = badRequestResult.Result as BadRequestObjectResult;

            Assert.IsType<BadRequestObjectResult>(badRequestResult.Result);
            Assert.IsType<string>(item.Value);

            var result = item.Value as string;
            Assert.Equal("Nome já existe", result);

            usersToDelete.Add(new User { Id = firstUserInserted.Id });
            usersToDelete.Add(new User { Id = secondUserInserted.Id });

        }

        [Fact]
        public async Task UserControllerUpdateFailBecauseEmailExists()
        {

            var autoFaker = new AutoFaker<UserPost>()
                .RuleFor(o => o.Email, f => f.Internet.Email());

            UserPost firstUserToInsert = autoFaker.Generate();
            UserPost secondUserToInsert = autoFaker.Generate();

            var firstUserInserted = await userService.Post(firstUserToInsert);
            var secondUserInserted = await userService.Post(secondUserToInsert);

            UserPut userToUpdate = new UserPut
            {
                Id = firstUserInserted.Id,
                Email = secondUserToInsert.Email,
                Name = firstUserInserted.Name
            };

            ActionResult<string> badRequestResult = null;
            BadRequestObjectResult item = null;
            try
            {
                badRequestResult = await usersController.Update(userToUpdate);
                item = badRequestResult.Result as BadRequestObjectResult;
            }
            catch
            {
                Assert.IsType<BadRequestObjectResult>(badRequestResult.Result);
                Assert.IsType<string>(item.Value);

                var result = item.Value as string;
                Assert.Equal("Email já existe", result);
            }
            usersToDelete.Add(new User { Id = firstUserInserted.Id });
            usersToDelete.Add(new User { Id = secondUserInserted.Id });

        }


    }
}