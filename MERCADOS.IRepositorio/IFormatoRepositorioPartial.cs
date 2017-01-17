using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MERCADOS.Response;
using MERCADOS.Entidades;

namespace MERCADOS.IRepositorio
{
    public partial interface  IFormatoRepositorio
    {

        IEnumerable<FormatoResponse> ConsultarFormato(int pageIndex, int pageSize, string nom_formato);

        int ConsultarFormatoCount(string nom_formato);

        FormatoResponse GetFormatoById(int id_formato);

        new void Insertar(DAT_FORMATO formato);

        void Eliminar(FormatoResponse entidad_empresa);

        new void Actualizar(DAT_FORMATO formato);
    }
}
