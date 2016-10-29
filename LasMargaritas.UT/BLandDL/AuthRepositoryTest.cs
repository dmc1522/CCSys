using LasMargaritas.DL.EF;
using LasMargaritas.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LasMargaritas.ULT
{
    [TestClass]
    public class AuthRepositoryTest
    {
        public AuthRepositoryTest()
        {
        }
        [TestMethod]
        public void TestRegisterUser()
        {
            AuthRepository repository = new AuthRepository();
            var result = repository.RegisterUser(new User() { UserName = "Test"+DateTime.Now.ToString("yyyyMMddhhmmssfff"), Password = "Pass12", ConfirmPassword = "Pass12" }).Result;
            Assert.IsTrue(result.Succeeded);
            
        }
           
    }
}
