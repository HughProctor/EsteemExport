using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ESTReporting.EntityModel.Context;
using ESTReporting.EntityModel.Models;

namespace ESTReportingSite.Controllers
{
    public class PartManufacturersController : Controller
    {
        private BAMEsteemExportContext db = new BAMEsteemExportContext();

        // GET: PartManufacturers
        public ActionResult Index()
        {
            return View(db.PartManufacturers.ToList().OrderBy(x => x.Name));
        }

        // GET: PartManufacturers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartManufacturer partManufacturer = db.PartManufacturers.Find(id);
            if (partManufacturer == null)
            {
                return HttpNotFound();
            }
            return View(partManufacturer);
        }

        // GET: PartManufacturers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PartManufacturers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Code,CodeEsteem,CodeEsteemAlt")] PartManufacturer partManufacturer)
        {
            partManufacturer.CreatedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.PartManufacturers.Add(partManufacturer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(partManufacturer);
        }

        // GET: PartManufacturers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartManufacturer partManufacturer = db.PartManufacturers.Find(id);
            if (partManufacturer == null)
            {
                return HttpNotFound();
            }
            return View(partManufacturer);
        }

        // POST: PartManufacturers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Code,CodeEsteem,CodeEsteemAlt")] PartManufacturer partManufacturer)
        {
            if (ModelState.IsValid)
            {
                partManufacturer.UpdatedDate = DateTime.Now;
                db.Entry(partManufacturer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(partManufacturer);
        }

        // GET: PartManufacturers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartManufacturer partManufacturer = db.PartManufacturers.Find(id);
            if (partManufacturer == null)
            {
                return HttpNotFound();
            }
            return View(partManufacturer);
        }

        // POST: PartManufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PartManufacturer partManufacturer = db.PartManufacturers.Find(id);
            db.PartManufacturers.Remove(partManufacturer);
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
