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
    public class FormatoController : Controller
    {
        private MERCADOSWebContext db = new MERCADOSWebContext();

        // GET: Formato
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "nom_formato" : "";
            ViewBag.TipoSortParm = sortOrder == "Tipo" ? "tipo_formato" : "Tipo";
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

            var formatos = from s in db.FormatoViewModels
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                formatos = formatos.Where(s => s.nom_formato.Contains(searchString.ToUpper())
                                            || s.ult_usuario.Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "nom_formato":
                    formatos = formatos.OrderByDescending(s => s.nom_formato);
                    break;
                case "Tipo":
                    formatos = formatos.OrderBy(s => s.tipo_formato);
                    break;
                case "tipo_formato":
                    formatos = formatos.OrderByDescending(s => s.tipo_formato);
                    break;
                case "Usuario":
                    formatos = formatos.OrderBy(s => s.ult_usuario);
                    break;
                case "ult_usuario":
                    formatos = formatos.OrderByDescending(s => s.ult_usuario);
                    break;
                case "Fecha":
                    formatos = formatos.OrderBy(s => s.ult_fecha);
                    break;
                case "ult_fecha":
                    formatos = formatos.OrderByDescending(s => s.ult_fecha);
                    break;
                case "Estado":
                    formatos = formatos.OrderBy(s => s.estado);
                    break;
                case "estado":
                    formatos = formatos.OrderByDescending(s => s.estado);
                    break;
                default:
                    formatos = formatos.OrderBy(s => s.nom_formato);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(formatos.ToPagedList(pageNumber, pageSize));
        }

        // GET: Formato/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormatoViewModel formatoModel = db.FormatoViewModels.Find(id);
            if (formatoModel == null)
            {
                return HttpNotFound();
            }
            return View(formatoModel);
        }

        // GET: Formato/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Formato/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_formato,tipo_formato,nom_formato,usuario_creacion,fecha_creacion,estado,EsActivo")] FormatoModel formatoModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    formatoModel.usuario_creacion = "RTELLO1";
                    formatoModel.fecha_creacion = DateTime.Now;
                    formatoModel.estado = (formatoModel.EsActivo ? "1" : "0");
                    formatoModel.nom_formato = formatoModel.nom_formato.ToUpper();
                    
                    db.FormatoModels.Add(formatoModel);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (SqlException e)
            {
                ModelState.AddModelError("", "No se pudo grabar la operación. Por favor contacte con el administrador: " + e.Message);
            }
            return View(formatoModel);
        }

        // GET: Formato/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormatoModel formatoModel = db.FormatoModels.Find(id);
            if (formatoModel == null)
            {
                return HttpNotFound();
            }
            return View(formatoModel);
        }

        // POST: Formato/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_formato,tipo_formato,nom_formato,usuario_modificacion,fecha_modificacion,estado,EsActivo")] FormatoModel formatoModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    formatoModel.usuario_modificacion = "RTELLO1";
                    formatoModel.fecha_modificacion = DateTime.Now;
                    formatoModel.nom_formato = formatoModel.nom_formato.ToUpper();

                    db.Entry(formatoModel).State = EntityState.Modified;
                    db.Entry(formatoModel).Property("usuario_creacion").IsModified = false;
                    db.Entry(formatoModel).Property("fecha_creacion").IsModified = false;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (SqlException e)
            {
                ModelState.AddModelError("", "No se pudo grabar la operación. Por favor contacte con el administrador: " + e.Message);
            }
            return View(formatoModel);
        }

        // GET: Formato/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormatoViewModel formatoModel = db.FormatoViewModels.Find(id);
            if (formatoModel == null)
            {
                return HttpNotFound();
            }
            return View(formatoModel);
        }

        // POST: Formato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                FormatoModel formatoModel = db.FormatoModels.Find(id);
                db.FormatoModels.Remove(formatoModel);
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
