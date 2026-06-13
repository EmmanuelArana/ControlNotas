using System;
using System.Collections.Generic;
using System.Text;
using ControlNotas.Common;
using ControlNotas.DataAccess;

namespace ControlNotas.Business
{
    public class TareaService
    {
        private readonly TareaRepository _repo; // Definimos una variable de tipo TareaRepository, llamada _repo, privada y de solo lectura.
                                                // Solo es posible accederla en esta clase.
        public TareaService() // Constructor
        {
            _repo = new TareaRepository(); // Instanciamos _repo como un objeto de tipo tarea repository
        }

        public List<Tarea> ObtenerTodas() => _repo.ObtenerTodas(); // Llamar al metodo obtenerTodas() de la clase TareaRepository de la capa DataAccess

        public void Guardar(Tarea tarea) // Creamos un metodo para guardar una tarea nueva o actualizar una existente
        {
            if (string.IsNullOrWhiteSpace(tarea.Titulo)) // Validacion #1: el titulo debe tener datos, no puede ser vacio
                throw new BusinessException("El titulo de la tarea es requerido", "TAREA_TITULO_REQ");
            if (tarea.Titulo.Length < 3) // Validacion #2: el titulo no puede ser demasiado corto
                throw new BusinessException("El titulo debe contener al menos 3 caracteres", "TAREA_TITULO_CORTO");
            if (tarea.FechaLimite.Date < DateTime.Today) // Validacion #3: que la fecha de la tarea sea una fecha presente o futura, no pasada.
                throw new BusinessException("La fecha limite no puede estar en el pasado","TAREA_FECHA_PASADA");
            if (tarea.Id == 0)
                _repo.Insertar(tarea); // Si el ID de la tarea es 0, entonces agregamos la tarea en la lista
            else
                _repo.Actualizar(tarea); // Si el ID de la tarea no es 0, actualizamos la tarea
        }

        public void MarcarCompletada(int id, bool completada) // Metodo para marcar una tarea existente como completada
        {
            var todas = _repo.ObtenerTodas(); //Definimos la variable "todas" que nos almacena la lista de las tareas existentes.
            var tarea = todas.FirstOrDefault(t => t.Id == id) // almacenamos la tarea que coincida con el id por parametro
                ?? throw new BusinessException("Tarea no encontrada", "TAREA_NOT_FOUND"); // el operador ?? reemplaza al if(variable==null)
            if(tarea.Completada && completada) // tomamos el atributo "completada" del objeto tara y lo comparamos con el valor del parametro completada
            {                                  // Si la comparacion da como resultado true, la tarea ya fue completada, lanzamos una excepcion nueva
                throw new BusinessException("Esta tarea ya fue completada", "TAREA_YA_COMPLETADA");
            }
            tarea.Completada = completada; // Asignamos el valor True al atributo completada del objeto tarea
            _repo.Actualizar(tarea); // Llamamos al metodo para actualizar el valor de la tarea
        }

        public void Eliminar(int id) => _repo.Eliminar(id); //Eliminamos la tarea.  El operador => nos reemplaza las llaves del metodo

    }
}
