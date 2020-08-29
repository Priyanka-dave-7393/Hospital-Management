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
    public class RoomPatientsController : Controller
    {
        private HmsDb db = new HmsDb();

        // GET: RoomPatients
        public ActionResult Index()
        {
            return View(db.RoomPatients.ToList());
        }

        // GET: RoomPatients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomPatients roomPatients = db.RoomPatients.Find(id);
            if (roomPatients == null)
            {
                return HttpNotFound();
            }
            return View(roomPatients);
        }

        // GET: RoomPatients/Create
        public ActionResult Create()
        {
            ViewBag.PatientId = new SelectList(db.Patients, "PatientId", "Firstname");
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "Name");
            return View();
        }

        // POST: RoomPatients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoomId,PatinetId")] RoomPatients roomPatients)
        {
            if (ModelState.IsValid)
            {
                db.RoomPatients.Add(roomPatients);
                db.SaveChanges();
                return RedirectToAction("Create","PatientDoctors");
            }

            return View(roomPatients);
        }

        // GET: RoomPatients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomPatients roomPatients = db.RoomPatients.Find(id);
            if (roomPatients == null)
            {
                return HttpNotFound();
            }
            return View(roomPatients);
        }

        // POST: RoomPatients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoomId,PatinetId")] RoomPatients roomPatients)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomPatients).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roomPatients);
        }

        // GET: RoomPatients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomPatients roomPatients = db.RoomPatients.Find(id);
            if (roomPatients == null)
            {
                return HttpNotFound();
            }
            return View(roomPatients);
        }

        // POST: RoomPatients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoomPatients roomPatients = db.RoomPatients.Find(id);
            db.RoomPatients.Remove(roomPatients);
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
