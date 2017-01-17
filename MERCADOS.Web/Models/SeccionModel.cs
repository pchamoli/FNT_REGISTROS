using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MERCADOS.Web.Models
{
    [Table("DAT_SECCION")]
    public class SeccionModel
    {
        public SeccionModel()
        {
            EsActivo = true;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_seccion { get; set; }

        [Required(ErrorMessage = "Nombre de formato es requerido")]
        [Display(Name = "Nombre formato")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione un formato válido")]
        public int id_formato { get; set; }

        [Required(ErrorMessage = "Nombre de sección es requerido")]
        [Display(Name = "Nombre sección")]
        public string nom_seccion { get; set; }

        [Required(ErrorMessage = "Número de orden es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El número de orden debe ser mayor que 0")]
        [Display(Name = "Orden de sección")]
        public int orden_seccion { get; set; }

        [Display(Name = "Estado")]
        public string estado_seccion { get; set; }

        public string usuario_creacion { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime fecha_creacion { get; set; }

        [Display(Name = "Usuario modificación")]
        public string usuario_modificacion { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha modificación")]
        public Nullable<System.DateTime> fecha_modificacion { get; set; }
        
        [Display(Name = "Estado")]
        [NotMapped]
        public bool EsActivo
        {
            get { return estado_seccion == "1"; }
            set { estado_seccion = value ? "1" : "0"; }
        }
    }

    [Table("VW_MAESTRO_SECCION")]
    public class SeccionViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_seccion { get; set; }

        [Required]
        [Display(Name = "Nombre de formato")]
        public string nom_formato { get; set; }

        [Required]
        [Display(Name = "Nombre de sección")]
        public string nom_seccion { get; set; }

        [Required]
        [Display(Name = "Orden de sección")]
        public int orden { get; set; }

        [Display(Name = "Usuario modificación")]
        public string ult_usuario { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha modificación")]
        public System.DateTime ult_fecha { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public string estado { get; set; }
    }
}
