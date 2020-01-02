using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Final.Models;

namespace Final.Controllers
{
    public class bukuController : Controller
    {
        private perpustakaan3Entities db = new perpustakaan3Entities();

        // GET: buku
        public ActionResult Index()
        {
            return View(db.tabel_buku.ToList());
        }

        // GET: buku/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tabel_buku tabel_buku = db.tabel_buku.Find(id);
            if (tabel_buku == null)
            {
                return HttpNotFound();
            }
            return View(tabel_buku);
        }

        // GET: buku/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: buku/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_buku,nama_buku,penerbit_buku,stok_buku")] tabel_buku tabel_buku)
        {
            if (ModelState.IsValid)
            {
                db.tabel_buku.Add(tabel_buku);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tabel_buku);
        }

        // GET: buku/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tabel_buku tabel_buku = db.tabel_buku.Find(id);
            if (tabel_buku == null)
            {
                return HttpNotFound();
            }
            return View(tabel_buku);
        }

        // POST: buku/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_buku,nama_buku,penerbit_buku,stok_buku")] tabel_buku tabel_buku)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tabel_buku).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tabel_buku);
        }

        // GET: buku/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tabel_buku tabel_buku = db.tabel_buku.Find(id);
            if (tabel_buku == null)
            {
                return HttpNotFound();
            }
            return View(tabel_buku);
        }

        // POST: buku/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tabel_buku tabel_buku = db.tabel_buku.Find(id);
            db.tabel_buku.Remove(tabel_buku);
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
