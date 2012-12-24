using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanarkshireGamers;
using System.Data.Entity.Validation;
using System.Text;

namespace LanarkshireGamers.Controllers
{ 
    public class GameController : Controller
    {
        private LanarkshireGamersContext db = new LanarkshireGamersContext();

        //

        // GET: /Game/
        public ViewResult Index()
        {
            return View(db.Game.ToList());
        }

        //
        // GET: /Game/Details/5
        public ViewResult Details(int id)
        {
            Game game = db.Game.Find(id);
            return View(game);
        }

        //
        // GET: /Game/Create
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Game/Create

        [HttpPost]
        public ActionResult Create(Game game)
        {
            if (ModelState.IsValid)
            {
                db.Game.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(game);
        }
        
        //
        // GET: /Game/Edit/5
        public ActionResult Edit(int id)
        {
            Game game = db.Game.Find(id);
            return View(game);
        }

        //
        // POST: /Game/Edit/5
        [HttpPost]
        public ActionResult Edit(Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(game);
        }

        //
        // GET: /Game/Delete/5
        public ActionResult Delete(int id)
        {
            Game game = db.Game.Find(id);
            return View(game);
        }

        // POST: /Game/CreateFromGeekCollection/BigBearScot
        public ActionResult CreateFromGeekCollection(string id)
        {
            foreach (Game g in GeekHelpers.GetCollectionFromID(id))
            {
                db.Game.Add(g);
            }
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var errors in ex.EntityValidationErrors)
                {
                    foreach (var error in errors.ValidationErrors)
                    {
                        sb.AppendFormat("{0} {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
                string errorStr = sb.ToString();
                throw new Exception("Validation Errors " + sb.ToString(),ex);
            }
            return RedirectToAction("Index");
        }

        //
        // POST: /Game/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Game game = db.Game.Find(id);
            db.Game.Remove(game);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}