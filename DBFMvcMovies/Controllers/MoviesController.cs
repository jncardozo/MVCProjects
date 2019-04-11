using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccess.Repository;
using DBFMvcMovies.Security;
using Domain.Models;

namespace DBFMvcMovies.Controllers
{
    public class MoviesController : Controller
    {
        private IGenericRepository<Movies> _genericRepository;

        public MoviesController(IGenericRepository<Movies> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public MoviesController()
        {
            _genericRepository = new GeneralReposity<Movies>();
        }

        // GET: Movies
        [Authorize]
        public ActionResult Index(string movieGenre, string searchString)
        {
            var movies = from m in _genericRepository.GetAll()
                         select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }
            return View(movies);
        }

        // GET: Movies/Details/5
        public ActionResult Details(int id)
        {
            Movies movie = _genericRepository.GetById(id);
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieId,Title,ReleaseDate,Genre,Price")] Movies movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _genericRepository.Insert(movie);
                    _genericRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Movies movie = _genericRepository.GetById(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovieId,Title,ReleaseDate,Genre,Price")] Movies movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _genericRepository.Update(movie);
                    _genericRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(bool? saveChangesError = false, int  id=0)
        {            
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Movies movie = _genericRepository.GetById(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movies movie = _genericRepository.GetById(id);
            _genericRepository.Delete(id);
            _genericRepository.Save();
            return RedirectToAction("Index");
        }        
    }
}
