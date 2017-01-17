using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MERCADOS.Web.Models
{
    public class SeccionModel
    {
        [Key]
        public int id_seccion { get; set; }

        public int id_formato { get; set; }

        [Display(Name = "Nombre sección")]
        public string nom_seccion { get; set; }

        [Display(Name = "Orden de sección")]
        public int orden_seccion { get; set; }

        [Display(Name = "Estado")]
        public string estado_seccion { get; set; }
        public string usuario_creacion { get; set; }
        public System.DateTime fecha_creacion { get; set; }
        public string usuario_modificacion { get; set; }
        public Nullable<System.DateTime> fecha_modificacion { get; set; }

        public CampoModel Campo { get; set; }
    }
}
