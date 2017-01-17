using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MERCADOS.Web.Models
{
    public class CampoModel
    {
        [Key]
        public int id_campo { get; set; }
        public int id_seccion { get; set; }

        [Display(Name = "Nombre de campo")]
        public string nom_campo { get; set; }

        [Display(Name = "Descripción de campo")]
        public string desc_campo { get; set; }

        [Display(Name = "Estado")]
        public string estado_campo { get; set; }

        [Display(Name = "Campo obligatorio?")]
        public string oblig_campo { get; set; }
        public string usuario_creacion { get; set; }
        public System.DateTime fecha_creacion { get; set; }
        public string usuario_modificacion { get; set; }
        public Nullable<System.DateTime> fecha_modificacion { get; set; }
    }
}