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
    public class EstadoController : Controller
    {
        private MERCADOSWebContext db = new MERCADOSWebContext();

        // GET: Estado
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "nom_estado" : "";
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

            var estados = from s in db.EstadoViewModels
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                estados = estados.Where(s => s.nom_estado.Contains(searchString.ToUpper())
                                            || s.ult_usuario.Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "nom_estado":
                    estados = estados.OrderByDescending(s => s.nom_estado);
                    break;
                case "Usuario":
                    estados = estados.OrderBy(s => s.ult_usuario);
                    break;
                case "ult_usuario":
                    estados = estados.OrderByDescending(s => s.ult_usuario);
                    break;
                case "Fecha":
                    estados = estados.OrderBy(s => s.ult_fecha);
                    break;
                case "ult_fecha":
                    estados = estados.OrderByDescending(s => s.ult_fecha);
                    break;
                case "Estado":
                    estados = estados.OrderBy(s => s.estado);
                    break;
                case "estado":
                    estados = estados.OrderByDescending(s => s.estado);
                    break;
                default:
                    estados = estados.OrderBy(s => s.nom_estado);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(estados.ToPagedList(pageNumber, pageSize));
        }

        // GET: Estado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoViewModel estadoModel = db.EstadoViewModels.Find(id);
            if (estadoModel == null)
            {
                return HttpNotFound();
            }
            return View(estadoModel);
        }

        // GET: Estado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_estado,nom_estado,usuario_creacion,fecha_creacion,usuario_modificacion,fecha_modificacion,estado,EsActivo")] EstadoModel estadoModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    estadoModel.usuario_creacion = "RTELLO1";
                    estadoModel.fecha_creacion = DateTime.Now;
                    estadoModel.estado = (estadoModel.EsActivo ? "1" : "0");
                    estadoModel.nom_estado = estadoModel.nom_estado.ToUpper();

                    db.EstadoModels.Add(estadoModel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (SqlException e)
            {
                ModelState.AddModelError("", "No se pudo grabar la operación. Por favor contacte con el administrador: " + e.Message);
            }

            return View(estadoModel);
        }

        // GET: Estado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoModel estadoModel = db.EstadoModels.Find(id);
            if (estadoModel == null)
            {
                return HttpNotFound();
            }
            return View(estadoModel);
        }

        // POST: Estado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_estado,nom_estado,usuario_creacion,fecha_creacion,usuario_modificacion,fecha_modificacion,estado,EsActivo")] EstadoModel estadoModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    estadoModel.usuario_modificacion = "RTELLO1";
                    estadoModel.fecha_modificacion = DateTime.Now;
                    estadoModel.nom_estado = estadoModel.nom_estado.ToUpper();

                    db.Entry(estadoModel).State = EntityState.Modified;
                    db.Entry(estadoModel).Property("usuario_creacion").IsModified = false;
                    db.Entry(estadoModel).Property("fecha_creacion").IsModified = false;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (SqlException e)
            {
                ModelState.AddModelError("", "No se pudo grabar la operación. Por favor contacte con el administrador: " + e.Message);
            }
            return View(estadoModel);
        }

        // GET: Estado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoViewModel estadoModel = db.EstadoViewModels.Find(id);
            if (estadoModel == null)
            {
                return HttpNotFound();
            }
            return View(estadoModel);
        }

        // POST: Estado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                EstadoModel estadoModel = db.EstadoModels.Find(id);
                db.EstadoModels.Remove(estadoModel);
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
