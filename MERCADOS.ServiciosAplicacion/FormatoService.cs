using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MERCADOS.IServiciosAplicacion;
using MERCADOS.IRepositorio;
using MERCADOS.Request;
using MERCADOS.Response;
using MERCADOS.Entidades;
using System.Transactions;
using MERCADOS.ServiciosAplicacion.Recursos;

namespace MERCADOS.ServiciosAplicacion
{
    public class FormatoService : IFormatoService 
    {
        private readonly IFormatoRepositorio _formato;
        private readonly IUnitOfWork _unitOfWork;

        public FormatoService(IFormatoRepositorio formato, IUnitOfWork unitOfWork)
        {
            _formato = formato;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<FormatoResponse> ConsultarFormato(int pageIndex, int pageSize, FormatoRequest request)
        {
            if (pageIndex <= 0)
                throw new InvalidOperationException(Mensajes.error_IndexPageZero);
            if (pageSize <= 0)
                throw new InvalidOperationException(Mensajes.error_PageSizeZero);
            if (request == null)
                throw new ArgumentNullException("request");

            return _formato.ConsultarFormato(pageIndex, pageSize, request.nom_formato);
        }

        public int ConsultarFormatoCount(FormatoRequest request)
        {
            return _formato.ConsultarFormatoCount(request.nom_formato);
        }

        public FormatoResponse GetFormatoById(int id_formato)
        {
            return _formato.GetFormatoById(id_formato);
        }

        public void GuardarNuevoFormato(FormatoRequest request)
        {
            try
            {

                using (TransactionScope scope = new TransactionScope())
                {

                    DAT_FORMATO formato = RequestToEntidad.FormatoRequestToEntidad(request);
                    _formato.Insertar(formato);
                    _unitOfWork.Guardar();
                    request.id_formato = formato.id_formato;
                    scope.Complete();

                }
            }
            catch
            {
                throw new InvalidOperationException();
            }
        }

        public void ActualizarFormato(FormatoRequest request)
        {
            DAT_FORMATO formato = RequestToEntidad.FormatoRequestToEntidad(request);
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    if (formato != null)
                    {
                        _formato.Actualizar(formato);
                        _unitOfWork.Guardar();
                    }

                    scope.Complete();
                }
            }
            catch
            {
                throw new InvalidOperationException();
            }
        }

        public void EliminarFormato(int id_formato)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var entidad_empresa = _formato.GetFormatoById(id_formato);
                    if (entidad_empresa != null)
                    {
                        _formato.Eliminar(entidad_empresa);
                        _unitOfWork.Guardar();

                    }
                    scope.Complete();
                }
            }
            catch
            {
                throw new InvalidOperationException();
            }
        }
    }
}
