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
    public class FmtMinoristaController : Controller
    {
        private MERCADOSWebContext db = new MERCADOSWebContext();

        // GET: FmtMinorista
        public ActionResult Index(int? page, int formatoID, int instanciaID)
        {
            /*ViewBag.CurrentSort = sortOrder;
             ViewBag.EspecieSortParm = String.IsNullOrEmpty(sortOrder) ? "nom_especie" : "";
             ViewBag.VolumenSortParm = sortOrder == "Volumen" ? "volumen" : "Volumen";
             ViewBag.CantidadSortParm = sortOrder == "Cantidad" ? "cantidad" : "Cantidad";
             ViewBag.UnidadSortParm = sortOrder == "Unidad" ? "nom_unidad" : "Unidad";
             ViewBag.ProcedenciaSortParm = sortOrder == "Procedencia" ? "nom_lugar" : "Procedencia";
             ViewBag.PrecioMinSortParm = sortOrder == "PrecioMin" ? "precio_min" : "PrecioMin";
             ViewBag.PrecioMaxSortParm = sortOrder == "PrecioMax" ? "precio_max" : "PrecioMax";
             ViewBag.PresentacionSortParm = sortOrder == "Presentacion" ? "nom_presentacion" : "Presentacion";
             ViewBag.EstadoSortParm = sortOrder == "Estado" ? "nom_estado" : "Estado";*/

            if (page == null)
            {
                page = 1;
            }

            ViewBag.id_formato = formatoID;
            ViewBag.id_instancia = instanciaID;

            FormatoPrincipalModelMin vista = new FormatoPrincipalModelMin();

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            vista.Cabecera = (from i in db.InstanciaViewModels
                              where i.id_formato == formatoID &&
                                    i.id_instancia == instanciaID
                              select i).AsQueryable();

            vista.Detalle = (from d in db.FmtMinoristaViewModels
                             where d.id_formato == formatoID &&
                                   d.id_instancia == instanciaID
                             select d).ToList();

            /*switch (sortOrder)
            {
                case "nom_especie":
                    vista.Detalle = vista.Detalle.OrderByDescending(s => s.nom_especie);
                    break;
                case "Volumen":
                    vista.Detalle = vista.Detalle.OrderBy(s => s.volumen);
                    break;
                case "volumen":
                    vista.Detalle = vista.Detalle.OrderByDescending(s => s.volumen);
                    break;
                case "Cantidad":
                    vista.Detalle = vista.Detalle.OrderBy(s => s.cantidad);
                    break;
                case "cantidad":
                    vista.Detalle = vista.Detalle.OrderByDescending(s => s.cantidad);
                    break;
                case "Unidad":
                    vista.Detalle = vista.Detalle.OrderBy(s => s.nom_unidad);
                    break;
                case "nom_unidad":
                    vista.Detalle = vista.Detalle.OrderByDescending(s => s.nom_unidad);
                    break;
                case "Procedencia":
                    vista.Detalle = vista.Detalle.OrderBy(s => s.nom_lugar);
                    break;
                case "nom_lugar":
                    vista.Detalle = vista.Detalle.OrderByDescending(s => s.nom_lugar);
                    break;
                case "PrecioMin":
                    vista.Detalle = vista.Detalle.OrderBy(s => s.precio_min);
                    break;
                case "precio_min":
                    vista.Detalle = vista.Detalle.OrderByDescending(s => s.precio_min);
                    break;
                case "PrecioMax":
                    vista.Detalle = vista.Detalle.OrderBy(s => s.precio_max);
                    break;
                case "precio_max":
                    vista.Detalle = vista.Detalle.OrderByDescending(s => s.precio_max);
                    break;
                case "Presentacion":
                    vista.Detalle = vista.Detalle.OrderBy(s => s.nom_presentacion);
                    break;
                case "nom_presentacion":
                    vista.Detalle = vista.Detalle.OrderByDescending(s => s.nom_presentacion);
                    break;
                case "Estado":
                    vista.Detalle = vista.Detalle.OrderBy(s => s.nom_estado);
                    break;
                case "nom_estado":
                    vista.Detalle = vista.Detalle.OrderByDescending(s => s.nom_estado);
                    break;
                default:
                    vista.Detalle = vista.Detalle.OrderBy(s => s.nom_especie);
                    break;
            }*/

            ViewBag.TotalCountMinorista = vista.Detalle.Count();

            return View(vista);
        }

        // GET: FmtMinorista/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FmtMinoristaViewModel fmtMinoristaModel = db.FmtMinoristaViewModels.Find(id);
            if (fmtMinoristaModel == null)
            {
                return HttpNotFound();
            }
            return View(fmtMinoristaModel);
        }

        #region Utilitarios

        public IEnumerable<SelectListItem> GetEspecies()
        {
            var especies = db.EspecieModels.Select(f => new SelectListItem
            {
                Value = f.id_especie.ToString(),
                Text = f.nom_especie
            });

            return Enumerable.Repeat(new SelectListItem
            {
                Value = "0",
                Text = "Seleccione una especie"
            }, count: 1).Concat(especies);
        }

        public IEnumerable<SelectListItem> GetEstados()
        {
            var estados = db.EstadoModels.Select(f => new SelectListItem
            {
                Value = f.id_estado.ToString(),
                Text = f.nom_estado
            });

            return Enumerable.Repeat(new SelectListItem
            {
                Value = "0",
                Text = "Seleccione un estado"
            }, count: 1).Concat(estados);
        }

        public IEnumerable<SelectListItem> GetPresentaciones()
        {
            var presentaciones = db.PresentacionModels.Select(f => new SelectListItem
            {
                Value = f.id_presentacion.ToString(),
                Text = f.nom_presentacion
            });

            return Enumerable.Repeat(new SelectListItem
            {
                Value = "0",
                Text = "Seleccione una presentación"
            }, count: 1).Concat(presentaciones);
        }

        public IEnumerable<SelectListItem> GetUnidades()
        {
            var unidades = db.UnidadModels.Select(f => new SelectListItem
            {
                Value = f.id_unidad.ToString(),
                Text = f.nom_unidad
            });

            return Enumerable.Repeat(new SelectListItem
            {
                Value = "0",
                Text = "Seleccione una unidad"
            }, count: 1).Concat(unidades);
        }

        public IEnumerable<SelectListItem> GetProcedencia()
        {
            var lugares = db.LugarModels.Select(f => new SelectListItem
            {
                Value = f.id_lugar.ToString(),
                Text = f.nom_lugar
            });

            return Enumerable.Repeat(new SelectListItem
            {
                Value = "0",
                Text = "Seleccione un lugar"
            }, count: 1).Concat(lugares);
        }

        #endregion

        // GET: FmtMinorista/Create
        public ActionResult Create(int formatoID, int instanciaID)
        {
            ViewBag.EspecieList = new SelectList(GetEspecies(), "value", "text");
            ViewBag.EstadoList = new SelectList(GetEstados(), "value", "text");
            ViewBag.PresentacionList = new SelectList(GetPresentaciones(), "value", "text");
            ViewBag.UnidadList = new SelectList(GetUnidades(), "value", "text");
            ViewBag.ProcedenciaList = new SelectList(GetProcedencia(), "value", "text");

            ViewBag.id_formato = formatoID;
            ViewBag.id_instancia = instanciaID;

            FmtMinoristaModel fmt = new FmtMinoristaModel();
            fmt.id_formato = formatoID;
            fmt.id_instancia = instanciaID;

            return View(fmt);
        }

        // POST: FmtMinorista/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_registro,id_formato,id_instancia,id_especie,puesto,id_estado,id_presentacion,id_unidad,id_lugar,tamanio,precio_min,precio_max,observaciones,usuario_creacion,fecha_creacion,usuario_modificacion,fecha_modificacion")] FmtMinoristaModel fmtMinoristaModel, int formatoID, int instanciaID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    fmtMinoristaModel.usuario_creacion = "RTELLO1";
                    fmtMinoristaModel.fecha_creacion = DateTime.Now;
                    fmtMinoristaModel.id_formato = formatoID;
                    fmtMinoristaModel.id_instancia = instanciaID;
                    fmtMinoristaModel.observaciones = fmtMinoristaModel.observaciones?.ToUpper();

                    db.FmtMinoristaModels.Add(fmtMinoristaModel);
                    db.SaveChanges();

                    // redirigimos a la pantalla de listado de registros de formato
                    return RedirectToAction("Index", "FmtMinorista", new { formatoID = fmtMinoristaModel.id_formato, instanciaID = fmtMinoristaModel.id_instancia });
                }
            }
            catch (SqlException e)
            {
                ModelState.AddModelError("", "No se pudo grabar la operación. Por favor contacte con el administrador: " + e.Message);
            }

            ViewBag.EspecieList = new SelectList(GetEspecies(), "value", "text");
            ViewBag.EstadoList = new SelectList(GetEstados(), "value", "text");
            ViewBag.PresentacionList = new SelectList(GetPresentaciones(), "value", "text");
            ViewBag.UnidadList = new SelectList(GetUnidades(), "value", "text");
            ViewBag.ProcedenciaList = new SelectList(GetProcedencia(), "value", "text");

            ViewBag.id_formato = formatoID;
            ViewBag.id_instancia = instanciaID;

            return View(fmtMinoristaModel);
        }

        // GET: FmtMinorista/Edit/5
        public ActionResult Edit(int? id, int formatoID, int instanciaID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FmtMinoristaModel fmtMinoristaModel = db.FmtMinoristaModels.Find(id);
            if (fmtMinoristaModel == null)
            {
                return HttpNotFound();
            }

            ViewBag.EspecieList = new SelectList(GetEspecies(), "value", "text", fmtMinoristaModel.id_especie);
            ViewBag.EstadoList = new SelectList(GetEstados(), "value", "text", fmtMinoristaModel.id_estado);
            ViewBag.PresentacionList = new SelectList(GetPresentaciones(), "value", "text", fmtMinoristaModel.id_presentacion);
            ViewBag.UnidadList = new SelectList(GetUnidades(), "value", "text", fmtMinoristaModel.id_unidad);
            ViewBag.ProcedenciaList = new SelectList(GetProcedencia(), "value", "text", fmtMinoristaModel.id_lugar);

            ViewBag.id_formato = formatoID;
            ViewBag.id_instancia = instanciaID;

            return View(fmtMinoristaModel);
        }

        // POST: FmtMinorista/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_registro,id_formato,id_instancia,id_especie,puesto,id_estado,id_presentacion,id_unidad,id_lugar,tamanio,precio_min,precio_max,observaciones,usuario_creacion,fecha_creacion,usuario_modificacion,fecha_modificacion")] FmtMinoristaModel fmtMinoristaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    fmtMinoristaModel.usuario_modificacion = "RTELLO1";
                    fmtMinoristaModel.fecha_modificacion = DateTime.Now;
                    fmtMinoristaModel.observaciones = fmtMinoristaModel.observaciones?.ToUpper();

                    db.Entry(fmtMinoristaModel).State = EntityState.Modified;
                    db.Entry(fmtMinoristaModel).Property("usuario_creacion").IsModified = false;
                    db.Entry(fmtMinoristaModel).Property("fecha_creacion").IsModified = false;
                    db.Entry(fmtMinoristaModel).Property("id_formato").IsModified = false;
                    db.Entry(fmtMinoristaModel).Property("id_instancia").IsModified = false;

                    db.SaveChanges();
                    // redirigimos a la pantalla de listado de registros de formato
                    return RedirectToAction("Index", "FmtMinorista", new { formatoID = fmtMinoristaModel.id_formato, instanciaID = fmtMinoristaModel.id_instancia });
                }
            }
            catch (SqlException e)
            {
                ModelState.AddModelError("", "No se pudo grabar la operación. Por favor contacte con el administrador: " + e.Message);
            }

            ViewBag.EspecieList = new SelectList(GetEspecies(), "value", "text", fmtMinoristaModel.id_especie);
            ViewBag.EstadoList = new SelectList(GetEstados(), "value", "text", fmtMinoristaModel.id_estado);
            ViewBag.PresentacionList = new SelectList(GetPresentaciones(), "value", "text", fmtMinoristaModel.id_presentacion);
            ViewBag.UnidadList = new SelectList(GetUnidades(), "value", "text", fmtMinoristaModel.id_unidad);
            ViewBag.ProcedenciaList = new SelectList(GetProcedencia(), "value", "text", fmtMinoristaModel.id_lugar);
            return View(fmtMinoristaModel);
        }

        // GET: FmtMinorista/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FmtMinoristaViewModel fmtMinoristaModel = db.FmtMinoristaViewModels.Find(id);
            if (fmtMinoristaModel == null)
            {
                return HttpNotFound();
            }
            return View(fmtMinoristaModel);
        }

        // POST: FmtMinorista/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FmtMinoristaModel fmtMinoristaModel = db.FmtMinoristaModels.Find(id);
            db.FmtMinoristaModels.Remove(fmtMinoristaModel);
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
