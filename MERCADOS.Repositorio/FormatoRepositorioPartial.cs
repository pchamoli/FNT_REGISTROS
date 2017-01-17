using MERCADOS.IRepositorio;
using MERCADOS.Response;
using MERCADOS.Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace MERCADOS.Repositorio
{
    public partial class FormatoRepositorio : IFormatoRepositorio
    {
        public IEnumerable<FormatoResponse> ConsultarFormato(int pageIndex, int pageSize, string nom_formato)
        {
            DB_REGISTROSEntities _dataContext = base.Context.GetContext() as DB_REGISTROSEntities;

            var result = (from c in _dataContext.DAT_FORMATO
                          where (string.IsNullOrEmpty(nom_formato) ? true : c.nom_formato.ToLower().Contains(nom_formato.ToLower()))
                          select new FormatoResponse 
                          {
                              id_formato = c.id_formato,
                              tipo_formato = c.tipo_formato,
                              nom_formato = c.nom_formato,
                              usuario_creacion = c.usuario_creacion,
                              fecha_creacion = c.fecha_creacion,
                              usuario_modificacion = c.usuario_modificacion, 
                              fecha_modificacion = c.fecha_modificacion 
                          }).OrderByDescending(i => i.id_formato).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();

            return result;
        }

        public int ConsultarFormatoCount(string nombre_formato)
        {
            return ConsultarFormato(1, int.MaxValue, nombre_formato).Count();
        }
        
        public FormatoResponse GetFormatoById(int id_formato)
        {
            DB_REGISTROSEntities _dataContext = base.Context.GetContext() as DB_REGISTROSEntities;
            var result = (from c in _dataContext.DAT_FORMATO
                          where c.id_formato == id_formato 
                          select new FormatoResponse 
                          {
                              id_formato = c.id_formato,
                              tipo_formato = c.tipo_formato,
                              nom_formato = c.nom_formato,
                              usuario_creacion = c.usuario_creacion,
                              fecha_creacion = c.fecha_creacion,
                              usuario_modificacion = c.usuario_modificacion, 
                              fecha_modificacion = c.fecha_modificacion 
                          }).First();

            return result;
        }

        public void Eliminar(FormatoResponse entidad_empresa)
        {
            DB_REGISTROSEntities _dataContext = base.Context.GetContext() as DB_REGISTROSEntities;

            ObjectParameter param1 = new ObjectParameter("cod_error", 0);
            ObjectParameter param2 = new ObjectParameter("msg_error", "");

            _dataContext.p_DAT_FORMATO_Eliminar(entidad_empresa.id_formato,
                                                param1,
                                                param2);
        }

        public new void Actualizar(DAT_FORMATO formato)
        {
            DB_REGISTROSEntities _dataContext = base.Context.GetContext() as DB_REGISTROSEntities;

            ObjectParameter param1 = new ObjectParameter("cod_error", 0);
            ObjectParameter param2 = new ObjectParameter("msg_error", "");

            _dataContext.p_DAT_FORMATO_Procesar(1, // actualizar formato
                                                formato.id_formato,
                                                formato.tipo_formato,
                                                formato.nom_formato,
                                                formato.estado,
                                                formato.usuario_creacion,
                                                param1,
                                                param2);
        }

        public new void Insertar(DAT_FORMATO formato)
        {
            DB_REGISTROSEntities _dataContext = base.Context.GetContext() as DB_REGISTROSEntities;

            ObjectParameter param1 = new ObjectParameter("cod_error", 0);
            ObjectParameter param2 = new ObjectParameter("msg_error", "");
            
            _dataContext.p_DAT_FORMATO_Procesar(0,  // crear formato
                                                formato.id_formato,
                                                formato.tipo_formato,
                                                formato.nom_formato,
                                                formato.estado,
                                                formato.usuario_creacion,
                                                param1,
                                                param2);
        }
    }
}
