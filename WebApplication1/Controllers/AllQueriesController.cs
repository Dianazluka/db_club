using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Classes;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class AllQueriesController : Controller
    {

        
        // GET: AllQueries
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Query1()
        {
            List<Query1Data> result;
            using (var db = new sportClubsEntities())
            { 
                string sql = @"SELECT c.rank, p.result, s.surname, se.section_name, s.s_section_ID
                    FROM Participation p JOIN Competition c
                    ON p.p_сompetition_ID = c.сompetition_ID
                    JOIN Sportsman s
                    ON p.p_sportsman_ID = s.sportsman_ID
                    JOIN Section se
                    ON s.s_section_ID = se.section_ID
                    WHERE se.section_name = ''  AND c.rank = ''  AND s.s_section_ID = ''; ";
                result = db.Database.SqlQuery<Query1Data>(sql).ToList();
            }

            TempData["result"] = result;

            return RedirectToAction("Query1Result");
        }
        [HttpPost]
        public ActionResult Query2()
        {
            List<Query2Data> result;
            using (var db = new sportClubsEntities())
            {
                string sql = @"SELECT   s.sportsman_ID AS [Спортсмен], AVG(s.age) AS [Возраст]
                FROM Participation p JOIN Competition c
                ON p.p_сompetition_ID = c.сompetition_ID
                JOIN Sportsman s
                ON p.p_sportsman_ID = s.sportsman_ID
                JOIN Section se
                ON s.s_section_ID = se.section_ID
                WHERE se.section_name = '' 
                GROUP BY s.sportsman_ID";
                result = db.Database.SqlQuery<Query2Data>(sql).ToList();
            }

            TempData["result"] = result;

            return RedirectToAction("Query2Result");
        }
        [HttpPost]
        public ActionResult Query3(string letter)
        {
            List<Query3Data> result;
            using (var db = new sportClubsEntities())
            {
                string sql = @"SELECT        Section.section_name, Competition.date_competition, Sportsman.surname, Sportsman.age, Participation.place
                            FROM            Competition INNER JOIN
                         Participation ON Competition.сompetition_ID = Participation.p_сompetition_ID INNER JOIN
                         Sportsman ON Participation.p_sportsman_ID = Sportsman.sportsman_ID INNER JOIN
                         Section ON Sportsman.s_section_ID = Section.section_ID
                        WHERE        (Section.section_name = '') AND (Competition.date_competition = '') AND (Participation.place = '')";
                result = db.Database.SqlQuery<Query3Data>(sql).ToList();
            }

            TempData["result"] = result;

            return RedirectToAction("Query3Result");
        }
        [HttpPost]
        public ActionResult Query4()
        {
            List<Query4Data> result;
            using (var db = new sportClubsEntities())
            {
                string sql = @"SELECT COUNT(sp.sportsman_ID) FROM Sportsman sp
                    JOIN Section sec ON sp.s_section_ID = sec.section_ID
                    JOIN Participation p ON sp.sportsman_ID = p.p_sportsman_ID
                    JOIN Competition c ON c.сompetition_ID = p.p_сompetition_ID
                    WHERE (c.rank = '') AND (sec.section_name = '')";
                result = db.Database.SqlQuery<Query4Data>(sql).ToList();
            }

            TempData["result"] = result;

            return RedirectToAction("Query4Result");
        }
        public ActionResult Query1Result()
        {
            TempData.Keep();
            return View(TempData["result"]);
        }
        public ActionResult Query2Result()
        {
            TempData.Keep();
            return View(TempData["result"]);
        }
        public ActionResult Query3Result()
        {
            TempData.Keep();
            return View(TempData["result"]);
        }
        public ActionResult Query4Result()
        {
            TempData.Keep();
            return View(TempData["result"]);
        }



    }
}