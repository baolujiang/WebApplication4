using MusicStore.Models;
using MusicStore.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace MusicStore.Controllers
{
    public class ArtistsController : Controller
    {
        //MusicStoreDataContext context = new MusicStoreDataContext();

        ArtistRepository repo = new ArtistRepository();

        public ActionResult Details(int id)
        {
            var artist = repo.Get(id);
            if (artist == null)
                return HttpNotFound();
            else
                return View(artist);
        }

        // GET: Artists
        public ActionResult Index()
        {
            return View(repo.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }
            
        [HttpPost]
        public ActionResult Create(Artist artist)
        {
            if (!ModelState.IsValid) return View(artist);

            repo.Add(artist);
            repo.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var artist = repo.Get(id);

            if (artist == null) return HttpNotFound();

            return View(artist);
        }

        [HttpPost]
        public ActionResult Edit(Artist artist)
        {
            if (!ModelState.IsValid) return View(artist);
            try
            {
                repo.Update(artist);
                repo.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ViewBag.Message = "Sorry! Someone else has updated the record.";
                return View(artist);
            }
        }
    }
}