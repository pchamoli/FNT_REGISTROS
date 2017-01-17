using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MERCADOS.Entidades;
using MERCADOS.Request;

namespace MERCADOS.ServiciosAplicacion.Recursos
{
    public static class RequestToEntidad
    {
        public static DAT_FORMATO FormatoRequestToEntidad(FormatoRequest  request)
        {
            if (request == null) return null;

            var entidad = new DAT_FORMATO
            {
                id_formato = request.id_formato,
                nom_formato = request.nom_formato,
                tipo_formato = request.tipo_formato,
                estado = request.estado,
                usuario_creacion = request.usuario_creacion,
                fecha_creacion = request.fecha_creacion,
                usuario_modificacion = request.usuario_modificacion,
                fecha_modificacion = request.fecha_modificacion
            };

            foreach (var item in request.seccion)
            {
                var nodo = new DAT_SECCION()
                {
                    id_formato = request.id_formato,
                    id_seccion = item.id_seccion ,
                    nom_seccion = item.nom_seccion,
                    orden_seccion = item.orden_seccion,
                    estado_seccion = item.estado_seccion,
                    usuario_creacion = item.usuario_creacion,
                    fecha_creacion = item.fecha_creacion,
                    usuario_modificacion = item.usuario_modificacion,
                    fecha_modificacion = item.fecha_modificacion
                };

                foreach (var item1 in item.campo)
                {
                    nodo.DAT_CAMPO.Add(new DAT_CAMPO()
                    {
                        id_seccion = item.id_seccion,
                        id_campo = item1.id_campo,
                        nom_campo = item1.nom_campo,
                        desc_campo = item1.desc_campo,
                        estado_campo = item1.estado_campo,
                        oblig_campo = item1.oblig_campo,
                        usuario_creacion = item.usuario_creacion,
                        fecha_creacion = item.fecha_creacion,
                        usuario_modificacion = item.usuario_modificacion,
                        fecha_modificacion = item.fecha_modificacion
                    });
                }
                entidad.DAT_SECCION.Add(nodo);
            }

            return entidad;
        }
    }
}
