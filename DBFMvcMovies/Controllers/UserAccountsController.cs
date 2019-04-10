using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Repository;
using Domain.Models;

namespace DBFMvcMovies.Controllers
{
    public class UserAccountsController : Controller
    {
        private IGenericRepository<UserAccount> _genericRepository;
        
        public UserAccountsController()
        {
            _genericRepository = new GeneralReposity<UserAccount>();
        }

        public UserAccountsController(IGenericRepository<UserAccount> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        // GET: UserAccounts
        public ActionResult Index()
        {
            return View(_genericRepository.GetAll());
        }
                

        // GET: UserAccounts/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: UserAccounts/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Id,FirstName,LastName,Email,Username,Password,ConfirmPassword")] UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                _genericRepository.Insert(userAccount);
                _genericRepository.Save();
                return RedirectToAction("Login");
            }
            return View(userAccount);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAccount user)
        {
            var usr = _genericRepository.GetAll().FirstOrDefault(s => s.Username == user.Username && s.Password == user.Password);
            if (usr != null)
            {
                Session["UserID"] = usr.Id.ToString();
                Session["Username"] = usr.FirstName.ToString();
                return RedirectToAction("Index", "Movies");
            }
            else
            {
                ModelState.AddModelError("", "Username or Password is wrong. ");
            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


    }
}
