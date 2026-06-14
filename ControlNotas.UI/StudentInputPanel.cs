using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ControlNotas.Common;

namespace ControlNotas.UI
{
    public partial class panelInput : UserControl
    {
        public event EventHandler<Estudiante>? GuardarSolicitado; // Declaracion de evento custom. Le envia al form el objeto tarea para que el form decida que hacer
        public panelInput() // Constructor
        {
            InitializeComponent(); // Inicializar el componente custom (user control)
        }

        public void CargarTarea(Estudiante estudiante) // Metodo para cargar tareas en interfaz
        {
            // Desplegamos en pantalla los atributos del objeto tarea que viene por parametro.
            txtNombre.Text = estudiante.Nombre;
            txtApellido.Text = estudiante.Apellidos;
            txtNombre.Tag = estudiante.IdEstudiante; // Asignamos el id de la tarea a la propiedad Tag del textbox, para no crear una variable nueva
        }
        private void TaskInputPanel_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e) // El metodo que se va a ejecutar cuando el usuario le de click al boton guardar
        {
            lblError.Text = ""; // Inicializamos el texto de la etiqueta en vacio
            var estudiante = new Estudiante // Definimos una nueva tarea a guardar
            {
                // Definimos todos los atributos:
                IdEstudiante = txtNombre.Tag is int id ? id : 0, // comprobacion de valor existente en la propiedad Tag del txtbox
                Nombre = txtNombre.Text.Trim(), // Trim = eliminar espacios en blanco al inicio o al final del string 
                Apellidos = txtApellido.Text.Trim(),
            };

            GuardarSolicitado?.Invoke(this, estudiante); // Enviamos al form el objeto tarea con la informacion completa
        }

        public void btnLimpiar_Click(object sender, EventArgs e) 
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtNombre.Tag = 0;
            lblError.Text = "";
            txtNombre.Focus();
        }
    }
}
