using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Aplication.Services.UserAccount.Interfaces;
using DataAccess;
using DataAccess.Repository;
using DBFMvcMovies.Models;
using Domain.Models;

namespace DBFMvcMovies.Controllers
{
    public class UserAccountsController : Controller
    {        
        private IUserAccountAppService _userAccountAppService;

        public UserAccountsController(     
            IUserAccountAppService userAccountAppService)
        {            
            _userAccountAppService = userAccountAppService;
        }

        //public UserAccountsController()
        //{
        //    _genericRepository = new GeneralReposity<Domain.Models.UserAccount>();
        //}

        // GET: UserAccounts
        public ActionResult Index()
        {
            return View(_userAccountAppService.GetAll());
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
        public ActionResult Register(UserAccountsModel userAccountsModel)
        {            
            var lista = _userAccountAppService.GetAll().Where(ua => ua.Username == userAccountsModel.Username && ua.Password == userAccountsModel.Password).FirstOrDefault();

            if (lista == null)
            {
                _userAccountAppService.Insert(userAccountsModel.GetDocumentoTipo());
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Ya existe un Usuario con ese Alias/Contraseña");
            }
            if (ModelState.IsValid)
            {
                _userAccountAppService.Save();
                return RedirectToAction("Login");                
            }            
            return View(userAccountsModel);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Domain.Models.UserAccount user, string returnUrl)
        {
            var usr = _userAccountAppService.GetAll().FirstOrDefault(s => s.Username == user.Username && s.Password == user.Password);
            if (usr != null)
            {
                Session["UserID"] = usr.Id.ToString();
                FormsAuthentication.SetAuthCookie(usr.Username, false);

                // Redirect to URL "returnURL" if "returnURL" is NOT null; otherwise, Redirect Movie Index View.
                return Redirect(returnUrl ?? Url.Action("Index", "Movies"));
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

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }

    }
}
