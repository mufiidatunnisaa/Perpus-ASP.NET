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
    public class peminjamanController : Controller
    {
        private perpustakaan3Entities db = new perpustakaan3Entities();

        // GET: peminjaman
        public ActionResult Index()
        {
            var tabel_peminjaman = db.tabel_peminjaman.Include(t => t.tabel_buku).Include(t => t.tabel_pegawai).Include(t => t.tabel_peminjam);
            return View(tabel_peminjaman.ToList());
        }

        // GET: peminjaman/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tabel_peminjaman tabel_peminjaman = db.tabel_peminjaman.Find(id);
            if (tabel_peminjaman == null)
            {
                return HttpNotFound();
            }
            return View(tabel_peminjaman);
        }

        // GET: peminjaman/Create
        public ActionResult Create()
        {
            ViewBag.id_buku = new SelectList(db.tabel_buku, "id_buku", "nama_buku");
            ViewBag.id_pegawai = new SelectList(db.tabel_pegawai, "id_pegawai", "nama_pegawai");
            ViewBag.id_peminjam = new SelectList(db.tabel_peminjam, "id_peminjam", "nama_peminjam");
            
            return View();

        }

        // POST: peminjaman/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_peminjaman,id_peminjam,id_pegawai,id_buku,tanggal_peminjaman,tanggal_harus_kembali,status")] tabel_peminjaman tabel_peminjaman, tabel_buku tabel_buku)
        {
                if (ModelState.IsValid)
                {
                    db.tabel_peminjaman.Add(tabel_peminjaman);
                // Mengurangi Stock
                    tabel_buku buku = db.tabel_buku.Find(tabel_peminjaman.id_buku);
                    buku.stok_buku -= 1;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            //db.SaveChanges();
            ViewBag.id_buku = new SelectList(db.tabel_buku, "id_buku", "nama_buku", tabel_peminjaman.id_buku);
            ViewBag.id_pegawai = new SelectList(db.tabel_pegawai, "id_pegawai", "nama_pegawai", tabel_peminjaman.id_pegawai);
            ViewBag.id_peminjam = new SelectList(db.tabel_peminjam, "id_peminjam", "nama_peminjam", tabel_peminjaman.id_peminjam);
            return View(tabel_peminjaman);
        }

        // GET: peminjaman/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tabel_peminjaman tabel_peminjaman = db.tabel_peminjaman.Find(id);
            if (tabel_peminjaman == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_buku = new SelectList(db.tabel_buku, "id_buku", "nama_buku", tabel_peminjaman.id_buku);
            ViewBag.id_pegawai = new SelectList(db.tabel_pegawai, "id_pegawai", "nama_pegawai", tabel_peminjaman.id_pegawai);
            ViewBag.id_peminjam = new SelectList(db.tabel_peminjam, "id_peminjam", "nama_peminjam", tabel_peminjaman.id_peminjam);
            return View(tabel_peminjaman);
        }

        // POST: peminjaman/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_peminjaman,id_peminjam,id_pegawai,id_buku,tanggal_peminjaman,tanggal_harus_kembali,status")] tabel_peminjaman tabel_peminjaman)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tabel_peminjaman).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_buku = new SelectList(db.tabel_buku, "id_buku", "nama_buku", tabel_peminjaman.id_buku);
            ViewBag.id_pegawai = new SelectList(db.tabel_pegawai, "id_pegawai", "nama_pegawai", tabel_peminjaman.id_pegawai);
            ViewBag.id_peminjam = new SelectList(db.tabel_peminjam, "id_peminjam", "nama_peminjam", tabel_peminjaman.id_peminjam);
            return View(tabel_peminjaman);
        }

        // GET: peminjaman/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tabel_peminjaman tabel_peminjaman = db.tabel_peminjaman.Find(id);
            if (tabel_peminjaman == null)
            {
                return HttpNotFound();
            }
            return View(tabel_peminjaman);
        }

        // POST: peminjaman/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tabel_peminjaman tabel_peminjaman = db.tabel_peminjaman.Find(id);
            db.tabel_peminjaman.Remove(tabel_peminjaman);
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
