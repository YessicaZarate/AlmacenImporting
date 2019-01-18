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

namespace AlmacenImporting.Controllers
{
    public class ProvidersController : Controller
    {
        private AlmacenImportingContext db = new AlmacenImportingContext();

        // GET: Providers
        public async Task<ActionResult> Index()
        {
            return View(await db.Providers.ToListAsync());
        }

        // GET: Providers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Providers providers = await db.Providers.FindAsync(id);
            if (providers == null)
            {
                return HttpNotFound();
            }
            return View(providers);
        }

        // GET: Providers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Providers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProvId,ProvName,Notes")] Providers providers)
        {
            if (ModelState.IsValid)
            {
                db.Providers.Add(providers);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(providers);
        }

        // GET: Providers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Providers providers = await db.Providers.FindAsync(id);
            if (providers == null)
            {
                return HttpNotFound();
            }
            return View(providers);
        }

        // POST: Providers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProvId,ProvName,Notes")] Providers providers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(providers).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(providers);
        }

        // GET: Providers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Providers providers = await db.Providers.FindAsync(id);
            if (providers == null)
            {
                return HttpNotFound();
            }
            return View(providers);
        }

        // POST: Providers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Providers providers = await db.Providers.FindAsync(id);
            db.Providers.Remove(providers);
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
