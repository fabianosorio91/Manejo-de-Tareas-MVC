namespace TareasMVC.Entidades
{
    public class Paso
    {
        public Guid Id { get; set; } //identificador global unico
        //relacion entre tarea y paso
        public int TareaId { get; set; }
        public Tarea Tarea { get; set; }//permite cargar la data del paso, como un inner Join
        public string Descripcion { get; set; }
        public bool Realizado { get; set; }
        public int Orden { get; set; }
    }
}
