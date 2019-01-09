using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class SportsmanController : Controller
    {
        private sportClubsEntities db = new sportClubsEntities();
        [AllowAnonymous]
        // GET: Sportsmen
        public ActionResult Index()
        {
            var sportsman = db.Sportsman.Include(s => s.s_section_ID);
            return View(sportsman.ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Index(string search)
        {
            var result = db.Sportsman
                 .Include(s => s.s_section_ID)
                .Where(s =>  s.surname.ToString().Contains(search.ToLower())
                || s.age.ToString().Contains(search.ToLower())
                || s.address.ToString().Contains(search.ToLower())
                || s.phone.ToString().Contains(search.ToLower())
                || s.growth.ToString().Contains(search.ToLower())
                || s.weight.ToString().Contains(search.ToLower())
                || s.achievement.ToString().Contains(search.ToLower()))
                .ToList();
            return View(result);
            }
        [AllowAnonymous]
        // GET: Sportsmen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sportsman sportsman = db.Sportsman.Find(id);
            if (sportsman == null)
            {
                return HttpNotFound();
            }
            return View(sportsman);
        }
       
        // GET: Sportsmen/Create
        public ActionResult Create()
        {
            ViewBag.s_section_ID = new SelectList(db.Section, "section_ID", "section_name");
            return View();
        }
       
        // POST: Sportsmen/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sportsman_ID,s_section_ID,surname,age,address,phone,growth,weight,achievement")] Sportsman sportsman)
        {
            if (ModelState.IsValid)
            {
                db.Sportsman.Add(sportsman);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.s_section_ID = new SelectList(db.Section, "section_ID", "section_name", sportsman.s_section_ID);
            return View(sportsman);
        }
        
        // GET: Sportsmen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sportsman sportsman = db.Sportsman.Find(id);
            if (sportsman == null)
            {
                return HttpNotFound();
            }
            ViewBag.s_section_ID = new SelectList(db.Section, "section_ID", "section_name", sportsman.s_section_ID);
            return View(sportsman);
        }
        
        // POST: Sportsmen/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sportsman_ID,s_section_ID,surname,age,address,phone,growth,weight,achievement")] Sportsman sportsman)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sportsman).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.s_section_ID = new SelectList(db.Section, "section_ID", "section_name", sportsman.s_section_ID);
            return View(sportsman);
        }
        
        // GET: Sportsmen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sportsman sportsman = db.Sportsman.Find(id);
            if (sportsman == null)
            {
                return HttpNotFound();
            }
            return View(sportsman);
        }
        
        // POST: Sportsmen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sportsman sportsman = db.Sportsman.Find(id);
            db.Sportsman.Remove(sportsman);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
