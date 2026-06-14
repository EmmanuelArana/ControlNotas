using System.Text.Json;
using ControlNotas.Common;

namespace ControlNotas.DataAccess
{
    public class EstudianteRepository
    {
        private readonly string _ruta;
        public EstudianteRepository()
        {
            //establecemos la ruta del archivo
            _ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "estudiante.json");
        }

        private List<Estudiante> Leer()
        {
            if (!File.Exists(_ruta)) // Si el archivo no existe
                return new List<Estudiante>(); //devolvemos una lista vacia de tipo tarea

            string json = File.ReadAllText(_ruta); // Si existe, leemos el archivo

            return JsonSerializer.Deserialize<List<Estudiante>>(json) // convertimos los datos a una lista de tareas(objeto)
                ?? new List<Estudiante>(); // pero si el archivo existe y el contenido es null (vacio) devolvemos una lista vacia
        }
        public Estudiante ObtenerPorId(int id)
        {
            var estudiante = Leer().FirstOrDefault(e => e.IdEstudiante == id);
            if (estudiante == null)
                throw new Exception($"Estudiante ID {id} no encontrado");
            return estudiante;
        }

        private void Guardar(List<Estudiante> estudiante)
        { // convertimos datos/objetos en formato json
            var opciones = new JsonSerializerOptions { WriteIndented = true }; //identamos el texto para que quede en vertical
            string json = JsonSerializer.Serialize(estudiante, opciones); // llamamos al serializador (convertidor)
            File.WriteAllText(_ruta, json); //escribimos todo el texto en el archivo
        }
        public List<Estudiante> ObtenerTodos()
        { // leemos el archivo y devolvemos la lista de tareas ordenadas segun criterio
            return Leer()
            .OrderBy(t => t.Estado)
            .ToList();
        }
        public void Insertar(Estudiante estudiante)
        { //insertar tarea nueva
            var lista = Leer();
            //verificamos si la lista tiene al menos un elemento con el metodo Any()
            // De ser asi: toma el elemento mas alto con Max, y le suma 1 para el nuevo ID, de lo contrario
            // Si la lista esta vacia, el id = 1
            estudiante.IdEstudiante = lista.Any() ? lista.Max(e => e.IdEstudiante) + 1 : 1;
            lista.Add(estudiante);
            Guardar(lista);
        }
        public void Actualizar(Estudiante estudiante)
        {
            var lista = Leer(); //cargar la lista existente en el archivo
            var existente = lista.FirstOrDefault(e => e.IdEstudiante == estudiante.IdEstudiante) //verificamos que la tarea exista y la guardamos en "existente"
                ?? throw new Exception($"Tarea ID {estudiante.IdEstudiante} no encontrada");

            // actualizamos los atributos de la tarea existente
            existente.Nombre = estudiante.Nombre;
            existente.Apellidos = estudiante.Apellidos;
            existente.Estado = estudiante.Estado;
            Guardar(lista); // llaamamos a Guardar, para escribir el archivo
        }
        public void Eliminar(int id)
        {
            var lista = Leer(); //cargar la lista existente en el archivo
            var estudiante = lista.FirstOrDefault(e => e.IdEstudiante == id) // comprobar que la tarea existe, buscando por id
                ?? throw new Exception($"Tarea ID {id} no encontrada");

            lista.Remove(estudiante); //quitamos la tarea de la lista
            Guardar(lista); // escribimos al archivo la tarea actualizada
        }
    }
}