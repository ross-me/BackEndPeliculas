using System;
using System.Collections.Generic;

namespace ApiCine.Model;

public partial class sala_cine
{
    public int id_sala { get; set; }

    public string nombre { get; set; } = null!;

    public bool estado { get; set; }

    public virtual ICollection<pelicula_salacine> pelicula_salacines { get; set; } = new List<pelicula_salacine>();
}
