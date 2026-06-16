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
            // Normalizar almacenamiento: eliminar duplicados históricos (por estudiante + nombre normalizado)
            try
            {
                NormalizeStorage();
            }
            catch
            {
                // si algo falla no bloqueamos la app; seguimos sin normalizar
            }
        }

        // Elimina duplicados históricos en el archivo de materias.
        private void NormalizeStorage()
        {
            if (!File.Exists(_ruta)) return;
            var all = Leer();
            var result = new List<Materia>();
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var m in all)
            {
                var key = m.IdEstudiante + "|" + (m.Nombre ?? string.Empty).Trim();
                if (!seen.Contains(key))
                {
                    seen.Add(key);
                    result.Add(m);
                }
            }

            if (result.Count != all.Count)
            {
                Guardar(result);
            }
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
            // Evitar insertar más de una materia con el mismo nombre para un mismo estudiante
            var nombreNormalizado = (materia.Nombre ?? string.Empty).Trim();
            var existePorNombre = lista.Any(m => m.IdEstudiante == materia.IdEstudiante
                                               && string.Equals((m.Nombre ?? string.Empty).Trim(), nombreNormalizado, StringComparison.OrdinalIgnoreCase));
            if (existePorNombre)
            {
                // Ya existe una materia con el mismo nombre para este estudiante: no insertar
                return;
            }
            // Verificamos si la lista tiene al menos un elemento con el método Any()
            // De ser así: toma el elemento más alto con Max, y le suma 1 para el nuevo ID, de lo contrario
            // Si la lista está vacía, el id = 1
            materia.IdMateria = lista.Any() ? lista.Max(m => m.IdMateria) + 1 : 1;
            lista.Add(materia);
            Guardar(lista);
            // Normalizar archivo tras insertar
            NormalizeStorage();
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
            // Normalizar archivo tras actualizar
            NormalizeStorage();
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
            var lista = Leer();
            var estudiantesMaterias = lista.Where(m => m.IdEstudiante == idEstudiante).ToList();

            // Eliminar duplicados existentes (mismo estudiante y mismo nombre normalizado),
            // conservando la primera ocurrencia
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var duplicates = new List<Materia>();
            foreach (var m in estudiantesMaterias)
            {
                var nombre = (m.Nombre ?? string.Empty).Trim();
                if (seen.Contains(nombre))
                {
                    duplicates.Add(m);
                }
                else
                {
                    seen.Add(nombre);
                }
            }

            if (duplicates.Count > 0)
            {
                // remover duplicados del listado general y guardar el archivo
                foreach (var d in duplicates)
                {
                    var idx = lista.FindIndex(x => x.IdMateria == d.IdMateria);
                    if (idx >= 0) lista.RemoveAt(idx);
                }
                Guardar(lista);
            }

            return lista.Where(m => m.IdEstudiante == idEstudiante).ToList();
        }
    }
}