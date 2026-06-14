using System;
using System.Collections.Generic;
using System.Text;
using ControlNotas.Common;
using ControlNotas.DataAccess;

namespace ControlNotas
{
    public class EstudianteService
    {
        private readonly EstudianteRepository _estudianteRepository;
        private readonly MateriaRepository _materiaRepository;

        public EstudianteService()
        {
            _estudianteRepository = new EstudianteRepository();
            _materiaRepository = new MateriaRepository();
        }

        public List<Estudiante> ObtenerTodos() => _estudianteRepository.ObtenerTodos();
        public void Guardar(Estudiante estudiante) // Creamos un metodo para guardar una tarea nueva o actualizar una existente
        {
            if (string.IsNullOrWhiteSpace(estudiante.Nombre)) // Validacion #1: el nombre debe tener datos, no puede ser vacio
                throw new BusinessException("El nombre del Estudiante es requerido", "ESTUDIANTE_NOMBRE_REQ");
            if (string.IsNullOrEmpty(estudiante.Apellidos)) // Validacion #2: el apellido debe contener datos, no puede ser vacio
                throw new BusinessException("El titulo debe contener al menos 3 caracteres", "ESTUDIANTE_APELLIDO_REQ");
            if (estudiante.IdEstudiante == 0)
                _estudianteRepository.Insertar(estudiante); // Si el ID de la tarea es 0, entonces agregamos el estudiante en la lista
            else
                _estudianteRepository.Actualizar(estudiante); // Si el ID de la tarea no es 0, actualizamos el estudiante
        }
        public void MarcarCompletada(int id, bool estado) // Metodo para marcar una tarea existente como completada
        {
            var todos = _estudianteRepository.ObtenerTodos(); //Definimos la variable "todas" que nos almacena la lista de las tareas existentes.
            var estudiantes = todos.FirstOrDefault(e => e.IdEstudiante == id) // almacenamos la tarea que coincida con el id por parametro
                ?? throw new BusinessException("Tarea no encontrada", "TAREA_NOT_FOUND"); // el operador ?? reemplaza al if(variable==null)
            estudiantes.Estado = estado; // Asignamos el valor True al atributo completada del objeto tarea
            _estudianteRepository.Actualizar(estudiantes); // Llamamos al metodo para actualizar el valor de la tarea
        }
        public Estudiante BuscarEstudiante(int id)
        {
            // El repositorio ya lanza una excepción interna si no lo encuentra
            return _estudianteRepository.ObtenerPorId(id);
        }

        // Modifica un estudiante validando primero si existe en el archivo.
        public bool ModificarEstudiante(Estudiante estudiante, out string mensaje)
        {
            try
            {
                //Intentamos buscar si el estudiante existe antes de actualizar
                var estudianteExistente = BuscarEstudiante(estudiante.IdEstudiante);

                // Si no lanzó excepción, procedemos a actualizar
                _estudianteRepository.Actualizar(estudiante);

                mensaje = "Estudiante modificado correctamente.";
                return true;
            }
            catch (Exception ex)
            {
                // Captura el mensaje de error si el ID no fue encontrado o si hubo error de archivo
                mensaje = $"No se pudo modificar el estudiante: {ex.Message}";
                return false;
            }
        }

        // Elimina un estudiante. Valida que no tenga materias asociadas.
        public bool EliminarEstudiante(int id, out string mensaje)
        {
            try
            {
                var estudianteExistente = BuscarEstudiante(id);

                // Usamos el método que creamos en MateriaRepository para ver si el alumno tiene materias asociadas
                //Hacer un metodo que permita eliminar estudiantes y materias asociadas, pero primero se eliminan las materias y luego el estudiante. De esta forma no quedan registros huérfanos.
                var materiasAsignadas = _materiaRepository.ObtenerPorEstudiante(id);

                if (materiasAsignadas != null && materiasAsignadas.Count > 0)
                {
                    mensaje = $"No se puede eliminar al estudiante '{estudianteExistente.Nombre}'. " +
                              $"Tiene {materiasAsignadas.Count} materia(s) registrada(s). " +
                              $"Elimine primero sus materias para evitar registros huérfanos.";
                    return false;
                }

                // 3. Si no tiene materias asignadas, se procede con la eliminación segura
                _estudianteRepository.Eliminar(id);

                mensaje = "Estudiante eliminado correctamente.";
                return true;
            }
            catch (Exception ex)
            {
                mensaje = $"No se pudo eliminar al estudiante: {ex.Message}";
                return false;
            }
        }
        public void Eliminar(int id) => _estudianteRepository.Eliminar(id); //Eliminamos la tarea.  El operador => nos reemplaza las llaves del metodo
    }
}
