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
    public class peminjamController : Controller
    {
        private perpustakaan3Entities db = new perpustakaan3Entities();

        // GET: peminjam
        public ActionResult Index()
        {
            return View(db.tabel_peminjam.ToList());
        }

        // GET: peminjam/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tabel_peminjam tabel_peminjam = db.tabel_peminjam.Find(id);
            if (tabel_peminjam == null)
            {
                return HttpNotFound();
            }
            return View(tabel_peminjam);
        }

        // GET: peminjam/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: peminjam/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_peminjam,nama_peminjam,alamat_peminjam,password_peminjam")] tabel_peminjam tabel_peminjam)
        {
            if (ModelState.IsValid)
            {
                db.tabel_peminjam.Add(tabel_peminjam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tabel_peminjam);
        }

        // GET: peminjam/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tabel_peminjam tabel_peminjam = db.tabel_peminjam.Find(id);
            if (tabel_peminjam == null)
            {
                return HttpNotFound();
            }
            return View(tabel_peminjam);
        }

        // POST: peminjam/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_peminjam,nama_peminjam,alamat_peminjam,password_peminjam")] tabel_peminjam tabel_peminjam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tabel_peminjam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tabel_peminjam);
        }

        // GET: peminjam/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tabel_peminjam tabel_peminjam = db.tabel_peminjam.Find(id);
            if (tabel_peminjam == null)
            {
                return HttpNotFound();
            }
            return View(tabel_peminjam);
        }

        // POST: peminjam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tabel_peminjam tabel_peminjam = db.tabel_peminjam.Find(id);
            db.tabel_peminjam.Remove(tabel_peminjam);
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
