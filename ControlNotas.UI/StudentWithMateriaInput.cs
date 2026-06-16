using ControlNotas.Common;

namespace ControlNotas.UI
{
    public class StudentWithMateriaInput
    {
        public Estudiante Estudiante { get; set; } = new Estudiante();
        public Materia Materia { get; set; } = new Materia();
    }
}
