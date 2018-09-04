using ESTReporting.EntityModel.Context;
using ESTReporting.EntityModel.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ESTReportingSite.Controllers
{
    public class BAM_ReportingController : Controller
    {
        private BAMEsteemExportContext db = new BAMEsteemExportContext();

        // GET: BAM_Reporting
        public ActionResult Index()
        {
            var data = db.BAM_Reporting
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

        public ActionResult BAM_HardwareTemplate_From_Report(int id)
        {
            var data = db.BAM_Reporting
                .Include(x => x.SCAuditDeploy_Item)
                .Include(x => x.SCAudit_Item)
                .Include(x => x.BAM_HardwareTemplate_Exception)
                .Where(x => x.ServiceProgressReport.Id == id)
                .ToList();

            return View("Index", data);
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

        // GET: BAM_Reporting/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BAM_Reporting bAM_Reporting = db.BAM_Reporting.Find(id);
            if (bAM_Reporting == null)
            {
                return HttpNotFound();
            }
            return View(bAM_Reporting);
        }

        // GET: BAM_Reporting/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BAM_Reporting/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreatedDate,UpdatedDate,DeletedDate")] BAM_Reporting bAM_Reporting)
        {
            if (ModelState.IsValid)
            {
                db.BAM_Reporting.Add(bAM_Reporting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bAM_Reporting);
        }

        // GET: BAM_Reporting/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BAM_Reporting bAM_Reporting = db.BAM_Reporting.Find(id);
            if (bAM_Reporting == null)
            {
                return HttpNotFound();
            }
            return View(bAM_Reporting);
        }

        // POST: BAM_Reporting/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedDate,UpdatedDate,DeletedDate")] BAM_Reporting bAM_Reporting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bAM_Reporting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bAM_Reporting);
        }

        // GET: BAM_Reporting/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BAM_Reporting bAM_Reporting = db.BAM_Reporting.Find(id);
            if (bAM_Reporting == null)
            {
                return HttpNotFound();
            }
            return View(bAM_Reporting);
        }

        // POST: BAM_Reporting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BAM_Reporting bAM_Reporting = db.BAM_Reporting.Find(id);
            db.BAM_Reporting.Remove(bAM_Reporting);
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
