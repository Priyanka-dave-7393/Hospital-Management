using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospitalManagement.Models;
using Rotativa;

namespace HospitalManagement.Controllers
{
    [Authorize]
    public class BillsController : Controller
    {
        private HmsDb db = new HmsDb();

        // GET: Bills
        public ActionResult Index()
        {
            var bills = db.Bills.Include(b => b.Doctor).Include(b => b.Patient).Include(b => b.Room);
            return View(bills.ToList());
        }
        public ActionResult PrintPDF()
        {
            var report = new ActionAsPdf("Index");
            return report;
        }
       
        // GET: Bills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }
        public ActionResult PrintPartialPDF()
        {
            var report = new ActionAsPdf("Details");
            return report;
        }

        // GET: Bills/Create
        public ActionResult Create()
        {
            ViewBag.DoctorId = new SelectList(db.Doctors, "DoctorId", "Firstname");
            ViewBag.PatientId = new SelectList(db.Patients, "PatientId", "Firstname");
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "Name");
            return View();
        }

        // POST: Bills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BillId,DoctorId,RoomId,DoctorCharge,RoomCharge,Dischargedate,PatientId")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Bills.Add(bill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoctorId = new SelectList(db.Doctors, "DoctorId", "Firstname", bill.DoctorId);
            ViewBag.PatientId = new SelectList(db.Patients, "PatientId", "Firstname", bill.PatientId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "Name", bill.RoomId);
            return View(bill);
        }

        // GET: Bills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorId = new SelectList(db.Doctors, "DoctorId", "Firstname", bill.DoctorId);
            ViewBag.PatientId = new SelectList(db.Patients, "PatientId", "Firstname", bill.PatientId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "Name", bill.RoomId);
            return View(bill);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BillId,DoctorId,RoomId,DoctorCharge,RoomCharge,Dischargedate,PatientId")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorId = new SelectList(db.Doctors, "DoctorId", "Firstname", bill.DoctorId);
            ViewBag.PatientId = new SelectList(db.Patients, "PatientId", "Firstname", bill.PatientId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "Name", bill.RoomId);
            return View(bill);
        }

        // GET: Bills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bill bill = db.Bills.Find(id);
            db.Bills.Remove(bill);
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
