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
using Microsoft.AspNet.Identity;

namespace PetResort.UI.Controllers
{
    public class ReservationsController : Controller
    {
        //private PetResortEntities db = new PetResortEntities();

        private IUnitOfWork uow;
        public ReservationsController(IUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
        }
        // GET: Reservations
        public ActionResult Index()
        {
            //var reservations = db.Reservations.Include(r => r.Pet).Include(r => r.ResortLocation);
            List<Reservation> reservations = new List<Reservation>();

            //reservations = uow.ReservationRepository.GetReservations();

            if (User.IsInRole("Admin") || (User.IsInRole("Employee")))
            {
                reservations = uow.ReservationRepository.GetReservations();
            }
            else
            {
                string UserId = User.Identity.GetUserId();
                reservations = uow.ReservationRepository.GetReservations(UserId);
            }

            return View(reservations);
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Reservation reservation = db.Reservations.Find(id);
            Reservation reservation = uow.ReservationRepository.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        [Authorize]
        public ActionResult Create()
        {
            if (User.IsInRole("Admin") || (User.IsInRole("Employee")))
            {
                ViewBag.PetID = new SelectList(uow.PetRepository.Get(), "PetID", "Name");
            }
            else
            {
                string UserId = User.Identity.GetUserId();
                ViewBag.PetID = new SelectList(uow.PetRepository.GetPets(UserId), "PetID", "Name");
            }

            ViewBag.ResortLocationID = new SelectList(uow.ResortRepository.Get(), "ResortLocationID", "ResortName");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ReservationID,ResortLocationID,PetID,ReservationDate")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                //db.Reservations.Add(reservation);
                //db.SaveChanges();
                uow.ReservationRepository.Add(reservation);
                uow.Save();
                return RedirectToAction("Index");
            }

            ViewBag.PetID = new SelectList(uow.PetRepository.Get(), "PetID", "Name", reservation.PetID);
            ViewBag.ResortLocationID = new SelectList(uow.ResortRepository.Get(), "ResortLocationID", "ResortName", reservation.ResortLocationID);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Reservation reservation = db.Reservations.Find(id);
            Reservation reservation = uow.ReservationRepository.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole("Admin") || (User.IsInRole("Employee")))
            {
                ViewBag.PetID = new SelectList(uow.PetRepository.Get(), "PetID", "Name", reservation.PetID);
            }
            else
            {
                string UserId = User.Identity.GetUserId();
                ViewBag.PetID = new SelectList(uow.PetRepository.GetPets(UserId), "PetID", "Name", reservation.PetID);
            }

            ViewBag.ResortLocationID = new SelectList(uow.ResortRepository.Get(), "ResortLocationID", "ResortName", reservation.ResortLocationID);
            return View(reservation);
        }
        

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ReservationID,ResortLocationID,PetID,ReservationDate")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(reservation).State = System.Data.Entity.EntityState.Modified;
                //db.SaveChanges();

                uow.ReservationRepository.Update(reservation);
                uow.Save();
                return RedirectToAction("Index");
            }
            //ViewBag.PetID = new SelectList(db.Pets, "PetID", "Name", reservation.PetID);
            //ViewBag.ResortLocationID = new SelectList(db.ResortLocations, "ResortLocationID", "ResortName", reservation.ResortLocationID);
            return RedirectToAction("Index");
        }

        // GET: Reservations/Delete/5
        [Authorize(Roles = "Admin , Employee")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Reservation reservation = db.Reservations.Find(id);
            Reservation reservation = uow.ReservationRepository.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin , Employee")]
        public ActionResult DeleteConfirmed(int id)
        {
            //Reservation reservation = db.Reservations.Find(id);
            //db.Reservations.Remove(reservation);
            //db.SaveChanges();

            Reservation reservation = uow.ReservationRepository.Find(id);
            uow.ReservationRepository.Remove(reservation);
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
