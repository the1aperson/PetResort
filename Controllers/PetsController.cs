using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetResort.DATA;
using System.IO;
using System.Drawing;
using PetResort.Domain;
using PetResort.Domain.Repositories;
using PetResort.Domain.Services;
using log4net;
using Microsoft.AspNet.Identity;

namespace PetResort.UI.Controllers
{
    public class PetsController : Controller
    {
        private IUnitOfWork uow;
        public PetsController(IUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
        }
        //private PetResortEntities db = new PetResortEntities();

        // GET: Pets
        [Authorize]
        public ActionResult Index()
        {
            List<Pet> pets = new List<Pet>();

            //pets = uow.PetRepository.GetPets();

            if (User.IsInRole("Admin") || (User.IsInRole("Employee")))
            {
                pets = uow.PetRepository.GetPets();
            }
            else
            {
                string UserId = User.Identity.GetUserId();
                pets = uow.PetRepository.GetPets(UserId);

            }

            return View(pets);

            //return View(db.Pets.ToList());
        }

        // GET: Pets/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Pet pet = db.Pets.Find(id);
            Pet pet = uow.PetRepository.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        // GET: Pets/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.PetID = new SelectList(uow.PetRepository.Get(), "PetID", "Pet's Name");
            ViewBag.OwnerID = new SelectList(uow.PetRepository.Get(), "OwnerID", "OwnerID");
            return View();
        }

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // POST: Pets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "PetID,Name,OwnerID,PetPhoto,SpecialNotes,IsActive,DateAdded,TypeOfAnimal")] Pet pet, HttpPostedFileBase petImage)
        {
            if (ModelState.IsValid)
            {
                //db.Pets.Add(pet);
                //db.SaveChanges();
                //return RedirectToAction("Index");
                #region Image Upload

                string imageName = "no-image-found.jpg";
                if (petImage != null)
                {
                    string imgExt = Path.GetExtension(petImage.FileName).ToLower();

                    string[] allowedExtensions = { ".jpg", ".png", ".gif", ".jpeg" };

                    if (allowedExtensions.Contains(imgExt))
                    {
                        imageName = Guid.NewGuid().ToString() + imgExt;

                        string savePath = Server.MapPath("~/Content/Images/Pets/");

                        Image convertedImage = Image.FromStream(petImage.InputStream);

                        int maxImageSize = 600;

                        int maxThumbnailSize = 100;

                        ImageServices.ResizeImage(savePath, imageName, convertedImage, maxImageSize, maxThumbnailSize);
                    }
                    else
                    {
                        log.Info(User.Identity.Name + " tried to upload a pet photo " + imgExt + "when registering a pet.");
                    }
                    #endregion

                }

                pet.OwnerID = User.Identity.GetUserId();
                pet.IsActive = true;
                pet.DateAdded = DateTime.Now;
                pet.PetPhoto = imageName;
                uow.PetRepository.Add(pet);
                uow.Save();
                return RedirectToAction("Index");
            }

            return View(pet);
        }

        // GET: Pets/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Pet pet = db.Pets.Find(id);
            ViewBag.PetID = new SelectList(uow.PetRepository.Get(), "PetID", "Pet's Name");
            ViewBag.OwnerID = new SelectList(uow.PetRepository.Get(), "OwnerID", "OwnerID");

            Pet pet = uow.PetRepository.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "PetID,Name,OwnerID,PetPhoto,SpecialNotes,IsActive,DateAdded,TypeOfAnimal")] Pet pet, HttpPostedFileBase petImage)
        {

            if (ModelState.IsValid)
            {
                //db.Pets.Add(pet);
                //db.SaveChanges();
                //return RedirectToAction("Index");
                #region Image Upload

                string imageName = "no-image-found.jpg";
                if (petImage != null)
                {
                    string imgExt = Path.GetExtension(petImage.FileName).ToLower();

                    string[] allowedExtensions = { ".jpg", ".png", ".gif", ".jpeg" };

                    if (allowedExtensions.Contains(imgExt))
                    {
                        imageName = Guid.NewGuid().ToString() + imgExt;

                        string savePath = Server.MapPath("~/Content/Images/Pets/");

                        Image convertedImage = Image.FromStream(petImage.InputStream);

                        int maxImageSize = 600;

                        int maxThumbnailSize = 100;

                        ImageServices.ResizeImage(savePath, imageName, convertedImage, maxImageSize, maxThumbnailSize);
                    }
                    else
                    {
                        log.Info(User.Identity.Name + " tried to upload a pet photo " + imgExt + "when registering a pet.");
                    }
                    #endregion
                }
                //db.Entry(pet).State = EntityState.Modified;
                //db.SaveChanges();
                uow.PetRepository.Update(pet);
                pet.PetPhoto = imageName;
                uow.Save();
                return RedirectToAction("Index");


            }
            return View(pet);
        }

        // GET: Pets/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Pet pet = db.Pets.Find(id);

            Pet pet = uow.PetRepository.Find(id);
  
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            //Pet pet = db.Pets.Find(id);
            //db.Pets.Remove(pet);
            //db.SaveChanges();
                  Pet pet = uow.PetRepository.Find(id);

            string pathOfSavedImage = Server.MapPath("~/Content/Images/Pets/");
            Pet petPhotoDestroy = uow.PetRepository.Find(id);
            ImageServices.Delete(pathOfSavedImage, petPhotoDestroy.PetPhoto);


            uow.PetRepository.Remove(pet);
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
