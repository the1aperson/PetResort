using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetResort.DATA;
using PetResort.Domain.Repositories;

namespace PetResort.UI.Controllers
{
    public class ResortLocationsController : Controller
    {
        // private PetResortEntities db = new PetResortEntities();
        private IUnitOfWork uow;
        public ResortLocationsController(IUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
        }
        // GET: ResortLocations
        public ActionResult Index()
        {
            //return View(db.ResortLocations.ToList());
            return View(uow.ResortRepository.Get());
        }

        // GET: ResortLocations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ResortLocation resortLocation = db.ResortLocations.Find(id);
            ResortLocation resortLocation = uow.ResortRepository.Find(id);
            if (resortLocation == null)
            {
                return HttpNotFound();
            }
            return View(resortLocation);
        }

        // GET: ResortLocations/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.PetID = new SelectList(uow.PetRepository.Get(), "PetID", "Name");
            ViewBag.ReservationID = new SelectList(uow.ReservationRepository.Get(), "ReservationID", "ReservationID");

            return View();
        }

        // POST: ResortLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ResortLocationID,ResortName,Address,City,State,ZipCode,ReservationLimit")] ResortLocation resortLocation)
        {
            if (ModelState.IsValid)
            {
                //db.ResortLocations.Add(resortLocation);
                //db.SaveChanges();
                uow.ResortRepository.Add(resortLocation);
                uow.Save();
                return RedirectToAction("Index");
            }

            return View(resortLocation);
        }

        // GET: ResortLocations/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ResortLocation resortLocation = db.ResortLocations.Find(id);

            ViewBag.PetID = new SelectList(uow.PetRepository.Get(), "PetID", "Name");
            ViewBag.ReservationID = new SelectList(uow.ReservationRepository.Get(), "ReservationID", "ReservationID");

            ResortLocation resortLocation = uow.ResortRepository.Find(id);
            if (resortLocation == null)
            {
                return HttpNotFound();
            }
            return View(resortLocation);
        }

        // POST: ResortLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ResortLocationID,ResortName,Address,City,State,ZipCode,ReservationLimit")] ResortLocation resortLocation)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(resortLocation).State = System.Data.Entity.EntityState.Modified;
                //db.SaveChanges();
                uow.ResortRepository.Update(resortLocation);
                uow.Save();
                return RedirectToAction("Index");
            }
            return View(resortLocation);
        }

        // GET: ResortLocations/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ResortLocation resortLocation = db.ResortLocations.Find(id);
            ResortLocation resortLocation = uow.ResortRepository.Find(id);
            if (resortLocation == null)
            {
                return HttpNotFound();
            }
            return View(resortLocation);
        }

        // POST: ResortLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            //ResortLocation resortLocation = db.ResortLocations.Find(id);
            //db.ResortLocations.Remove(resortLocation);
            //db.SaveChanges();
            ResortLocation resortLocation = uow.ResortRepository.Find(id);
            uow.ResortRepository.Remove(resortLocation);
            uow.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                uow.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
