using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FullElectricCars.Models;

namespace FullElectricCars.Controllers
{
    public class carDetailsController : Controller
    {
        private LikeFinal db = new LikeFinal();

        // GET: carDetails
        public ActionResult Index()
        {
            var carDetails = db.carDetails.Include(c => c.manufacturers);
            return View(carDetails.ToList());
        }
      
        // GET: carDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            carDetails carDetails = db.carDetails.Find(id);
            if (carDetails == null)
            {
                return HttpNotFound();
            }
            return View(carDetails);
        }

        // GET: carDetails/Create
        public ActionResult Create()
        {
            ViewBag.mid = new SelectList(db.manufacturers, "mid", "manufacturerName");
            return View();
        }

        // POST: carDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cid,mid,model,price,mpc,image,video,likes,ImageFile")] carDetails carDetails)
        {
            if (carDetails.image == null)
            {
                carDetails.image = "~/Content/images/default1.png";
            }

            if (ModelState.IsValid)
            {
                //Upload the file wthout the extension

                string imagename = Path.GetFileNameWithoutExtension(carDetails.ImageFile.FileName);
                string extension = Path.GetExtension(carDetails.ImageFile.FileName);
                imagename = imagename + extension;
                carDetails.image = imagename;

                imagename = Path.Combine(Server.MapPath("~/Content/images/"), imagename);
                carDetails.ImageFile.SaveAs(imagename);

                //Add newCar to database

                db.carDetails.Add(carDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.mid = new SelectList(db.manufacturers, "mid", "manufacturerName", carDetails.mid);
            return View(carDetails);
        }

        // GET: carDetails/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            carDetails carDetails = db.carDetails.Find(id);
            if (carDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.mid = new SelectList(db.manufacturers, "mid", "manufacturerName", carDetails.mid);
            return View(carDetails);
        }

        // POST: carDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cid,mid,model,price,mpc,image,video,likes")] carDetails carDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.mid = new SelectList(db.manufacturers, "mid", "manufacturerName", carDetails.mid);
            return View(carDetails);
        }

        // GET: carDetails/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            carDetails carDetails = db.carDetails.Find(id);
            if (carDetails == null)
            {
                return HttpNotFound();
            }
            return View(carDetails);
        }

        // POST: carDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            carDetails carDetails = db.carDetails.Find(id);
            db.carDetails.Remove(carDetails);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult Miles()
        {
            var carDetails = db.carDetails.Include(c => c.manufacturers);
            return View(carDetails.ToList());
        }

        public ActionResult _index(int? cid)
        {
            //var carDetails = db.carDetails.Include(c => c.manufacturers);
            var carDetail = db.carDetails.Find(cid);
            return PartialView(carDetail);
        }

        //used by Index view to update likes when likes button is clicked
        [HttpPost]
        public ActionResult Like(int? cid)
        {
            if (cid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            carDetails carDetails = db.carDetails.Find(cid);
            if (carDetails == null)
            {
                return HttpNotFound();
            }

            int currentLikes = (int)carDetails.likes;
            carDetails.likes = currentLikes + 1;

            if (ModelState.IsValid)
            {
                db.Entry(carDetails).State = EntityState.Modified;
                db.SaveChanges();
            }

            carDetails = db.carDetails.Find(cid);

            return PartialView("_Index", carDetails);
        }

        //Ajax Test
        public string Test()
        {
            return ("Ajax is working !");
        }
        //End Ajax Test

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        

        ////View MPC Chart
        //public ActionResult mpcComparison()
        //{

        //    return View(mpcComparison);
        //}
    }
}
