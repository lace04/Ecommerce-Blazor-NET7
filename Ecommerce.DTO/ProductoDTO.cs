using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
  public class ProductoDTO
  {
    public int IdProducto { get; set; }
    [Required(ErrorMessage = "Ingrese nombre del producto")]
    public string? Nombre { get; set; }
    [Required(ErrorMessage = "Ingrese descripcion del producto")]
    public string? Descripcion { get; set; }
    public int? IdCategoria { get; set; }
    [Required(ErrorMessage = "Ingrese precio del producto")]
    public decimal? Precio { get; set; }
    [Required(ErrorMessage = "Ingrese precio de oferta del producto")]
    public decimal? PrecioOferta { get; set; }
    [Required(ErrorMessage = "Ingrese cantidad del producto")]
    public int? Cantidad { get; set; }
    [Required(ErrorMessage = "Ingrese imagen del producto")]
    public string? Imagen { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public virtual CategoriaDTO? IdCategoriaNavigation { get; set; }
  }
}
