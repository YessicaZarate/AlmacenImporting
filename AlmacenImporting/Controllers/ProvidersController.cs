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
using AlmacenImporting.ViewModels.Providers;
using System.Collections;


namespace AlmacenImporting.Controllers
{
    public class ProvidersController : Controller
    {
        private AlmacenImportingContext db = new AlmacenImportingContext();

        private ProductsService _productsService;
        private ProvidersService _providersService;

        public ProvidersController()
        {
            this._productsService = new ProductsService();
            this._providersService = new ProvidersService();
        }

        // GET: Providers
        public async Task<ActionResult> Index()
        {
            IEnumerable<Providers> products = await _providersService.GetAll();
            List<IndexVM> indexVMList = new List<IndexVM>();

            foreach (var prov in products)
            {
                IndexVM IndVM = new IndexVM()
                {
                  Id = prov.Id,
                  ProvName = prov.ProvName,
                  Notes = prov.Notes
                };

                indexVMList.Add(IndVM);
            }

            return View(indexVMList);
        }

        // GET: Providers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            DetailsProvVM detail = new DetailsProvVM();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Providers providers = await _providersService.Get(id.Value);
            if (providers == null)
            {
                return HttpNotFound();
            }

            detail.Id = providers.Id;
            detail.ProvName = providers.ProvName;
            detail.Notes = providers.Notes;
            detail.DateCreated = providers.DateCreated;
            detail.DateUpdated = providers.DateUpdated;

            return View(detail);
        }

        // GET: Providers/Create
        public ActionResult Create()
        {
            CreateProvVM model = new CreateProvVM();
            //ViewBag.ProvId = new SelectList(db.Providers, "ProvId", "ProvName");
            return View(model);
        }

        // POST: Providers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateProvVM model)
        {
            if (ModelState.IsValid)
            {
                Providers newprov = new Providers()
                {
                    ProvName = model.ProvName,
                    Notes = model.Notes,
                    DateCreated = DateTimeOffset.Now
                };

                try
                {
                    await _providersService.Create(newprov);

                    TempData.Add("SuccessMsg", "The new provider was created successfully!");
                }
                catch (Exception ex)
                {
                    // Add message to the user
                    Console.WriteLine("An error has occurred. Message: " + ex.ToString());
                    throw;
                }
                return RedirectToAction("Index");
                //db.Products.Add(products);
                //await db.SaveChangesAsync();
                //return RedirectToAction("Index");
            }

            //ViewBag.ProvId = new SelectList(db.Providers, "ProvId", "ProvName", products.ProvId);
            return View(model);
        }

        // GET: Providers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            EditProvVM model = new EditProvVM();
            Providers providers = await _providersService.Get(id.Value);


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (providers == null)
            {
                return HttpNotFound();
            }
            model.Id = providers.Id;
            model.ProvName = providers.ProvName;
            model.Notes = providers.Notes;
            
            return View(model);
        }

        // POST: Providers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditProvVM model, int? id)
        {
            if (ModelState.IsValid)
            {
                Providers existingProvider = await _providersService.Get(id.Value);
                if (existingProvider != null)
                {
                    existingProvider.ProvName = model.ProvName;
                    existingProvider.Notes = model.Notes;
                    existingProvider.DateUpdated = DateTimeOffset.Now;
                }
                else
                {
                    return HttpNotFound();
                }
                await _providersService.Update(existingProvider);
                return RedirectToAction("Index");
               
            }
 
            return View(model);
        }

        // GET: Providers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            DeleteProvVM deleteVM = new DeleteProvVM();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Providers providers = await _providersService.Get(id.Value);
            if (providers == null)
            {
                return HttpNotFound();
            }
            deleteVM.Id = providers.Id;
            deleteVM.ProvName = providers.ProvName;
            deleteVM.Notes = providers.Notes;
            deleteVM.DateCreated = providers.DateCreated;
            deleteVM.DateUpdated = providers.DateUpdated;

            return View(deleteVM);
        }

        // POST: Providers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var providers = await _providersService.Get(id);
            await _providersService.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _providersService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
