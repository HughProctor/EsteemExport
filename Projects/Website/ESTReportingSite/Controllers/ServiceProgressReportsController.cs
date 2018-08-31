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
    public class ServiceProgressReportsController : Controller
    {
        private BAMEsteemExportContext db = new BAMEsteemExportContext();

        // GET: ServiceProgressReports
        public ActionResult Index()
        {
            return View(db.ServiceProgressReport.OrderBy(x => x.StartDateTime).ToList());
        }

        // GET: ServiceProgressReports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceProgressReport serviceProgressReport = db.ServiceProgressReport.Find(id);
            if (serviceProgressReport == null)
            {
                return HttpNotFound();
            }
            return View(serviceProgressReport);
        }

        // GET: ServiceProgressReports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceProgressReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StartDateTime,EsteemExtractDateTime,BAMExportDateTime,ProcessSuccessFlag,NewItemCount,LocationChangeCount,AssetTagChangeCount,DeployedCount,ReturnedCount,CreatedDate,UpdatedDate,DeletedDate")] ServiceProgressReport serviceProgressReport)
        {
            if (ModelState.IsValid)
            {
                db.ServiceProgressReport.Add(serviceProgressReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serviceProgressReport);
        }

        // GET: ServiceProgressReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceProgressReport serviceProgressReport = db.ServiceProgressReport.Find(id);
            if (serviceProgressReport == null)
            {
                return HttpNotFound();
            }
            return View(serviceProgressReport);
        }

        // POST: ServiceProgressReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StartDateTime,EsteemExtractDateTime,BAMExportDateTime,ProcessSuccessFlag,NewItemCount,LocationChangeCount,AssetTagChangeCount,DeployedCount,ReturnedCount,CreatedDate,UpdatedDate,DeletedDate")] ServiceProgressReport serviceProgressReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceProgressReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serviceProgressReport);
        }

        // GET: ServiceProgressReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceProgressReport serviceProgressReport = db.ServiceProgressReport.Find(id);
            if (serviceProgressReport == null)
            {
                return HttpNotFound();
            }
            return View(serviceProgressReport);
        }

        // POST: ServiceProgressReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceProgressReport serviceProgressReport = db.ServiceProgressReport.Find(id);
            db.ServiceProgressReport.Remove(serviceProgressReport);
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
