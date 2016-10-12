using LasMargaritas.BL;
using LasMargaritas.DL.EF;
using LasMargaritas.Models;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace LasMargaritas.ULT
{
    [TestClass]
    public class TestAuthRepository
    {
        public TestAuthRepository()
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
