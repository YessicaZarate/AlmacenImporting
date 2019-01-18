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
using AlmacenImporting.ViewModels.Products;
using System.Collections;

namespace AlmacenImporting.Controllers
{
    public class ProductsController : Controller
    {
        //private AlmacenImportingContext db = new AlmacenImportingContext();

        private ProductsService _productsService;
        private ProvidersService _providersService;

        public ProductsController()
        {
            this._productsService = new ProductsService();
            this._providersService = new ProvidersService();
        }

        // GET: Products
        public async Task<ActionResult> Index()
        {
            IEnumerable<Products> products = await _productsService.GetAll();
            List<IndexProdVM> indexVMList = new List<IndexProdVM>();

            foreach (var prod in products)
            {
                IndexProdVM IndVM = new IndexProdVM()
                {
                    ProducId = prod.ProdId,
                    Item = prod.Item,
                    Brand = prod.Brand,
                    Description = prod.Description,
                    Qty = prod.Qty,
                    Cost = prod.Cost,
                    Price = prod.Price
                };

                indexVMList.Add(IndVM);
            }

            return View(indexVMList);
            //var products = db.Products.Include(p => p.Providers);
            //return View(await products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            DetailsProdVM detail = new DetailsProdVM();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await _productsService.Get(id.Value);
            if (products == null)
            {
                return HttpNotFound();
            }

            detail.ProducId = products.ProdId;
            detail.Item = products.Item;
            detail.Brand = products.Brand;
            detail.Description = products.Description;
            detail.Qty = products.Qty;
            detail.Cost = products.Cost;
            detail.Price = products.Price;
            detail.ProviderName = products.ProviderName;
            detail.Warranty = products.Warranty;
            detail.DateAd = products.DateAd;
            detail.DateCreated = products.DateCreated;
            detail.DateUpdated = products.DateUpdated;

            return View(detail);
        }

        // GET: Products/Create
        public async Task<ActionResult> Create()
        {
            CreateProductsVM model = new CreateProductsVM();

            var providers = await _providersService.GetAll();

            model.Providers = providers.Select(l => new SelectListItem()
            {
                Text = l.ProvName,
               Value = l.Id.ToString()
            });
            model.ProvidId = providers != null && providers.Any() ? providers.First().Id : 0;

            //ViewBag.ProvId = new SelectList(db.Providers, "ProvId", "ProvName");
            return View(model);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateProductsVM model)
        {
            if (ModelState.IsValid)
            {
                Products newprod = new Products()
                {
                    Item = model.Item,
                    Brand = model.Brand,
                    Description = model.Description,
                    Qty = model.Qty,
                    Cost = model.Cost,
                    Price = model.Price,
                    Warranty = model.Warranty,
                    DateAd = model.DateAd,
                    ProvId = model.ProvidId,
                    DateCreated = DateTimeOffset.Now
                };

                try
                {
                    await _productsService.Create(newprod);

                    TempData.Add("SuccessMsg", "The new product was created successfully!");
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

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            EditProdVM model = new EditProdVM();
            var providers = await _providersService.GetAll();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await _productsService.Get(id.Value);
            if (products == null)
            {
                return HttpNotFound();
            }
            model.ProducId = products.ProdId;
            model.Item = products.Item;
            model.Brand = products.Brand;
            model.Description = products.Description;
            model.Qty = products.Qty;
            model.Cost = products.Cost;
            model.Price = products.Price;
            model.Warranty = products.Warranty;
            model.DateAd = products.DateAd;


            model.Providers = providers.Select(l => new SelectListItem()
            {
                Text = l.ProvName,
                Value = l.Id.ToString()
            });
            model.ProvidId = providers != null && providers.Any() ? providers.First().Id : 0;

            model.ProvidId = products.ProvId;

            return View(model);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditProdVM model, int? id)
        {
            if (ModelState.IsValid)
            {
                Products existingProduct = await _productsService.Get(id.Value);
                if (existingProduct != null)
                {
                    existingProduct.Item = model.Item;
                    existingProduct.Brand = model.Brand;
                    existingProduct.Description = model.Description;
                    existingProduct.Qty = model.Qty;
                    existingProduct.Cost = model.Cost;
                    existingProduct.Price = model.Price;
                    existingProduct.Warranty = model.Warranty;
                    existingProduct.DateAd = model.DateAd;
                    existingProduct.DateUpdated = DateTimeOffset.Now;
                    existingProduct.ProvId = model.ProvidId;
                }
                else
                {
                    return HttpNotFound();
                }
                await _productsService.Update(existingProduct);
                return RedirectToAction("Index");
                //db.Entry(products).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                //return RedirectToAction("Index");
            }
            //ViewBag.ProvId = new SelectList(db.Providers, "ProvId", "ProvName", products.ProvId);
            return View(model);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            DeleteProdVM deleteVM = new DeleteProdVM();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await _productsService.Get(id.Value);
            if (products == null)
            {
                return HttpNotFound();
            }
            deleteVM.ProducId = products.ProdId;
            deleteVM.Item = products.Item;
            deleteVM.Brand = products.Brand;
            deleteVM.Description = products.Description;
            deleteVM.Qty = products.Qty;
            deleteVM.Cost = products.Cost;
            deleteVM.Price = products.Price;
            deleteVM.ProviderName = products.ProviderName;
            deleteVM.Warranty = products.Warranty;
            deleteVM.DateAd = products.DateAd;
            deleteVM.DateCreated = products.DateCreated;
            deleteVM.DateUpdated = products.DateUpdated;

            return View(deleteVM);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var products = await _productsService.Get(id);
            await _productsService.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _productsService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
