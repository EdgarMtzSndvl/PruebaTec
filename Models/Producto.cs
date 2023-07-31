using System;
using System.Collections.Generic;

namespace Prueba.Models;

public partial class Producto
{
    public int IdProd { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public int? IdCat { get; set; }

    public virtual Categoria? oCat { get; set; }
    
}
