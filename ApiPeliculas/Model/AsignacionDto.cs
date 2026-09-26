namespace ApiCine.Model
{
    public class AsignacionDto
    {
        public int id_pelicula { get; set; }
        public int id_sala_cine { get; set; }
        public DateOnly? fecha_publicacion { get; set; }
        public DateOnly? fecha_fin { get; set; }
    }
}
