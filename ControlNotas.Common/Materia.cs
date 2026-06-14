using System;
using System.Collections.Generic;
using System.Text;

//Entidad débil de Estudiante
namespace ControlNotas.Common
{
    public class Materia
    {
        public int IdMateria { get; set; }
        public int IdEstudiante { get; set; }
        public string Nombre { get; set; } = "";
        public double Parcial1 { get; set; }
        public double Parcial2 { get; set; }
        public double Parcial3 { get; set; }
    }
}

//Por cuestion de buenas practicas, se decidió que promedio fuera calculada y no guardada
