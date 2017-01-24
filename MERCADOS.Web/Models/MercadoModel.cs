using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MERCADOS.Web.Models
{
    [Table("DAT_MERCADO")]
    public class MercadoModel
    {
        public MercadoModel()
        {
            EsActivo = true;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_mercado { get; set; }

        [Required(ErrorMessage = "Nombre de mercado es requerido")]
        [Display(Name = "Nombre de mercado")]
        public string nom_mercado { get; set; }

        [UIHint("MultilineText")]
        [Display(Name = "Descripción de mercado")]
        public string desc_mercado { get; set; }

        [Required(ErrorMessage = "Departamento es requerido")]
        [Display(Name = "Departamento")]
        public string dpto_mercado { get; set; }

        [Required(ErrorMessage = "Provincia es requerido")]
        [Display(Name = "Provincia")]
        public string prov_mercado { get; set; }

        [Required(ErrorMessage = "Distrito es requerido")]
        [Display(Name = "Distrito")]
        public string distrito_mercado { get; set; }

        [Display(Name = "Tipo de mercado")]
        public int tipo_mercado { get; set; }

        [Display(Name = "Estado")]
        public string estado_mercado { get; set; }

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
            get { return estado_mercado == "1"; }
            set { estado_mercado = value ? "1" : "0"; }
        }
    }

    [Table("VW_MAESTRO_MERCADO")]
    public class MercadoViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_mercado { get; set; }

        [Required]
        [Display(Name = "Nombre de mercado")]
        public string nom_mercado { get; set; }

        [Required]
        [Display(Name = "Descripción de mercado")]
        public string desc_mercado { get; set; }

        [Required]
        [Display(Name = "Departamento")]
        public string departamento { get; set; }

        [Required]
        [Display(Name = "Provincia")]
        public string provincia { get; set; }

        [Required]
        [Display(Name = "Distrito")]
        public string distrito { get; set; }

        [Display(Name = "Usuario modificación")]
        public string ult_usuario { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha modificación")]
        public System.DateTime ult_fecha { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public string estado { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public string tipo { get; set; }
    }
}