using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Prueba.Models;

public partial class Categoria
{
    public int IdCat { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }
    //[JsonIgnore]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
