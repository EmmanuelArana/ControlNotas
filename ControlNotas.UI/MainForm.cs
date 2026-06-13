using ControlNotas.Business;
using ControlNotas.Common;

namespace ControlNotas.UI
{
    public partial class MainForm : Form
    {
        private readonly TareaService _service; // instancia de clase tareaservice que nos permite accesar todos sus metodos y atributos
        public MainForm() //Constructor
        {
            InitializeComponent(); // Inicializa el formulario (ciclos de vida)
            _service = new TareaService();
            panelInput.GuardarSolicitado += PanelInput_GuardarSolicitado;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CargarGrid(); // Llamamos al metodo cargar grid para mostrar todas las tareas existentes apenas cargue el form.
        }
        private void CargarGrid()
        {
            // Tomar todas las tareas y mostrarlas en el data grid
            dgvTareas.DataSource = null;
            dgvTareas.DataSource = _service.ObtenerTodas();
            lblStatus.Text = $"Total: {dgvTareas.Rows.Count} tareas";
        }

        private void PanelInput_GuardarSolicitado(object? sender, Tarea t)
        {
            try // intentar
            {
                _service.Guardar(t); // guardar una tarea (actualizar o crear)
                CargarGrid(); // mostrar la tarea con los datos actualizados o la tarea nueva
                lblStatus.Text = t.Id == 0 ? "Tarea Creada" : "Tarea Actualizada"; // validamos si la tarea era nueva o existente y le informamos al user
            }
            catch (BusinessException ex) // capturamos una excepcion de logica de negocio
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); // mostramos un cuadro de dialogo de warning con un boton ok
                lblStatus.Text = $"Error: {ex.CodigoError}"; // actualizamos etiqueta para mostrar el codigo de error al usuario
            }
        }

        private void dgvTareas_CellDoubleClick(object sender, DataGridViewCellEventArgs e) // Cuando damos doble click a una celda del datagridview
        { 
            if(e.RowIndex < 0) return;
            var t = (Tarea)dgvTareas.Rows[e.RowIndex].DataBoundItem; // Tomamos todos los datos de la celda seleccionada en el datagridview
            panelInput.CargarTarea(t); //Actualice los campos del user control (panel input) con los datos de la tarea seleccionada
            toggleCompletada.Checked = t.Completada; // Nuestro toggle nos muestra si la tarea esta completada o no
            lblStatus.Text = $"Editando tarea: {t.Titulo}"; // Estado actual de operacion
        }

        private void toggleCompletada_CheckedChanged(object sender, EventArgs e) // marcamos la tarea como completada
        {
            if (dgvTareas.CurrentRow == null) return; // si la celda en la que me posicione tiene valor null, retornamos sin hacer nada

            var t = (Tarea)dgvTareas.CurrentRow.DataBoundItem; // declaramos tarea nueva
            try // intentamos
            {
                _service.MarcarCompletada(t.Id, toggleCompletada.Checked); // intenatamos agregarle el valor de completada a la tarea
                CargarGrid(); // refrescamos el grid
                lblStatus.Text = toggleCompletada.Checked ? $"'{t.Titulo}' completada!" : $"'{t.Titulo}' reabierta!"; // actualizamos el estado de la tarea en el label
            }
            catch (BusinessException ex) // capturamos la excepcion en caso de que el bloque de codigo anterior falle
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); // mostrar un warning con el mensaje de error
                toggleCompletada.Checked = !toggleCompletada.Checked; // reseteamos el estado de nuestro toggle switch;
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e) // al click del boton eliminar
        {
            if (dgvTareas.CurrentRow == null) return; // si la celda elegida en dgv tiene valor nulo, terminamos el evento sin hacer nada

            var t = (Tarea)dgvTareas.CurrentRow.DataBoundItem; // cargamos una tarea con la seleccion de datos del dgv

            // Desplegamos un message box para que el usuario confirme si desea eliminar el elemento
            //Verificamos si el boton del message box es "Yes", eliminamos la tarea
            if (MessageBox.Show($"Eliminar '{t.Titulo}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _service.Eliminar(t.Id); // Eliminamos la tarea seleccionada
                CargarGrid(); // refrescamos el grid
                lblStatus.Text = "Tarea Eliminada!"; // actualizamos el estado en el lbl.
            }
        }
    }
}
