using LibraryImplementation.Controllers;
using LibraryImplementation.Data;
using LibraryImplementation.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LibraryImplementation.Tests
{
    public class UserControllerTests : IDisposable
    {
        private readonly IUserRepository _userRepository;
        private readonly string _filePath = "./USERTEST.txt";

        public UserControllerTests()
        {
            _userRepository = new UserRepository(_filePath);
            InsertUsers();
        }

        [Fact]
        public void TestList()
        {
            var users = _userRepository.GetAll();
            var controller = new UserController(_userRepository);

            var result = controller.List();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<User>>(viewResult.ViewData.Model);
            Assert.Equal(users.Count(), model.Count);
        }

        [Fact]
        public void TestDetails()
        {
            var user = _userRepository.GetById(1);
            var controller = new UserController(_userRepository);

            var result = controller.Details(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<User>(viewResult.ViewData.Model);
            Assert.Equal(user.FirstName, model.FirstName);
        }

        [Fact]
        public void TestCreate()
        {
            var currentCount = _userRepository.GetAll().Count();
            var user = new User()
            {
                Id = 0,
                FirstName = "Jonas",
                LastName = "Jonauskas",
                Email = "email@gmail.com",
                Address = "Lietuva",
                PhoneNumber = "+37062515478",
                Password = "PassWord!123"
            };
            var controller = new UserController(_userRepository);

            var result = controller.Create(user);

            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(currentCount + 1, _userRepository.GetAll().Count());
        }

        [Fact]
        public void TestDelete()
        {
            var currentCount = _userRepository.GetAll().Count();
            var controller = new UserController(_userRepository);

            var result = controller.Delete(1);

            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(currentCount - 1, _userRepository.GetAll().Count());
        }

        private void InsertUsers()
        {
            var user1 = new User()
            {
                Id = 0,
                FirstName = "Antanas",
                LastName = "Antanauskas",
                Email = "email@gmail.com",
                Address = "Lietuva",
                PhoneNumber = "+37062515478",
                Password = "PassWord!123"
            };

            var user2 = new User()
            {
                Id = 0,
                FirstName = "Antanas",
                LastName = "Petrauskas",
                Email = "emailpetrauskas@gmail.com",
                Address = "Vilnius",
                PhoneNumber = "+37062515478",
                Password = "PassWord!123"
            };

            _userRepository.Save(user1);
            _userRepository.Save(user2);
        }

        public void Dispose()
        {
            try
            {
                File.Delete(_filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
