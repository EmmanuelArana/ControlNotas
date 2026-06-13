using System;
using System.Collections.Generic;
using System.Text;

namespace ControlNotas.Common
{
    public class Tarea
    {
        public int Id { get; set; } // Identificador de la tarea
        public string Titulo { get; set; } = ""; // Titulo de la tarea
        public string Descripcion { get; set; } = ""; // Descripción: de que se trata la tarea
        public Prioridad Prioridad { get; set; } = Prioridad.Media; // Urgencia de la tarea
        public DateTime FechaLimite { get; set; } // Fecha limite para completar la tarea
        public bool Completada { get; set; } = false; // Booleano para marcar una tarea como realizada o pendiente
        public DateTime FechaCreacion {  get; set; } = DateTime.Now; // Fecha en la que se crea la tarea

    }
}
