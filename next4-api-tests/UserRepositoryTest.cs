using System.Collections.Generic;
using System.Threading.Tasks;
using next4_api.Data;
using next4_api.Models.DTO.User;
using System.Linq;
using next4_api.Models;
using next4_api.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using next4_api.Interfaces;
using Xunit;
using AutoBogus;

namespace next4_api_tests
{

    public class UserRepositoryTest : IAsyncLifetime
    {
        private IUserRepository userRepository;

        public UserRepositoryTest()
        {
            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()
                           .UseSqlServer(connectionString: @"Persist Security Info=False;server=.\SQLEXPRESS2019;database=next4;uid=sa;pwd=sql339023")
                           .Options;

            DataContext dataContext = new DataContext(options);

            this.userRepository = new UserRepository(dataContext);
        }

        public async Task DisposeAsync()
        {
            userRepository.Clear();
            var allUsersFromBD = await userRepository.GetAll();
            await userRepository.DeleteRange(allUsersFromBD);

        }

        public async Task InitializeAsync()
        { }

        [Fact]
        public async Task TestInsertNewUser()
        {

            var autoFaker = new AutoFaker<User>()
                .RuleFor(o => o.Id, f => 0);
            User userPost = autoFaker.Generate();
            User user = await userRepository.Post(userPost);
            Assert.True(user.Id > 0 ? true : false);
        }

        [Fact]
        public async Task TestLoginByEmailAndPassword()
        {

            string password = "1";

            var autoFaker = new AutoFaker<User>()
                .RuleFor(o => o.Id, f => 0)
                .RuleFor(o => o.Password, f => password);
            User userPost = autoFaker.Generate();
            User user = await userRepository.Post(userPost);
            var userLogin = await userRepository.GetByEmailAndPassword(user.Email, password);
            Assert.NotNull(userLogin);

        }

        [Fact]
        public async Task TestDeletePassingUserObject()
        {
            var autoFaker = new AutoFaker<User>()
                            .RuleFor(o => o.Id, f => 0);
            User userPost = autoFaker.Generate();
            User userInserted = await userRepository.Post(userPost);
            bool deletou = await userRepository.Delete(userPost);
            Assert.True(deletou);
        }

        [Fact]
        public async Task TestGetById()
        {

            var autoFaker = new AutoFaker<User>()
                            .RuleFor(o => o.Id, f => 0);
            User userPost = autoFaker.Generate();
            User userInserted = await userRepository.Post(userPost);
            User userFromBD = await userRepository.GetById(userInserted.Id);

            Assert.NotNull(userFromBD);

        }

        [Fact]
        public async Task TestNameIsUnique()
        {

            try
            {
                string nameToTest = "abcdef";

                var autoFaker = new AutoFaker<User>()
                         .RuleFor(o => o.Id, f => 0)
                         .RuleFor(o => o.Name, f => nameToTest);

                User user1 = autoFaker.Generate();
                User userInserted = await userRepository.Post(user1);
                User user2 = autoFaker.Generate();
                await userRepository.Post(user2);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Assert.True(ex.InnerException.Message.Contains("NameIsUnique"));
                }
            }

        }

        [Fact]
        public async Task TestEmailIsUnique()
        {
            try
            {

                string emailToTest = "abcdef@gmail.com";

                var autoFaker = new AutoFaker<User>()
                         .RuleFor(o => o.Id, f => 0)
                         .RuleFor(o => o.Email, f => emailToTest);

                User user1 = autoFaker.Generate();
                User userInserted = await userRepository.Post(user1);
                User user2 = autoFaker.Generate();
                await userRepository.Post(user2);

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Assert.True(ex.InnerException.Message.Contains("EmailIsUnique"));
                }
            }

        }

        [Fact]
        public async Task TestGetListByNameStartsWith()
        {

            List<User> usersForPost = new List<User>();

            var autoFaker = new AutoFaker<User>()
                     .RuleFor(o => o.Id, f => 0);

            User user1 = autoFaker.Generate();
            User user2 = autoFaker.Generate();
            User user3 = autoFaker.Generate();

            user1.Name = "teste1";
            user2.Name = "teste2";
            user3.Name = "teste3";

            await userRepository.Post(user1);
            await userRepository.Post(user2);
            await userRepository.Post(user3);

            string name = "teste";

            List<User> users = await userRepository.GetListByNameStartsWith(name);

            int total = users.Where(u => u.Name.StartsWith(name)).Count();

            Assert.True(total >= 3);

        }

        [Fact]
        public async Task TestGetListByEmailStartsWith()
        {

            List<User> usersForPost = new List<User>();

            var autoFaker = new AutoFaker<User>()
                     .RuleFor(o => o.Id, f => 0);

            User user1 = autoFaker.Generate();
            User user2 = autoFaker.Generate();
            User user3 = autoFaker.Generate();

            user1.Email = "teste1@gmail.com";
            user2.Email = "teste2@gmail.com";
            user3.Email = "teste3@gmail.com";

            await userRepository.Post(user1);
            await userRepository.Post(user2);
            await userRepository.Post(user3);

            string email = "teste";

            List<User> users = await userRepository.GetListByEmailStartsWith(email);

            int total = users.Where(u => u.Email.StartsWith(email)).Count();

            Assert.True(total >= 3);

        }

        [Fact]
        public async Task TestLoginByUsernameAndPassword()
        {

            string password = "1";

            var autoFaker = new AutoFaker<User>()
                     .RuleFor(o => o.Id, f => 0)
                     .RuleFor(o => o.Password, f => password);

            User user = autoFaker.Generate();
            await userRepository.Post(user);

            var userLogin = await userRepository.GetByUsernameAndPassword(user.Name, password);

            Assert.NotNull(userLogin);

        }

        [Fact]
        public async Task TestUpdatePassword()
        {

            string name = "TestUpdatePassword";

            var autoFaker = new AutoFaker<User>()
                            .RuleFor(o => o.Id, f => 0)
                            .RuleFor(o => o.Name, f => name);

            User user = autoFaker.Generate();
            user = await userRepository.Post(user);

            await userRepository.UpdatePassword(new User
            {
                Id = user.Id,
                Password = "2"
            });

            User userToken = await userRepository.GetByUsernameAndPassword(name, "2");

            Assert.NotNull(userToken);

        }

        [Fact]
        public async Task TestUpdate()
        {
            string name = "TestUpdate";
            string email = "TestUpdate@email.com";

            var autoFaker = new AutoFaker<User>()
                            .RuleFor(o => o.Id, f => 0);

            User user = autoFaker.Generate();
            user = await userRepository.Post(user);

            User userToUpdate = autoFaker.Generate();
            userToUpdate.Id = user.Id;
            userToUpdate.Name = name;
            userToUpdate.Email = email;

            await userRepository.Update(userToUpdate);

            User retorno = await userRepository.GetById(user.Id);

            bool atualizouNome = retorno.Name == name ? true : false;
            bool atualizouEmail = retorno.Email == email ? true : false;

            Assert.True(atualizouEmail && atualizouNome);

        }

    }
}