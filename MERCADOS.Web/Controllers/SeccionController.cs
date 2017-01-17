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
    public class SeccionController : Controller
    {
        private MERCADOSWebContext db = new MERCADOSWebContext();

        // GET: Seccion
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "nom_seccion" : "";
            ViewBag.Name1SortParm = sortOrder == "Nombre1" ? "nom_formato" : "Nombre1";
            ViewBag.OrdenSortParm = sortOrder == "Orden" ? "orden" : "Orden";
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

            var secciones = from s in db.SeccionViewModels
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                secciones = secciones.Where(s => s.nom_formato.Contains(searchString.ToUpper())
                                            || s.nom_seccion.Contains(searchString.ToUpper())
                                            || s.ult_usuario.Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "nom_seccion":
                    secciones = secciones.OrderByDescending(s => s.nom_seccion);
                    break;
                case "Nombre1":
                    secciones = secciones.OrderBy(s => s.nom_formato);
                    break;
                case "nom_formato":
                    secciones = secciones.OrderByDescending(s => s.nom_formato);
                    break;
                case "Orden":
                    secciones = secciones.OrderBy(s => s.orden);
                    break;
                case "orden":
                    secciones = secciones.OrderByDescending(s => s.orden);
                    break;
                case "Usuario":
                    secciones = secciones.OrderBy(s => s.ult_usuario);
                    break;
                case "ult_usuario":
                    secciones = secciones.OrderByDescending(s => s.ult_usuario);
                    break;
                case "Fecha":
                    secciones = secciones.OrderBy(s => s.ult_fecha);
                    break;
                case "ult_fecha":
                    secciones = secciones.OrderByDescending(s => s.ult_fecha);
                    break;
                case "Estado":
                    secciones = secciones.OrderBy(s => s.estado);
                    break;
                case "estado":
                    secciones = secciones.OrderByDescending(s => s.estado);
                    break;
                default:
                    secciones = secciones.OrderBy(s => s.nom_formato);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(secciones.ToPagedList(pageNumber, pageSize));
        }

        // GET: Seccion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeccionViewModel seccionModel = db.SeccionViewModels.Find(id);
            if (seccionModel == null)
            {
                return HttpNotFound();
            }
            return View(seccionModel);
        }

        public IEnumerable<SelectListItem> GetFormatoList()
        {
            var formatos = db.FormatoModels.Select(f => new SelectListItem
                                                    {
                                                        Value = f.id_formato.ToString(),
                                                        Text = f.nom_formato
                                                    });

            return DefaultFlavorItem.Concat(formatos);
        }

        public IEnumerable<SelectListItem> DefaultFlavorItem
        {
            get
            {
                return Enumerable.Repeat(new SelectListItem
                {
                    Value = "0",
                    Text = "Seleccione un formato"
                }, count: 1);
            }
        }

        // GET: Seccion/Create
        public ActionResult Create()
        {
            ViewBag.FormatoList = new SelectList(GetFormatoList(), "value", "text");
            return View();
        }

        // POST: Seccion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_seccion,id_formato,nom_seccion,orden_seccion,estado_seccion,usuario_creacion,fecha_creacion,usuario_modificacion,fecha_modificacion,EsActivo")] SeccionModel seccionModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    seccionModel.usuario_creacion = "RTELLO1";
                    seccionModel.fecha_creacion = DateTime.Now;
                    seccionModel.estado_seccion = (seccionModel.EsActivo ? "1" : "0");
                    seccionModel.nom_seccion = seccionModel.nom_seccion.ToUpper();

                    db.SeccionModels.Add(seccionModel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (SqlException e)
            {
                ModelState.AddModelError("", "No se pudo grabar la operación. Por favor contacte con el administrador: " + e.Message);
            }

            ViewBag.FormatoList = new SelectList(GetFormatoList(), "value", "text");
            return View(seccionModel);
        }

        // GET: Seccion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeccionModel seccionModel = db.SeccionModels.Find(id);
            if (seccionModel == null)
            {
                return HttpNotFound();
            }

            ViewBag.FormatoList = new SelectList(GetFormatoList(), "value", "text");
            return View(seccionModel);
        }

        // POST: Seccion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_seccion,id_formato,nom_seccion,orden_seccion,estado_seccion,usuario_creacion,fecha_creacion,usuario_modificacion,fecha_modificacion,EsActivo")] SeccionModel seccionModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    seccionModel.usuario_modificacion = "RTELLO1";
                    seccionModel.fecha_modificacion = DateTime.Now;
                    seccionModel.nom_seccion = seccionModel.nom_seccion.ToUpper();

                    db.Entry(seccionModel).State = EntityState.Modified;
                    db.Entry(seccionModel).Property("usuario_creacion").IsModified = false;
                    db.Entry(seccionModel).Property("fecha_creacion").IsModified = false;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (SqlException e)
            {
                ModelState.AddModelError("", "No se pudo grabar la operación. Por favor contacte con el administrador: " + e.Message);
            }

            ViewBag.FormatoList = new SelectList(GetFormatoList(), "value", "text");
            return View(seccionModel);
        }

        // GET: Seccion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeccionViewModel seccionModel = db.SeccionViewModels.Find(id);
            if (seccionModel == null)
            {
                return HttpNotFound();
            }
            return View(seccionModel);
        }

        // POST: Seccion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                SeccionModel seccionModel = db.SeccionModels.Find(id);
                db.SeccionModels.Remove(seccionModel);
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
