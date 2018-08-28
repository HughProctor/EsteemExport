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
    public class ESTPartsController : Controller
    {
        private BAMEsteemExportContext db = new BAMEsteemExportContext();

        // GET: ESTParts
        public ActionResult Index()
        {
            return View(db.ESTParts.ToList());
        }

        // GET: ESTParts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ESTPart eSTPart = db.ESTParts.Find(id);
            if (eSTPart == null)
            {
                return HttpNotFound();
            }
            return View(eSTPart);
        }

        // GET: ESTParts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ESTParts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreatedDate,UpdatedDate,Manufacturer,Model,SerialNumber,AssetName,DisplayName,RequestUser,CostCode")] ESTPart eSTPart)
        {
            if (ModelState.IsValid)
            {
                db.ESTParts.Add(eSTPart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eSTPart);
        }

        // GET: ESTParts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ESTPart eSTPart = db.ESTParts.Find(id);
            if (eSTPart == null)
            {
                return HttpNotFound();
            }
            return View(eSTPart);
        }

        // POST: ESTParts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedDate,UpdatedDate,Manufacturer,Model,SerialNumber,AssetName,DisplayName,RequestUser,CostCode")] ESTPart eSTPart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eSTPart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eSTPart);
        }

        // GET: ESTParts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ESTPart eSTPart = db.ESTParts.Find(id);
            if (eSTPart == null)
            {
                return HttpNotFound();
            }
            return View(eSTPart);
        }

        // POST: ESTParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ESTPart eSTPart = db.ESTParts.Find(id);
            db.ESTParts.Remove(eSTPart);
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
