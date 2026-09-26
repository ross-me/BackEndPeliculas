using System;
using System.Collections.Generic;

namespace ApiCine.Model;

public partial class pelicula_salacine
{
    public int id_pelicula_sala { get; set; }

    public int id_sala_cine { get; set; }

    public DateOnly? fecha_publicacion { get; set; }

    public DateOnly? fecha_fin { get; set; }

    public int id_pelicula { get; set; }

    public virtual pelicula id_peliculaNavigation { get; set; } = null!;

    public virtual sala_cine id_sala_cineNavigation { get; set; } = null!;
}
