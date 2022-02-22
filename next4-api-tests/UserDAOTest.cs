using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using next4_api.Data;
using next4_api.Models.DTO.User;
using System.Linq;
using next4_api.Models;

namespace next4_api_tests;

[TestClass]
public class UserDAOTest
{
    [TestMethod]
    public async Task EmailExists()
    {
        UserDAO userDAO = new UserDAO();
        string email = "testeemailpost@gmail.com";
        User user = await userDAO.Post(new UserPost{
            Email = email,
            Name = "tantofaz",
            Password = "1"            
        });        
        bool emailExists = await userDAO.EmailExists(email);
        await userDAO.Delete(user.Id);
        Assert.IsTrue(emailExists, "Email existe");
    }

    [TestMethod]
    public async Task NameExists()
    {
        UserDAO userDAO = new UserDAO();
        string name = "testnameexist";
        User user = await userDAO.Post(new UserPost{
            Email = "testnameexist@gmail.com",
            Name = name,
            Password = "1"            
        });        
        bool nameExists = await userDAO.NameExists(name);
        await userDAO.Delete(user.Id);
        Assert.IsTrue(nameExists, "Name existe");
    }

    [TestMethod]
    public async Task GetListByEmailStartsWith(){

    UserDAO userDAO = new UserDAO();

    User user1 = await userDAO.Post(new UserPost{
        Email = "testegetlistbyemail@yahoo.com",
        Name = "testegetlistbyemailyahoo",
        Password = "1"
    });

    User user2 = await userDAO.Post(new UserPost{
        Email = "testegetlistbyemail@gmail.com",
        Name = "testegetlistbyemailgmail",
        Password = "1"
    });

    string email = "testegetlistbyemail";
    List<UserGet> lista =  await userDAO.GetListByEmailStartsWith(email);
    int total = lista.Where(e => e.Email.StartsWith(email)).Count();

    await userDAO.Delete(user1.Id);
    await userDAO.Delete(user2.Id);

    Assert.IsTrue(total >= 2 ? true : false);
    }

    [TestMethod]
    public async Task GetListByNameStartsWith(){

    UserDAO userDAO = new UserDAO();

    User user1 = await userDAO.Post(new UserPost{
        Email = "testGetListByNameStartsWith@yahoo.com",
        Name = "GetListByNameStartsWith_abcdef",
        Password = "1"
    });

    User user2 = await userDAO.Post(new UserPost{
        Email = "testegetlistbyemail@gmail.com",
        Name = "GetListByNameStartsWith_defghi",
        Password = "1"
    });

    const string name = "GetListByNameStartsWith";
    List<UserGet> lista =  await userDAO.GetListByNameStartsWith(name);
    int total = lista.Where(e => e.Name.StartsWith(name)).Count();

    await userDAO.Delete(user1.Id);
    await userDAO.Delete(user2.Id);

    Assert.IsTrue(total >= 2 ? true : false);
    }

    [TestMethod]
    public async Task TestUserNameExistsToAnotherId(){
        
        UserDAO userDAO = new UserDAO();

        User user1 = await userDAO.Post(new UserPost{
            Email = "user1@gmail.com",
            Name = "teste1234567",
            Password = "1"
        });

        User user2 = await userDAO.Post(new UserPost{
            Email = "user2@gmail.com",
            Name = "teste1234567",
            Password = "1"
        });

        bool isValid = await userDAO.NameExistsWithIdNotEqualsTo(user1.Name, user1.Id);

        await userDAO.Delete(user1.Id);
        await userDAO.Delete(user2.Id);

        Assert.IsTrue(isValid);

    }

    [TestMethod]
    public async Task TestUserNameNotExistsToAnotherId(){
        
        UserDAO userDAO = new UserDAO();

        User user1 = await userDAO.Post(new UserPost{
            Email = "user1@gmail.com",
            Name = "testeNomeNaoExiste",
            Password = "1"
        });        

        bool isValid = await userDAO.NameExistsWithIdNotEqualsTo(user1.Name, user1.Id);

        await userDAO.Delete(user1.Id);

        Assert.IsFalse(isValid);

    }

    [TestMethod]
    public async Task TestEmailExistsToAnotherId(){
        
        UserDAO userDAO = new UserDAO();

        User user1 = await userDAO.Post(new UserPost{
            Email = "teste1234567@gmail.com",
            Name = "teste1234567",
            Password = "1"
        });

        User user2 = await userDAO.Post(new UserPost{
            Email = "teste1234567@gmail.com",
            Name = "teste1234567",
            Password = "1"
        });

        bool isValid = await userDAO.EmailExistsWithIdNotEqualsTo(user1.Email, user1.Id);

        await userDAO.Delete(user1.Id);
        await userDAO.Delete(user2.Id);

        Assert.IsTrue(isValid);

    }

    [TestMethod]
    public async Task TestEmailNotExistsToAnotherId(){
        
        UserDAO userDAO = new UserDAO();

        User user1 = await userDAO.Post(new UserPost{
            Email = "testeEmailNaoExiste@gmail.com",
            Name = "testeNomeNaoExiste",
            Password = "1"
        });        

        bool isValid = await userDAO.EmailExistsWithIdNotEqualsTo(user1.Email, user1.Id);

        await userDAO.Delete(user1.Id);

        Assert.IsFalse(isValid);

    }

}