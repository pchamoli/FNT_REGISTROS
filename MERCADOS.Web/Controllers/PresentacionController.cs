using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MERCADOS.Web.Models;
using System.Data.SqlClient;
using PagedList;

namespace MERCADOS.Web.Controllers
{
    public class PresentacionController : Controller
    {
        private MERCADOSWebContext db = new MERCADOSWebContext();

        // GET: Presentacion
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "nom_presentacion" : "";
            ViewBag.UsuarioSortParm = sortOrder == "Usuario" ? "ult_usuario" : "Usuario";
            ViewBag.FechaSortParm = sortOrder == "Fecha" ? "ult_fecha" : "Fecha";
            ViewBag.EstadoSortParm = sortOrder == "Estado" ? "estado" : "Estado";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var presentaciones = from s in db.PresentacionViewModels
                                 select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                presentaciones = presentaciones.Where(s => s.nom_presentacion.Contains(searchString)
                                            || s.ult_usuario.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nom_presentacion":
                    presentaciones = presentaciones.OrderByDescending(s => s.nom_presentacion);
                    break;
                case "Usuario":
                    presentaciones = presentaciones.OrderBy(s => s.ult_usuario);
                    break;
                case "ult_usuario":
                    presentaciones = presentaciones.OrderByDescending(s => s.ult_usuario);
                    break;
                case "Fecha":
                    presentaciones = presentaciones.OrderBy(s => s.ult_fecha);
                    break;
                case "ult_fecha":
                    presentaciones = presentaciones.OrderByDescending(s => s.ult_fecha);
                    break;
                case "Estado":
                    presentaciones = presentaciones.OrderBy(s => s.estado);
                    break;
                case "estado":
                    presentaciones = presentaciones.OrderByDescending(s => s.estado);
                    break;
                default:
                    presentaciones = presentaciones.OrderBy(s => s.nom_presentacion);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(presentaciones.ToPagedList(pageNumber, pageSize));
        }

        // GET: Presentacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresentacionViewModel presentacionViewModel = db.PresentacionViewModels.Find(id);
            if (presentacionViewModel == null)
            {
                return HttpNotFound();
            }
            return View(presentacionViewModel);
        }

        // GET: Presentacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Presentacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_presentacion,nom_presentacion,estado,usuario_creacion,fecha_creacion,EsActivo")] PresentacionModel presentacionModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    presentacionModel.usuario_creacion = "RTELLO1";
                    presentacionModel.fecha_creacion = DateTime.Now;
                    presentacionModel.estado = (presentacionModel.EsActivo ? "1" : "0");

                    db.PresentacionModels.Add(presentacionModel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (SqlException e)
            {
                ModelState.AddModelError("", "No se pudo grabar la operación. Por favor contacte con el administrador: " + e.Message);
            }

            return View(presentacionModel);
        }

        // GET: Presentacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresentacionModel presentacionModel = db.PresentacionModels.Find(id);
            if (presentacionModel == null)
            {
                return HttpNotFound();
            }
            return View(presentacionModel);
        }

        // POST: Presentacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_presentacion,nom_presentacion,usuario_creacion,usuario_modificacion,fecha_creacion,fecha_modificacion,estado,EsActivo")] PresentacionModel presentacionModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    presentacionModel.usuario_modificacion = "RTELLO1";
                    presentacionModel.fecha_modificacion = DateTime.Now;

                    db.Entry(presentacionModel).State = EntityState.Modified;
                    db.Entry(presentacionModel).Property("usuario_creacion").IsModified = false;
                    db.Entry(presentacionModel).Property("fecha_creacion").IsModified = false;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (SqlException e)
            {
                ModelState.AddModelError(e.Message, "No se pudo grabar la operación. Por favor contacte con el administrador: " + e.Message);
            }
            return View(presentacionModel);
        }

        // GET: Presentacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresentacionViewModel presentacionViewModel = db.PresentacionViewModels.Find(id);
            if (presentacionViewModel == null)
            {
                return HttpNotFound();
            }
            return View(presentacionViewModel);
        }

        // POST: Presentacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                PresentacionModel presentacionModel = db.PresentacionModels.Find(id);
                db.PresentacionModels.Remove(presentacionModel);
                db.SaveChanges();
            }
            catch (SqlException e)
            {
                ModelState.AddModelError("", "No se pudo grabar la operación. Por favor contacte con el administrador: " + e.Message);
            }
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
