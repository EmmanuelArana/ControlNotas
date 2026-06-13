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
    public partial class TaskInputPanel : UserControl
    {
        public event EventHandler<Tarea>? GuardarSolicitado; // Declaracion de evento custom. Le envia al form el objeto tarea para que el form decida que hacer
        public TaskInputPanel() // Constructor
        {
            InitializeComponent(); // Inicializar el componente custom (user control)
            // Llenamos el combo box con los valores del enum: enumerar o listar
            cmbPrioridad.DataSource = Enum.GetValues(typeof(Prioridad)); // Le decimos al cmb box que tome como datos la lista de prioridades en el objeto prioridad (common)
            cmbPrioridad.SelectedItem = Prioridad.Media; // Por defecto el elemento seleccionado sea Prioridad media;
        }

        public void CargarTarea(Tarea t) // Metodo para cargar tareas en interfaz
        {
            // Desplegamos en pantalla los atributos del objeto tarea que viene por parametro.
            txtTitulo.Text = t.Titulo;
            txtDescripcion.Text = t.Descripcion;
            cmbPrioridad.SelectedItem = t.Prioridad;
            // La fecha limite se selecciona "automaticamente" dependiendo de si la fecha original de la tarea es mayor o o menor que la fecha actual (today)
            dtpFechaLimite.Value = t.FechaLimite > dtpFechaLimite.MinDate ? t.FechaLimite : DateTime.Today;
            txtTitulo.Tag = t.Id; // Asignamos el id de la tarea a la propiedad Tag del textbox, para no crear una variable nueva
        }
        private void TaskInputPanel_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e) // El metodo que se va a ejecutar cuando el usuario le de click al boton guardar
        {
            lblError.Text = ""; // Inicializamos el texto de la etiqueta en vacio
            var tarea = new Tarea // Definimos una nueva tarea a guardar
            {
                // Definimos todos los atributos:
                Id = txtTitulo.Tag is int id ? id : 0, // comprobacion de valor existente en la propiedad Tag del txtbox
                Titulo = txtTitulo.Text.Trim(), // Trim = eliminar espacios en blanco al inicio o al final del string 
                Descripcion = txtDescripcion.Text.Trim(),
                Prioridad = (Prioridad)cmbPrioridad.SelectedItem, // Castear o convertir a tipo prioridad el valor seleccionado en el combo box
                FechaLimite = dtpFechaLimite.Value
            };

            GuardarSolicitado?.Invoke(this, tarea); // Enviamos al form el objeto tarea con la informacion completa
        }

        public void btnLimpiar_Click(object sender, EventArgs e) 
        {
            txtTitulo.Text = "";
            txtDescripcion.Text = "";
            cmbPrioridad.SelectedItem = Prioridad.Media;
            dtpFechaLimite.Value = DateTime.Today;
            txtTitulo.Tag = 0;
            lblError.Text = "";
            txtTitulo.Focus();
        }
    }
}
