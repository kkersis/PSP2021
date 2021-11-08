using LibraryImplementation.Data;
using LibraryImplementation.Models;
using LibraryImplementation.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryImplementation.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ActionResult List()
        {
            var users = _userRepository.GetAll();
            return View(users);
        }

        public ActionResult Details(int id)
        {
            var user = _userRepository.GetById(id);
            return View(user);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            var validatorResponse = UserValidator.Validate(user);
            if (validatorResponse.IsValid)
            {
                _userRepository.Save(user);
                return RedirectToAction("List");
            }
            else
            {
                TempData["ErrorMessage"] = validatorResponse.ErrorMessage;
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var user = _userRepository.GetById(id);
            return View(user);
        }

        public ActionResult Delete(int id)
        {
            _userRepository.DeleteById(id);
            return RedirectToAction("List");
        }
    }
}
