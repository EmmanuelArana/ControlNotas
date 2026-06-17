using System;
using System.Collections.Generic;
using System.Text;

//Entidad fuerte
namespace ControlNotas.Common
{
    public class Estudiante
    {
        public int IdEstudiante { get; set; }
        public string Nombre { get; set; } = "";
        public string Apellidos { get; set; } = "";
        public bool Estado { get; set; } = false; // Estados de un estudiante son "Reprovado" | "Combocatoria" | "Aprobado"
    }
}
