using System.Text.Json;
using ControlNotas.Common;
namespace ControlNotas.DataAccess
{
    public class TareaRepository
    {
        private readonly string _ruta;
        public TareaRepository()
        {
            //establecemos la ruta del archivo
            _ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"tareas.json");
        }

        //Métodos privados de lectura/escritura
        //CRUD
        private List<Tarea> Leer()
        {
            if (!File.Exists(_ruta)) // Si el archivo no existe
                return new List<Tarea>(); //devolvemos una lista vacia de tipo tarea

            string json = File.ReadAllText(_ruta); // Si existe, leemos el archivo

            return JsonSerializer.Deserialize<List<Tarea>>(json) // convertimos los datos a una lista de tareas(objeto)
                ?? new List<Tarea>(); // pero si el archivo existe y el contenido es null (vacio) devolvemos una lista vacia
        }
        private void Guardar(List<Tarea> tareas)
        { // convertimos datos/objetos en formato json
            var opciones = new JsonSerializerOptions { WriteIndented = true }; //identamos el texto para que quede en vertical
            string json = JsonSerializer.Serialize(tareas, opciones); // llamamos al serializador (convertidor)
            File.WriteAllText(_ruta, json); //escribimos todo el texto en el archivo
        }

        //Métodos públicos (misma forma que antes)
        public List<Tarea> ObtenerTodas()
        { // leemos el archivo y devolvemos la lista de tareas ordenadas segun criterio
            return Leer()
            .OrderBy(t => t.Completada)
            .ThenBy(t => t.FechaLimite)
            .ToList();
        }
        public void Insertar(Tarea tarea)
        { //insertar tarea nueva
            var lista = Leer();
            //verificamos si la lista tiene al menos un elemento con el metodo Any()
            // De ser asi: toma el elemento mas alto con Max, y le suma 1 para el nuevo ID, de lo contrario
            // Si la lista esta vacia, el id = 1
            tarea.Id = lista.Any() ? lista.Max(t => t.Id) + 1 : 1;
            tarea.FechaCreacion = DateTime.Now;
            lista.Add(tarea);
            Guardar(lista);
        }
        public void Actualizar(Tarea tarea)
        {
            var lista = Leer(); //cargar la lista existente en el archivo
            var existente = lista.FirstOrDefault(t => t.Id == tarea.Id) //verificamos que la tarea exista y la guardamos en "existente"
                ?? throw new Exception($"Tarea ID {tarea.Id} no encontrada");

            // actualizamos los atributos de la tarea existente
            existente.Titulo = tarea.Titulo;
            existente.Descripcion = tarea.Descripcion;
            existente.Prioridad = tarea.Prioridad;
            existente.FechaLimite = tarea.FechaLimite;
            existente.Completada = tarea.Completada;
            Guardar(lista); // llaamamos a Guardar, para escribir el archivo
        }
        public void Eliminar(int id)
        {
            var lista = Leer(); //cargar la lista existente en el archivo
            var tarea = lista.FirstOrDefault(t => t.Id == id) // comprobar que la tarea existe, buscando por id
                ?? throw new Exception($"Tarea ID {id} no encontrada");
            
            lista.Remove(tarea); //quitamos la tarea de la lista
            Guardar(lista); // escribimos al archivo la tarea actualizada
        }
    }
}