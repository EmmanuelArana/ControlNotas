using System;
using System.Collections.Generic;
using ControlNotas.Common;
using ControlNotas.DataAccess;

namespace ControlNotas
{
    public class MateriaService
    {
        private readonly MateriaRepository _materiaRepository;
        private readonly EstudianteRepository _estudianteRepository;

        private const double NOTA_MINIMA = 1.0;
        private const double NOTA_MAXIMA = 100.0;

        public MateriaService()
        {
            _materiaRepository = new MateriaRepository();
            _estudianteRepository = new EstudianteRepository();
        }

        // Método de apoyo que valida rango de notas.
        private void ValidarNotas(Materia materia)
        {
            if (materia.Parcial1 < NOTA_MINIMA || materia.Parcial1 > NOTA_MAXIMA ||
                materia.Parcial2 < NOTA_MINIMA || materia.Parcial2 > NOTA_MAXIMA ||
                materia.Parcial3 < NOTA_MINIMA || materia.Parcial3 > NOTA_MAXIMA)
            {
                throw new BusinessException(
                    $"Las notas parciales deben estar estrictamente en el rango de {NOTA_MINIMA} a {NOTA_MAXIMA}.",
                    "NOTA_FUERA_DE_RANGO"
                );
            }
        }

        // Asigna una nueva materia a un estudiante. 
        public void AsignarMateriaAEstudiante(int idEstudiante, Materia materia)
        {
            //Validar que el estudiante exista en el sistema
            try
            {
                _estudianteRepository.ObtenerPorId(idEstudiante);
            }
            catch (Exception)
            {
                throw new BusinessException($"El estudiante con ID {idEstudiante} no existe.", "ESTUDIANTE_NO_ENCONTRADO");
            }

            // El rango de las notas asignadas
            ValidarNotas(materia);

            // Vincular el ID del estudiante y guardar
            materia.IdEstudiante = idEstudiante;
            _materiaRepository.Insertar(materia);
        }

        // Obtiene las materias de un estudiante. Si el ID es inválido, no procesa la búsqueda.
        public List<Materia> ListarMateriasDeEstudiante(int idEstudiante)
        {
            if (idEstudiante <= 0)
            {
                return new List<Materia>();
            }

            return _materiaRepository.ObtenerPorEstudiante(idEstudiante);
        }

        // Indica si ya existe una materia con el mismo nombre para un estudiante (case-insensitive)
        public bool ExisteMateriaParaEstudiante(int idEstudiante, string nombreMateria)
        {
            if (idEstudiante <= 0 || string.IsNullOrWhiteSpace(nombreMateria)) return false;
            var lista = _materiaRepository.ObtenerPorEstudiante(idEstudiante) ?? new List<Materia>();
            var nombre = nombreMateria.Trim();
            return lista.Any(m => string.Equals((m.Nombre ?? string.Empty).Trim(), nombre, StringComparison.OrdinalIgnoreCase));
        }

        // Modifica las notas de una materia. 
        public void ModificarNota(Materia materia)
        {
            ValidarNotas(materia);

            try
            {
                _materiaRepository.Actualizar(materia);
            }
            catch (Exception)
            {
                throw new BusinessException($"La materia con ID {materia.IdMateria} no existe para ser actualizada.", "MATERIA_NO_ENCONTRADA");
            }
        }

        // Procesa la baja de una materia. Lanza BusinessException si no se encuentra el ID.
        public void QuitarMateria(int idMateria)
        {
            try
            {
                _materiaRepository.Eliminar(idMateria);
            }
            catch (Exception)
            {
                throw new BusinessException($"No se pudo eliminar la materia ID {idMateria} porque no existe.", "MATERIA_INEXISTENTE");
            }
        }
    }
}