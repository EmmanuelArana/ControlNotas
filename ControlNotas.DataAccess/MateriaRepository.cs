using System.Text.Json;
using ControlNotas.Common;

namespace ControlNotas.DataAccess
{
    public class MateriaRepository
    {
        private readonly string _ruta;

        public MateriaRepository()
        {
            // Establecemos la ruta del archivo para materias
            _ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "materia.json");
        }

        private List<Materia> Leer()
        {
            if (!File.Exists(_ruta)) // Si el archivo no existe
                return new List<Materia>(); // Devolvemos una lista vacía de tipo Materia

            string json = File.ReadAllText(_ruta); // Si existe, leemos el archivo

            return JsonSerializer.Deserialize<List<Materia>>(json) // Convertimos los datos a una lista de materias (objeto)
                ?? new List<Materia>(); // Pero si el archivo existe y el contenido es null (vacío) devolvemos una lista vacía
        }

        public Materia ObtenerPorId(int id)
        {
            var materia = Leer().FirstOrDefault(m => m.IdMateria == id);
            if (materia == null)
                throw new Exception($"Materia ID {id} no encontrada");
            return materia;
        }

        private void Guardar(List<Materia> materias)
        { // Convertimos datos/objetos en formato json
            var opciones = new JsonSerializerOptions { WriteIndented = true }; // Indentamos el texto para que quede en vertical
            string json = JsonSerializer.Serialize(materias, opciones); // Llamamos al serializador (convertidor)
            File.WriteAllText(_ruta, json); // Escribimos todo el texto en el archivo
        }

        public List<Materia> ObtenerTodas()
        { // Leemos el archivo y devolvemos la lista de materias ordenadas por nombre
            return Leer()
                .OrderBy(m => m.Nombre)
                .ToList();
        }

        public void Insertar(Materia materia)
        { // Insertar materia nueva
            var lista = Leer();
            // Verificamos si la lista tiene al menos un elemento con el método Any()
            // De ser así: toma el elemento más alto con Max, y le suma 1 para el nuevo ID, de lo contrario
            // Si la lista está vacía, el id = 1
            materia.IdMateria = lista.Any() ? lista.Max(m => m.IdMateria) + 1 : 1;
            lista.Add(materia);
            Guardar(lista);
        }

        public void Actualizar(Materia materia)
        {
            var lista = Leer(); // Cargar la lista existente en el archivo
            var existente = lista.FirstOrDefault(m => m.IdMateria == materia.IdMateria) // Verificamos que la materia exista
                ?? throw new Exception($"Materia ID {materia.IdMateria} no encontrada");

            // Actualizamos los atributos de la materia existente con tus nuevas propiedades
            existente.IdEstudiante = materia.IdEstudiante;
            existente.Nombre = materia.Nombre;
            existente.Parcial1 = materia.Parcial1;
            existente.Parcial2 = materia.Parcial2;
            existente.Parcial3 = materia.Parcial3;

            Guardar(lista); // Llamamos a Guardar, para escribir el archivo
        }

        public void Eliminar(int id)
        {
            var lista = Leer(); // Cargar la lista existente en el archivo
            var materia = lista.FirstOrDefault(m => m.IdMateria == id) // Comprobar que la materia existe, buscando por id
                ?? throw new Exception($"Materia ID {id} no encontrada");

            lista.Remove(materia); // Quitamos la materia de la lista
            Guardar(lista); // Escribimos al archivo la lista actualizada
        }

        /// Método extra útil: Permite obtener todas las materias que le pertenecen a un estudiante en específico.
        public List<Materia> ObtenerPorEstudiante(int idEstudiante)
        {
            return Leer().Where(m => m.IdEstudiante == idEstudiante).ToList();
        }
    }
}