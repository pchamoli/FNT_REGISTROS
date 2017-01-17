using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MERCADOS.Web.Models
{
    [Table("DAT_CAMPO")]
    public class CampoModel
    {
        public CampoModel()
        {
            EsActivo = true;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_campo { get; set; }

        [Required(ErrorMessage = "Nombre de sección es requerido")]
        [Display(Name = "Nombre sección")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione sección válida")]
        public int id_seccion { get; set; }

        [Required(ErrorMessage = "Nombre de campo es requerido")]
        [Display(Name = "Nombre de campo")]
        public string nom_campo { get; set; }

        [UIHint("MultilineText")]
        [Display(Name = "Descripción de campo")]
        public string desc_campo { get; set; }

        [Display(Name = "Estado")]
        public string estado_campo { get; set; }

        [Display(Name = "Campo obligatorio?")]
        public string oblig_campo { get; set; }

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
            get { return estado_campo == "1"; }
            set { estado_campo = value ? "1" : "0"; }
        }

        [Display(Name = "Obligatorio")]
        [NotMapped]
        public bool EsObligatorio
        {
            get { return oblig_campo == "1"; }
            set { oblig_campo = value ? "1" : "0"; }
        }
    }

    [Table("VW_MAESTRO_CAMPO")]
    public class CampoViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_campo { get; set; }

        [Required]
        [Display(Name = "Nombre de formato")]
        public string nom_formato { get; set; }

        [Required]
        [Display(Name = "Nombre de sección")]
        public string nom_seccion { get; set; }

        [Required]
        [Display(Name = "Nombre de campo")]
        public string nom_campo { get; set; }

        [Required]
        [Display(Name = "Descripción de campo")]
        public string desc_campo { get; set; }

        [Required]
        [Display(Name = "Obligatorio")]
        public string obligatorio { get; set; }

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