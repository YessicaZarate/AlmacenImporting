using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AlmacenImporting.Models;
using AlmacenImporting.Services;
using AlmacenImporting.ViewModels.Locations;

namespace AlmacenImporting.Controllers
{
    public class LocationsController : Controller
    {
        //private AlmacenImportingContext db = new AlmacenImportingContext();

        private LocationService _locationService;

        public LocationsController()
        {
            this._locationService = new LocationService();
        }

        // GET: Locations
        public async Task<ActionResult> Index()
        {
            IEnumerable<Locations> products = await _locationService.GetAll();
            List<IndexLocVM> indexVMList = new List<IndexLocVM>();

            foreach (var loc in products)
            {
                IndexLocVM IndVM = new IndexLocVM()
                {
                    Id = loc.Id,
                    LocationName = loc.LocName,
                    Notes = loc.Notes
                };

                indexVMList.Add(IndVM);
            }

            return View(indexVMList);
        }

        // GET: Locations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            DetailsLocVM detail = new DetailsLocVM();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locations locations = await _locationService.Get(id.Value);
            if (locations == null)
            {
                return HttpNotFound();
            }

            detail.Id = locations.Id;
            detail.LocationsName = locations.LocName;
            detail.Notes = locations.Notes;
            detail.DateCreated = locations.DateCreated;
            detail.DateUpdated = locations.DateUpdated;

            return View(detail);
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            CreateLocVM model = new CreateLocVM();
            //ViewBag.ProvId = new SelectList(db.Providers, "ProvId", "ProvName");
            return View(model);
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLocVM model)
        {
            if (ModelState.IsValid)
            {
                Locations newloc = new Locations()
                {
                    LocName = model.LocationName,
                    Notes = model.Notes,
                    DateCreated = DateTimeOffset.Now
                };

                try
                {
                    await _locationService.Create(newloc);

                    TempData.Add("SuccessMsg", "The new provider was created successfully!");
                }
                catch (Exception ex)
                {
                    // Add message to the user
                    Console.WriteLine("An error has occurred. Message: " + ex.ToString());
                    throw;
                }
                return RedirectToAction("Index");
            }
            
            return View(model);
        }

        // GET: Locations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            EditLocVM model = new EditLocVM();
            Locations locations = await _locationService.Get(id.Value);


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (locations == null)
            {
                return HttpNotFound();
            }
            model.Id = locations.Id;
            model.LocationName = locations.LocName;
            model.Notes = locations.Notes;

            return View(model);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditLocVM model, int? id)
        {
            if (ModelState.IsValid)
            {
                Locations existingLocation = await _locationService.Get(id.Value);
                if (existingLocation != null)
                {
                    existingLocation.LocName = model.LocationName;
                    existingLocation.Notes = model.Notes;
                    existingLocation.DateUpdated = DateTimeOffset.Now;
                }
                else
                {
                    return HttpNotFound();
                }
                await _locationService.Update(existingLocation);
                return RedirectToAction("Index");

            }

            return View(model);
        }

        // GET: Locations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            DeleteLocVM deleteVM = new DeleteLocVM();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locations providers = await _locationService.Get(id.Value);
            if (providers == null)
            {
                return HttpNotFound();
            }
            deleteVM.Id = providers.Id;
            deleteVM.LocationsName = providers.LocName;
            deleteVM.Notes = providers.Notes;
            deleteVM.DateCreated = providers.DateCreated;
            deleteVM.DateUpdated = providers.DateUpdated;

            return View(deleteVM);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var locations = await _locationService.Get(id);
            await _locationService.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _locationService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
