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
    public class pengembalianController : Controller
    {
        private perpustakaan3Entities db = new perpustakaan3Entities();

        // GET: pengembalian
        public ActionResult Index()
        {
            var tabel_pengembalian = db.tabel_pengembalian.Include(t => t.tabel_peminjaman);
            return View(tabel_pengembalian.ToList());
        }

        public ActionResult ErrorPage()
        {
            return View();
        }

        // GET: pengembalian/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tabel_pengembalian tabel_pengembalian = db.tabel_pengembalian.Find(id);
            if (tabel_pengembalian == null)
            {
                return HttpNotFound();
            }
            return View(tabel_pengembalian);
        }

        // GET: pengembalian/Create
        public ActionResult Create()
        {
            //ViewBag.id_peminjaman = new SelectList(db.tabel_peminjaman, "id_peminjaman", "id_peminjaman");
            ViewBag.id_peminjaman = new SelectList(db.tabel_peminjaman, "id_peminjaman", "id_peminjaman");

            //ViewBag.id_peminjaman2 = new SelectList(db.tabel_peminjaman, "tanggal_harus_kembali", "id_peminjaman");
            return View();
        }

        // POST: pengembalian/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_pengembalian,id_peminjaman, tanggal_kembali,denda")] tabel_pengembalian tabel_pengembalian, tabel_buku tabel_buku, tabel_peminjaman tabel_peminjaman)
        {
            try
            {
                var peminjaman = (from a in db.tabel_peminjaman where a.id_peminjaman == tabel_pengembalian.id_peminjaman && a.status == 1 select a).Single();
                if (peminjaman == null)
                {
                    db.tabel_pengembalian.Add(tabel_pengembalian);
                    //tabel_buku buku = db.tabel_buku.Find(tabel_peminjaman);
                    //buku.stok_buku += 1;
                    tabel_peminjaman status = db.tabel_peminjaman.Find(tabel_pengembalian.id_peminjaman);
                    status.status = 1;
                    tabel_peminjaman cari_buku = db.tabel_peminjaman.Find(tabel_pengembalian.id_peminjaman);
                    cari_buku.tabel_buku.stok_buku += 1;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                    ViewBag.id_peminjaman = new SelectList(db.tabel_peminjaman, "id_peminjaman", "id_peminjaman");
                    return View(tabel_pengembalian);
                }
                return View("ErrorPage");
            }
            catch (Exception e) {
                db.tabel_pengembalian.Add(tabel_pengembalian);
                //tabel_buku buku = db.tabel_buku.Find(tabel_peminjaman);
                //buku.stok_buku += 1;
                tabel_peminjaman status = db.tabel_peminjaman.Find(tabel_pengembalian.id_peminjaman);
                status.status = 1;
                tabel_peminjaman cari_buku = db.tabel_peminjaman.Find(tabel_pengembalian.id_peminjaman);
                cari_buku.tabel_buku.stok_buku += 1;
                db.SaveChanges();
                return RedirectToAction("Index");
                ViewBag.id_peminjaman = new SelectList(db.tabel_peminjaman, "id_peminjaman", "id_peminjaman");
                return View(tabel_pengembalian);
            }
            /*if (ModelState.IsValid)
            {
                db.tabel_pengembalian.Add(tabel_pengembalian);
                //tabel_buku buku = db.tabel_buku.Find(tabel_peminjaman);
                //buku.stok_buku += 1;
                tabel_peminjaman status = db.tabel_peminjaman.Find(tabel_pengembalian.id_peminjaman);
                status.status = 1;
                tabel_peminjaman cari_buku = db.tabel_peminjaman.Find(tabel_pengembalian.id_peminjaman);
                cari_buku.tabel_buku.stok_buku += 1;
                db.SaveChanges();
                return RedirectToAction("Index");
            }*/

            ViewBag.id_peminjaman = new SelectList(db.tabel_peminjaman, "id_peminjaman", "id_peminjaman");
            return View(tabel_pengembalian);
        }

        // GET: pengembalian/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tabel_pengembalian tabel_pengembalian = db.tabel_pengembalian.Find(id);
            if (tabel_pengembalian == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_peminjaman = new SelectList(db.tabel_peminjaman, "id_peminjaman", "id_peminjaman", tabel_pengembalian.id_peminjaman);
            return View(tabel_pengembalian);
        }

        // POST: pengembalian/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_pengembalian,id_peminjaman,tanggal_kembali,denda")] tabel_pengembalian tabel_pengembalian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tabel_pengembalian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_peminjaman = new SelectList(db.tabel_peminjaman, "id_peminjaman", "id_peminjaman", tabel_pengembalian.id_peminjaman);
            return View(tabel_pengembalian);
        }

        // GET: pengembalian/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tabel_pengembalian tabel_pengembalian = db.tabel_pengembalian.Find(id);
            if (tabel_pengembalian == null)
            {
                return HttpNotFound();
            }
            return View(tabel_pengembalian);
        }

        // POST: pengembalian/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tabel_pengembalian tabel_pengembalian = db.tabel_pengembalian.Find(id);
            db.tabel_pengembalian.Remove(tabel_pengembalian);
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
