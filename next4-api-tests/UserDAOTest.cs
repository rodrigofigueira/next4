using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using next4_api.Data;

namespace next4_api_tests;

[TestClass]
public class UserDAOTest
{
    [TestMethod]
    public async Task EmailExists()
    {
        UserDAO userDAO = new UserDAO();
        string email = "rodrigo@gmail.com";
        bool emailExists = await userDAO.EmailExists(email);
        Assert.IsTrue(emailExists, "Email existe");
    }
}