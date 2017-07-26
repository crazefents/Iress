using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class SubsidiariesController : Controller
    {
        private SubsidiariesEntities db = new SubsidiariesEntities();

        // GET: Subsidiaries
        public ActionResult Index()
        {
            var subsidiaries = db.Subsidiaries.Include(s => s.CompanyName).Include(s => s.SubsidiariesType);
            return View(subsidiaries.ToList());
        }

        // GET: Subsidiaries/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subsidiary subsidiary = db.Subsidiaries.Find(id);
            if (subsidiary == null)
            {
                return HttpNotFound();
            }
            return View(subsidiary);
        }

        // GET: Subsidiaries/Create
        public ActionResult Create()
        {
            ViewBag.CompanyID = new SelectList(db.CompanyNames, "CompanyID", "ExchangeCode");
            ViewBag.SubType = new SelectList(db.SubsidiariesTypes, "SubType", "Description");
            return View();
        }

        // POST: Subsidiaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyID,SubsiID,SubType,SubPercent,DirectRel,UpdateDate,ChangeDate,NoShares_YN")] Subsidiary subsidiary)
        {
            if (ModelState.IsValid)
            {
                db.Subsidiaries.Add(subsidiary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyID = new SelectList(db.CompanyNames, "CompanyID", "ExchangeCode", subsidiary.CompanyID);
            ViewBag.SubType = new SelectList(db.SubsidiariesTypes, "SubType", "Description", subsidiary.SubType);
            return View(subsidiary);
        }

        // GET: Subsidiaries/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subsidiary subsidiary = db.Subsidiaries.Find(id);
            if (subsidiary == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.CompanyNames, "CompanyID", "ExchangeCode", subsidiary.CompanyID);
            ViewBag.SubType = new SelectList(db.SubsidiariesTypes, "SubType", "Description", subsidiary.SubType);
            return View(subsidiary);
        }

        // POST: Subsidiaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyID,SubsiID,SubType,SubPercent,DirectRel,UpdateDate,ChangeDate,NoShares_YN")] Subsidiary subsidiary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subsidiary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(db.CompanyNames, "CompanyID", "ExchangeCode", subsidiary.CompanyID);
            ViewBag.SubType = new SelectList(db.SubsidiariesTypes, "SubType", "Description", subsidiary.SubType);
            return View(subsidiary);
        }

        // GET: Subsidiaries/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subsidiary subsidiary = db.Subsidiaries.Find(id);
            if (subsidiary == null)
            {
                return HttpNotFound();
            }
            return View(subsidiary);
        }

        // POST: Subsidiaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Subsidiary subsidiary = db.Subsidiaries.Find(id);
            db.Subsidiaries.Remove(subsidiary);
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
