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
    public class BAM_ModelLookupController : Controller
    {
        private BAMEsteemExportContext db = new BAMEsteemExportContext();

        // GET: BAM_ModelLookup
        public ActionResult Index()
        {
            return View(db.EST_BAM_ModelLookup.OrderByDescending(x => x.IsActive).ToList());
        }

        // GET: BAM_ModelLookup/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EST_BAM_ModelLookup eST_BAM_ModelLookup = db.EST_BAM_ModelLookup.Find(id);
            if (eST_BAM_ModelLookup == null)
            {
                return HttpNotFound();
            }
            return View(eST_BAM_ModelLookup);
        }

        // GET: BAM_ModelLookup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BAM_ModelLookup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EST_ManufacturerCode,EST_ModelDescription,BAM_Name,BAM_ModelString,BAM_ManufacturerString,BAM_ModelType,BAM_DisplayName,BAM_BaseId,IsActive,CreatedDate,UpdatedDate,DeletedDate")] EST_BAM_ModelLookup eST_BAM_ModelLookup)
        {
            if (ModelState.IsValid)
            {
                db.EST_BAM_ModelLookup.Add(eST_BAM_ModelLookup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eST_BAM_ModelLookup);
        }

        // GET: BAM_ModelLookup/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EST_BAM_ModelLookup eST_BAM_ModelLookup = db.EST_BAM_ModelLookup.Find(id);
            if (eST_BAM_ModelLookup == null)
            {
                return HttpNotFound();
            }
            return View(eST_BAM_ModelLookup);
        }

        // POST: BAM_ModelLookup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EST_ManufacturerCode,EST_ModelDescription,BAM_Name,BAM_ModelString,BAM_ManufacturerString,BAM_ModelType,BAM_DisplayName,BAM_BaseId,IsActive,CreatedDate,UpdatedDate,DeletedDate")] EST_BAM_ModelLookup eST_BAM_ModelLookup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eST_BAM_ModelLookup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eST_BAM_ModelLookup);
        }

        // GET: BAM_ModelLookup/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EST_BAM_ModelLookup eST_BAM_ModelLookup = db.EST_BAM_ModelLookup.Find(id);
            if (eST_BAM_ModelLookup == null)
            {
                return HttpNotFound();
            }
            return View(eST_BAM_ModelLookup);
        }

        // POST: BAM_ModelLookup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EST_BAM_ModelLookup eST_BAM_ModelLookup = db.EST_BAM_ModelLookup.Find(id);
            db.EST_BAM_ModelLookup.Remove(eST_BAM_ModelLookup);
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
