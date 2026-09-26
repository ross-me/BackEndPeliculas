using System;
using System.Collections.Generic;

namespace ApiCine.Model;

public partial class pelicula
{
    public int id_pelicula { get; set; }

    public string nombre { get; set; } = null!;

    public int duracion { get; set; }

    public bool activo { get; set; }

    public virtual ICollection<pelicula_salacine> pelicula_salacines { get; set; } = new List<pelicula_salacine>();
}
