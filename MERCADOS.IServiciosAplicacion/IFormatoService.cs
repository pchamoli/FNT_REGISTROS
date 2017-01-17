using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using MERCADOS.Request;
using MERCADOS.Response;

namespace MERCADOS.IServiciosAplicacion
{
    [ServiceContract]
    public interface IFormatoService
    {
        [OperationContract]
        IEnumerable<FormatoResponse> ConsultarFormato(int pageIndex, int pageSize, FormatoRequest request);

        [OperationContract]
        int ConsultarFormatoCount(FormatoRequest request);

        [OperationContract]
        FormatoResponse GetFormatoById(int id_formato);

        [OperationContract]
        void GuardarNuevoFormato(FormatoRequest request);

        [OperationContract]
        void ActualizarFormato(FormatoRequest request);

        [OperationContract]
        void EliminarFormato(int id_formato);
    }
}
