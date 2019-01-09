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
    public class ParticipationsController : Controller
    {
        private sportClubsEntities db = new sportClubsEntities();
        [AllowAnonymous]
        // GET: Participations
        public ActionResult Index()
        {
            var participations = db.Participation.Include(p => p.Competition).Include(p => p.Sportsman);
            return View(participations.ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Index(string search)
    {
        var result = db.Participation
            .Include(p => p.p_сompetition_ID)
            .Include(p => p.p_sportsman_ID)
            .Where(p =>  p.result.ToString().Contains(search.ToLower())
            || p.place.ToString().Contains(search.ToLower()))
            .ToList();
        return View(result);
            }
        [AllowAnonymous]
        // GET: Participations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participation participation = db.Participation.Find(id);
            if (participation == null)
            {
                return HttpNotFound();
            }
            return View(participation);
        }
        
        // GET: Participations/Create
        public ActionResult Create()
        {
            ViewBag.p_сompetition_ID = new SelectList(db.Competition, "сompetition_ID", "date_competition");
            ViewBag.p_sportsman_ID = new SelectList(db.Sportsman, "sportsman_ID", "surname");
            return View();
        }
        
        // POST: Participations/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "participation_ID,p_sportsman_ID,p_сompetition_ID,result,place")] Participation participation)
        {
            if (ModelState.IsValid)
            {
                db.Participation.Add(participation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.p_сompetition_ID = new SelectList(db.Competition, "сompetition_ID", "date_competition", participation.p_сompetition_ID);
            ViewBag.p_sportsman_ID = new SelectList(db.Sportsman, "sportsman_ID", "surname", participation.p_sportsman_ID);
            return View(participation);
        }
    
    // GET: Participations/Edit/5
    public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participation participation = db.Participation.Find(id);
            if (participation == null)
            {
                return HttpNotFound();
            }
            ViewBag.p_сompetition_ID = new SelectList(db.Competition, "сompetition_ID", "date_competition", participation.p_сompetition_ID);
            ViewBag.p_sportsman_ID = new SelectList(db.Sportsman, "sportsman_ID", "surname", participation.p_sportsman_ID);
            return View(participation);
        }
    
    // POST: Participations/Edit/5
    // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
    // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "participation_ID,p_sportsman_ID,p_сompetition_ID,result,place")] Participation participation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(participation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.p_сompetition_ID = new SelectList(db.Competition, "сompetition_ID", "date_competition", participation.p_сompetition_ID);
            ViewBag.p_sportsman_ID = new SelectList(db.Sportsman, "sportsman_ID", "surname", participation.p_sportsman_ID);
            return View(participation);
        }
    
    // GET: Participations/Delete/5
    public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participation participation = db.Participation.Find(id);
            if (participation == null)
            {
                return HttpNotFound();
            }
            return View(participation);
        }
   
    // POST: Participations/Delete/5
    [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Participation participation = db.Participation.Find(id);
            db.Participation.Remove(participation);
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
