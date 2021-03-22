using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FullElectricCars.Models;

namespace FullElectricCars.Controllers
{
    public class manufacturersController : Controller
    {
        private LikeFinal db = new LikeFinal();

        // GET: manufacturers
        public ActionResult Index()
        {
            return View(db.manufacturers.ToList());
        }

        // GET: manufacturers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            manufacturers manufacturers = db.manufacturers.Find(id);
            if (manufacturers == null)
            {
                return HttpNotFound();
            }
            return View(manufacturers);
        }

        // GET: manufacturers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: manufacturers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mid,manufacturerName,addressLine1,city,postcode,tel,email,site")] manufacturers manufacturers)
        {
            if (ModelState.IsValid)
            {
                db.manufacturers.Add(manufacturers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(manufacturers);
        }

        // GET: manufacturers/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            manufacturers manufacturers = db.manufacturers.Find(id);
            if (manufacturers == null)
            {
                return HttpNotFound();
            }
            return View(manufacturers);
        }

        // POST: manufacturers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mid,manufacturerName,addressLine1,city,postcode,tel,email,site")] manufacturers manufacturers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manufacturers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manufacturers);
        }

        // GET: manufacturers/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            manufacturers manufacturers = db.manufacturers.Find(id);
            if (manufacturers == null)
            {
                return HttpNotFound();
            }
            return View(manufacturers);
        }

        // POST: manufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            manufacturers manufacturers = db.manufacturers.Find(id);
            db.manufacturers.Remove(manufacturers);
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
