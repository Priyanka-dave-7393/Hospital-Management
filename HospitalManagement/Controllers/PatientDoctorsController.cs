using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospitalManagement.Models;

namespace HospitalManagement.Controllers
{
    [Authorize]
    public class PatientDoctorsController : Controller
    {
        private HmsDb db = new HmsDb();

        // GET: PatientDoctors
        public ActionResult Index()
        {
            return View(db.PatientDoctors.ToList());
        }

        // GET: PatientDoctors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientDoctors patientDoctors = db.PatientDoctors.Find(id);
            if (patientDoctors == null)
            {
                return HttpNotFound();
            }
            return View(patientDoctors);
        }

        // GET: PatientDoctors/Create
        public ActionResult Create()
        {
            ViewBag.DoctorId = new SelectList(db.Doctors, "DoctorId", "Firstname");
            ViewBag.PatientId = new SelectList(db.Patients, "PatientId", "Firstname");
            return View();
        }

        // POST: PatientDoctors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatientId,DoctorID")] PatientDoctors patientDoctors)
        {
            if (ModelState.IsValid)
            {
                db.PatientDoctors.Add(patientDoctors);
                db.SaveChanges();
                return RedirectToAction("Create","Bills");
            }

            return View(patientDoctors);
        }

        // GET: PatientDoctors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientDoctors patientDoctors = db.PatientDoctors.Find(id);
            if (patientDoctors == null)
            {
                return HttpNotFound();
            }
            return View(patientDoctors);
        }

        // POST: PatientDoctors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PatientId,DoctorID")] PatientDoctors patientDoctors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patientDoctors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patientDoctors);
        }

        // GET: PatientDoctors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientDoctors patientDoctors = db.PatientDoctors.Find(id);
            if (patientDoctors == null)
            {
                return HttpNotFound();
            }
            return View(patientDoctors);
        }

        // POST: PatientDoctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PatientDoctors patientDoctors = db.PatientDoctors.Find(id);
            db.PatientDoctors.Remove(patientDoctors);
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
