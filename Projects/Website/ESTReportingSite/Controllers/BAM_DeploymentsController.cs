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
    public class BAM_DeploymentsController : Controller
    {
        private BAMEsteemExportContext db = new BAMEsteemExportContext();

        // GET: BAM_Deployments
        public ActionResult Index()
        {
            var data = db.BAM_Deployments
                .Include(x => x.SCAuditDeploy_Item)
                .Include(x => x.SCAudit_Item)
                .Include(x => x.BAM_HardwareTemplate_Exception)
                .ToList();
            return View(data);
        }

        public ActionResult BAM_HardwareTemplate(int id)
        {
            var data = db.BAM_HardwareTemplate_Full
                .Single(x => x.Id == id);

            return PartialView("BAM_HardwareTemplate_Full_PartialView", data);
        }

        public ActionResult EST_SCAudit(int id)
        {
            var data = db.SCAudit
              .Single(x => x.Id == id);
            return PartialView("EST_SCAudit_PartialView", data);
        }

        public ActionResult EST_SCAuditDeploy(int id)
        {
            var data = db.SCAuditDeploy
                .Single(x => x.Id == id);
            return PartialView("EST_SCAuditDeploy_PartialView", data);
        }

        // GET: BAM_Deployments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BAM_Deployments bAM_Deployments = db.BAM_Deployments.Find(id);
            if (bAM_Deployments == null)
            {
                return HttpNotFound();
            }
            return View(bAM_Deployments);
        }

        // GET: BAM_Deployments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BAM_Deployments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreatedDate,UpdatedDate,DeletedDate")] BAM_Deployments bAM_Deployments)
        {
            if (ModelState.IsValid)
            {
                db.BAM_Deployments.Add(bAM_Deployments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bAM_Deployments);
        }

        // GET: BAM_Deployments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BAM_Deployments bAM_Deployments = db.BAM_Deployments.Find(id);
            if (bAM_Deployments == null)
            {
                return HttpNotFound();
            }
            return View(bAM_Deployments);
        }

        // POST: BAM_Deployments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedDate,UpdatedDate,DeletedDate")] BAM_Deployments bAM_Deployments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bAM_Deployments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bAM_Deployments);
        }

        // GET: BAM_Deployments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BAM_Deployments bAM_Deployments = db.BAM_Deployments.Find(id);
            if (bAM_Deployments == null)
            {
                return HttpNotFound();
            }
            return View(bAM_Deployments);
        }

        // POST: BAM_Deployments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BAM_Deployments bAM_Deployments = db.BAM_Deployments.Find(id);
            db.BAM_Deployments.Remove(bAM_Deployments);
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
