﻿using System;
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
        private AlmacenImportingContext db = new AlmacenImportingContext();
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
                    Description = prod.Description,
                    Qty = prod.Qty,
                    Cost = prod.Cost,
                    Price = prod.Price,
                    ProvName = prod.ProviderName,
                    Warranty = prod.Warranty
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await db.Products.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
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
                    Description = model.Description,
                    Qty = model.Qty,
                    Cost = model.Cost,
                    Price = model.Price,
                    Warranty = model.Warranty,
                    DateAd = model.DateAd,
                    ProvId = model.ProvidId,
                    
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await db.Products.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProvId = new SelectList(db.Providers, "ProvId", "ProvName", products.ProvId);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProdId,Item,Description,Qty,Cost,Price,ProvId,Warranty,DateAd")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProvId = new SelectList(db.Providers, "ProvId", "ProvName", products.ProvId);
            return View(products);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await db.Products.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Products products = await db.Products.FindAsync(id);
            db.Products.Remove(products);
            await db.SaveChangesAsync();
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
