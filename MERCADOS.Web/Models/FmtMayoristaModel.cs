using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MERCADOS.Web.Models
{
    [Table("DAT_INST_FMT_MAYORISTA")]
    public class FmtMayoristaModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_registro { get; set; }

        public int id_formato { get; set; }

        public int id_instancia { get; set; }

        [Required(ErrorMessage = "Nombre de especie es requerido")]
        [Display(Name = "Especie")]
        [Range(0, int.MaxValue, ErrorMessage = "Seleccione una especie válida")]
        public int id_especie { get; set; }

        [Required(ErrorMessage = "Nombre de estado es requerido")]
        [Display(Name = "Estado")]
        [Range(0, int.MaxValue, ErrorMessage = "Seleccione un estado válido")]
        public int id_estado { get; set; }

        [Required(ErrorMessage = "Nombre de presentación es requerido")]
        [Display(Name = "Presentación")]
        [Range(0, int.MaxValue, ErrorMessage = "Seleccione una presentación válida")]
        public int id_presentacion { get; set; }

        [Required(ErrorMessage = "Cantidad es requerido")]
        [Display(Name = "Cantidad")]
        [Range(0, int.MaxValue, ErrorMessage = "Ingrese un número válido")]
        public int cantidad { get; set; }

        [Required(ErrorMessage = "Unidad es requerido")]
        [Display(Name = "Unidad de medida")]
        [Range(0, int.MaxValue, ErrorMessage = "Seleccione una unidad válida")]
        public int id_unidad { get; set; }

        [Required(ErrorMessage = "Peso es requerido")]
        [Display(Name = "Peso Unidad de Medida (Kg)")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "Ingrese un peso válido")]
        public decimal peso_unidad { get; set; }

        [Required(ErrorMessage = "Volumen es requerido")]
        [Display(Name = "Volumen (Kg)")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "Ingrese un volumen válido")]
        public decimal volumen { get; set; }

        [Required(ErrorMessage = "Lugar de procedencia es requerido")]
        [Display(Name = "Procedencia")]
        [Range(0, int.MaxValue, ErrorMessage = "Seleccione un lugar de procedencia válido")]
        public int id_lugar { get; set; }

        [Required(ErrorMessage = "Tamaño es requerido")]
        [Display(Name = "Tamaño estimado (cm)")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "Ingrese un tamaño válido")]
        public decimal tamanio { get; set; }

        [Required(ErrorMessage = "Precio mínimo es requerido")]
        [Display(Name = "Precio mín. (S/.)")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "Ingrese un precio válido")]
        public decimal precio_min { get; set; }

        [Required(ErrorMessage = "Precio máximo es requerido")]
        [Display(Name = "Precio máx (S/.)")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "Ingrese un precio válido")]
        public decimal precio_max { get; set; }

        [Display(Name = "Observaciones")]
        public string observaciones { get; set; }

        public string usuario_creacion { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime fecha_creacion { get; set; }

        [Display(Name = "Usuario modificación")]
        public string usuario_modificacion { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha modificación")]
        public Nullable<System.DateTime> fecha_modificacion { get; set; }
    }

    [Table("VW_DETALLE_FMT_MAYORISTA")]    
    public class FmtMayoristaViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_registro { get; set; }

        public int id_formato { get; set; }

        public int id_instancia { get; set; }

        [Display(Name = "Nombre de especie")]
        public string nom_especie { get; set; }

        [Display(Name = "Volumen")]
        public decimal volumen { get; set; }

        [Display(Name = "Cantidad")]
        public int cantidad { get; set; }

        [Display(Name = "Unidad")]
        public string nom_unidad { get; set; }

        [Display(Name = "Lugar de procedencia")]
        public string nom_lugar { get; set; }

        [Display(Name = "Precio mínimo")]
        public decimal precio_min { get; set; }

        [Display(Name = "Precio máximo")]
        public decimal precio_max { get; set; }

        [Display(Name = "Tamaño")]
        public decimal tamanio { get; set; }

        [Display(Name = "Peso")]
        public decimal peso_unidad { get; set; }

        [Display(Name = "Presentación")]
        public string nom_presentacion { get; set; }

        [Display(Name = "Estado")]
        public string nom_estado { get; set; }

        [Display(Name = "Usuario modificación")]
        public string ult_usuario { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha modificación")]
        public System.DateTime ult_fecha { get; set; }

        [Display(Name = "Observaciones")]
        public string observaciones { get; set; }
    }

    public class FormatoPrincipalModel
    {
     public IQueryable<InstanciaViewModel> Cabecera { get; set; }

     public IEnumerable<FmtMayoristaViewModel> Detalle { get; set; }
    }
}