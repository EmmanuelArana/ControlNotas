using ControlNotas.Common;

namespace ControlNotas.UI
{
    public partial class MainForm : Form
    {
        private readonly EstudianteService _service; // instancia de clase tareaservice que nos permite accesar todos sus metodos y atributos
        public MainForm() //Constructor
        {
            InitializeComponent(); // Inicializa el formulario (ciclos de vida)
            _service = new EstudianteService();
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
            dgvTareas.DataSource = null;
            dgvTareas.DataSource = _service.ObtenerTodos();
            lblStatus.Text = $"Total: {dgvTareas.Rows.Count} estudiantes";
        }

        private void PanelInput_GuardarSolicitado(object? sender, Estudiante estudiante)
        {
            bool esNuevo = estudiante.IdEstudiante == 0; // guardamos si es nuevo ANTES de guardar
            try
            {
                _service.Guardar(estudiante);
                CargarGrid();
                panelInput.btnLimpiar_Click(sender, EventArgs.Empty);
                lblStatus.Text = esNuevo ? "Estudiante creado!" : "Estudiante actualizado!";
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblStatus.Text = $"Error: {ex.CodigoError}";
            }
        }

        private void dgvTareas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // ignorar click en encabezado

            var estudiante = (Estudiante)dgvTareas.Rows[e.RowIndex].DataBoundItem; // tomamos el estudiante de la fila
            panelInput.CargarTarea(estudiante); // cargamos sus datos en el panel
            lblStatus.Text = $"Editando: {estudiante.Nombre} {estudiante.Apellidos}";
        }

        private void toggleCompletada_CheckedChanged(object sender, EventArgs e)
        {
            if (dgvTareas.CurrentRow == null) return;

            var estudiante = (Estudiante)dgvTareas.CurrentRow.DataBoundItem;
            try
            {
                _service.MarcarCompletada(estudiante.IdEstudiante, toggleCompletada.Checked);
                CargarGrid();
                lblStatus.Text = toggleCompletada.Checked
                    ? $"'{estudiante.Nombre}' aprobado!"
                    : $"'{estudiante.Nombre}' reprobado!";
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                toggleCompletada.Checked = !toggleCompletada.Checked; // revertimos el toggle si falla
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvTareas.CurrentRow == null) return;

            var estudiante = (Estudiante)dgvTareas.CurrentRow.DataBoundItem;

            if (MessageBox.Show($"¿Eliminar a '{estudiante.Nombre} {estudiante.Apellidos}'?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _service.Eliminar(estudiante.IdEstudiante);
                CargarGrid();
                panelInput.btnLimpiar_Click(sender, EventArgs.Empty); // limpiamos el panel por si estaba cargado
                lblStatus.Text = "Estudiante eliminado!";
            }
        }
    }
}
