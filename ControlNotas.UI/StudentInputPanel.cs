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
        public event EventHandler<Estudiante>? GuardarSolicitado; // antigua: guarda solo estudiante
        public event EventHandler<StudentWithMateriaInput>? GuardarEstudianteMateria; // nuevo: guarda estudiante y materia
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

        // Cargar tanto estudiante como materia para edición o creación
        public void CargarStudentWithMateria(StudentWithMateriaInput payload)
        {
            if (payload == null) return;
            txtNombre.Text = payload.Estudiante?.Nombre ?? string.Empty;
            txtApellido.Text = payload.Estudiante?.Apellidos ?? string.Empty;
            txtNombre.Tag = payload.Estudiante?.IdEstudiante ?? 0;

            // Seleccionar materia en el combo si existe
            if (!string.IsNullOrWhiteSpace(payload.Materia?.Nombre))
            {
                var idx = cmbMateria.Items.IndexOf(payload.Materia.Nombre);
                if (idx >= 0) cmbMateria.SelectedIndex = idx;
                else cmbMateria.Text = payload.Materia.Nombre; // permitir valor aunque no exista en la lista
            }

            txtParcial1.Text = payload.Materia?.Parcial1.ToString() ?? string.Empty;
            txtParcial2.Text = payload.Materia?.Parcial2.ToString() ?? string.Empty;
            txtParcial3.Text = payload.Materia?.Parcial3.ToString() ?? string.Empty;
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
            // Leemos los datos de materia y parciales del control
            var materia = new Materia();
            materia.Nombre = cmbMateria.SelectedItem?.ToString() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(materia.Nombre))
            {
                lblError.Text = "Seleccione una materia.";
                return;
            }

            if (!double.TryParse(txtParcial1.Text.Trim(), out var p1) || !double.TryParse(txtParcial2.Text.Trim(), out var p2) || !double.TryParse(txtParcial3.Text.Trim(), out var p3))
            {
                lblError.Text = "Ingrese valores numéricos válidos para las pruebas.";
                return;
            }

            // Validar rangos básicos (1..100)
            if (p1 < 1 || p1 > 100 || p2 < 1 || p2 > 100 || p3 < 1 || p3 > 100)
            {
                lblError.Text = "Las pruebas deben estar entre 1 y 100.";
                return;
            }

            materia.Parcial1 = p1;
            materia.Parcial2 = p2;
            materia.Parcial3 = p3;
            materia.IdEstudiante = estudiante.IdEstudiante;
            var payload = new StudentWithMateriaInput
            {
                Estudiante = estudiante,
                Materia = materia
            };

            // Disparamos solo el evento que contiene estudiante+y materia.
            // Evitamos invocar GuardarSolicitado para prevenir guardados duplicados
            // cuando el control se usa dentro de CreateStudentMateriaForm.
            GuardarEstudianteMateria?.Invoke(this, payload);
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
