using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ControlNotas;
using ControlNotas.Common;
using System.Windows.Forms;

namespace ControlNotas.UI
{
    public partial class MainForm : Form
    {
        private readonly EstudianteService _estudianteService; // acceso a estudiantes
        private readonly MateriaService _materiaService; // acceso a materias

        public MainForm() //Constructor
        {
            InitializeComponent(); 
            _estudianteService = new EstudianteService();
            _materiaService = new MateriaService();
        }

        private void btnCrearMateria_Click(object? sender, EventArgs e)
        {
            // Si hay un estudiante seleccionado preguntar si se desea añadir materia a ese estudiante
            if (dgvEstudiantes.CurrentRow != null)
            {
                var sel = dgvEstudiantes.CurrentRow.DataBoundItem as Estudiante;
                if (sel != null)
                {
                    var resp = MessageBox.Show($"¿Desea añadir una materia al estudiante '{sel.Nombre} {sel.Apellidos}'?\nSí = Añadir materia al seleccionado. No = Crear nuevo estudiante.",
                        "Crear materia", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (resp == DialogResult.Cancel) return;

                    if (resp == DialogResult.Yes)
                    {
                        // Añadir materia al estudiante seleccionado
                        var payload = new StudentWithMateriaInput { Estudiante = sel };
                        using var frmAdd = new CreateStudentMateriaForm(payload);
                        if (frmAdd.ShowDialog(this) == DialogResult.OK && frmAdd.ResultPayload != null)
                        {
                            try
                            {
                                var id = sel.IdEstudiante;
                                var mat = frmAdd.ResultPayload.Materia;
                                mat.IdEstudiante = id;
                                if (!string.IsNullOrWhiteSpace(mat.Nombre))
                                {
                                    if (_materiaService.ExisteMateriaParaEstudiante(id, mat.Nombre))
                                    {
                                        MessageBox.Show("El estudiante ya tiene registrada esa materia.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        _materiaService.AsignarMateriaAEstudiante(id, mat);
                                    }
                                }
                                CargarMateriasParaEstudiante(id);
                                lblStatus.Text = "Materia agregada al estudiante";
                            }
                            catch (BusinessException ex)
                            {
                                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }

                        return;
                    }
                    // si responde No -> seguirá el flujo de creación de nuevo estudiante
                }
            }

            // Abrir formulario vacío para crear un NUEVO estudiante + materia
            var frm = new CreateStudentMateriaForm();

            if (frm.ShowDialog(this) == DialogResult.OK && frm.ResultPayload != null)
            {
                try
                {
                    // Forzar Id = 0 para crear un nuevo estudiante (no actualizar uno seleccionado)
                    frm.ResultPayload.Estudiante.IdEstudiante = 0;
                    // Guardar estudiante primero
                    _estudianteService.Guardar(frm.ResultPayload.Estudiante);
                    // Usar el Id asignado por el repositorio al objeto guardado
                    var id = frm.ResultPayload.Estudiante.IdEstudiante;
                    frm.ResultPayload.Materia.IdEstudiante = id;
                    if (!string.IsNullOrWhiteSpace(frm.ResultPayload.Materia.Nombre))
                    {
                        // Evitar insertar si ya existe materia con mismo nombre para el estudiante
                        if (_materiaService.ExisteMateriaParaEstudiante(id, frm.ResultPayload.Materia.Nombre))
                        {
                            MessageBox.Show("El estudiante ya tiene registrada esa materia.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            _materiaService.AsignarMateriaAEstudiante(id, frm.ResultPayload.Materia);
                        }
                    }
                    CargarEstudiantes();
                    CargarMateriasParaEstudiante(id);
                    lblStatus.Text = "Estudiante y materia creados";
                }
                catch (BusinessException ex)
                {
                    MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnEditarMateria_Click(object? sender, EventArgs e)
        {
            if (dgvMaterias.CurrentRow == null) return;

            var estRow = dgvEstudiantes.CurrentRow?.DataBoundItem as Estudiante;
            if (estRow == null) return;

            // 1. Extraemos el objeto anónimo usando 'dynamic'
            dynamic item = dgvMaterias.CurrentRow.DataBoundItem;
            if (item == null) return;

            // 2. Leemos la llave primaria directamente
            int idMateriaSeleccionada = item.IdMateria;

            // 3. Buscamos de forma directa y precisa en el servicio por su ID
            var materias = _materiaService.ListarMateriasDeEstudiante(estRow.IdEstudiante);
            var mat = materias.FirstOrDefault(x => x.IdMateria == idMateriaSeleccionada);

            if (mat == null)
            {
                MessageBox.Show("Materia no encontrada para editar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 4. Mapeamos los datos reales al payload del formulario de edición
            var payload = new StudentWithMateriaInput
            {
                Estudiante = new Estudiante
                {
                    IdEstudiante = estRow.IdEstudiante,
                    Nombre = estRow.Nombre,
                    Apellidos = estRow.Apellidos
                },
                Materia = new Materia
                {
                    IdMateria = mat.IdMateria,
                    IdEstudiante = mat.IdEstudiante,
                    Nombre = mat.Nombre,
                    Parcial1 = mat.Parcial1,
                    Parcial2 = mat.Parcial2,
                    Parcial3 = mat.Parcial3
                }
            };

            using var frm = new CreateStudentMateriaForm(payload);
            if (frm.ShowDialog(this) == DialogResult.OK && frm.ResultPayload != null)
            {
                try
                {
                    // Guardar/actualizar estudiante
                    _estudianteService.Guardar(frm.ResultPayload.Estudiante);

                    // Asegurar que la materia mantiene su Id original para actualizar correctamente
                    var materiaAEditar = frm.ResultPayload.Materia;
                    materiaAEditar.IdMateria = mat.IdMateria;
                    materiaAEditar.IdEstudiante = frm.ResultPayload.Estudiante.IdEstudiante;

                    // Persistir cambios en la materia
                    _materiaService.ModificarNota(materiaAEditar);

                    // Recargar datos conservando el flujo limpio
                    CargarEstudiantes();
                    CargarMateriasParaEstudiante(materiaAEditar.IdEstudiante);
                    lblStatus.Text = "Materia editada";
                }
                catch (BusinessException ex)
                {
                    MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnEliminarMateriaDerecha_Click(object? sender, EventArgs e)
        {
            if (dgvMaterias.CurrentRow == null) return;

            var estRow = dgvEstudiantes.CurrentRow?.DataBoundItem as Estudiante;
            if (estRow == null) return;

            dynamic item = dgvMaterias.CurrentRow.DataBoundItem;
            if (item == null) return;

            // 2. Obtenemos el ID directamente
            int idMateriaSeleccionada = item.IdMateria;

            // 3. Buscamos la materia real de forma precisa por su ID único
            var materias = _materiaService.ListarMateriasDeEstudiante(estRow.IdEstudiante);
            var mat = materias.FirstOrDefault(x => x.IdMateria == idMateriaSeleccionada);

            if (mat == null)
            {
                MessageBox.Show("Materia no encontrada para eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 4. Confirmación y eliminación física
            if (MessageBox.Show($"¿Eliminar la materia '{mat.Nombre}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _materiaService.QuitarMateria(mat.IdMateria);

                    // Refrescar la vista derecha con el cálculo limpio
                    CargarMateriasParaEstudiante(estRow.IdEstudiante);
                    lblStatus.Text = "Materia eliminada";
                }
                catch (BusinessException ex)
                {
                    MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CargarEstudiantes();
        }

        private void CargarEstudiantes()
        {
            var estudiantes = _estudianteService.ObtenerTodos();
            dgvEstudiantes.DataSource = null;
            dgvEstudiantes.DataSource = estudiantes;

            if (dgvEstudiantes.Columns.Contains("IdEstudiante")) dgvEstudiantes.Columns["IdEstudiante"].HeaderText = "Id";
            if (dgvEstudiantes.Columns.Contains("Nombre")) dgvEstudiantes.Columns["Nombre"].HeaderText = "Nombre";
            if (dgvEstudiantes.Columns.Contains("Apellidos")) dgvEstudiantes.Columns["Apellidos"].HeaderText = "Apellidos";
            if (dgvEstudiantes.Columns.Contains("Estado")) dgvEstudiantes.Columns["Estado"].Visible = false;
        }

        private void CargarMateriasParaEstudiante(int idEstudiante)
        {
            var materias = _materiaService.ListarMateriasDeEstudiante(idEstudiante) ?? new List<Materia>();

            if (materias.Count == 0)
            {
                dgvMaterias.DataSource = null;
                lblStatus.Text = "Sin materias";
                return;
            }

            // BUENA PRÁCTICA: El promedio NO es un atributo. 
            // Se calcula aquí al vuelo mediante una proyección matemática para la vista.
            var fuentesVista = materias.Select(m => {
                double promedio = Math.Round(m.Parcial1 * 0.3 + m.Parcial2 * 0.3 + m.Parcial3 * 0.4, 2);
                string estado = promedio >= 60 ? "Aprobado" : (promedio >= 40 ? "Convocatoria" : "Reprobado");

                return new
                {
                    m.IdMateria,       
                    Materia = m.Nombre,
                    Prueba1 = m.Parcial1,
                    Prueba2 = m.Parcial2,
                    Prueba3 = m.Parcial3,
                    Promedio = promedio, 
                    Estado = estado      
                };
            }).ToList();

            dgvMaterias.DataSource = null;
            dgvMaterias.DataSource = fuentesVista;

            // Ocultar ID de control
            if (dgvMaterias.Columns.Contains("IdMateria")) dgvMaterias.Columns["IdMateria"].Visible = false;

            // Ajustar Headers ya que el objeto anónimo define los nombres directamente
            if (dgvMaterias.Columns.Contains("Materia")) dgvMaterias.Columns["Materia"].HeaderText = "Materia";
            if (dgvMaterias.Columns.Contains("Prueba1")) dgvMaterias.Columns["Prueba1"].HeaderText = "Prueba 1";
            if (dgvMaterias.Columns.Contains("Prueba2")) dgvMaterias.Columns["Prueba2"].HeaderText = "Prueba 2";
            if (dgvMaterias.Columns.Contains("Prueba3")) dgvMaterias.Columns["Prueba3"].HeaderText = "Prueba 3";
            if (dgvMaterias.Columns.Contains("Promedio"))
            {
                dgvMaterias.Columns["Promedio"].HeaderText = "Promedio";
                dgvMaterias.Columns["Promedio"].DefaultCellStyle.Format = "N2";
            }
            if (dgvMaterias.Columns.Contains("Estado")) dgvMaterias.Columns["Estado"].HeaderText = "Estado";

            // Colorear filas basándonos en la celda "Estado" del Grid
            foreach (DataGridViewRow row in dgvMaterias.Rows)
            {
                var estadoCelda = row.Cells["Estado"].Value?.ToString();
                switch (estadoCelda)
                {
                    case "Aprobado": row.DefaultCellStyle.BackColor = Color.LightGreen; break;
                    case "Convocatoria": row.DefaultCellStyle.BackColor = Color.Khaki; break;
                    case "Reprobado": row.DefaultCellStyle.BackColor = Color.LightCoral; break;
                }
            }
        }

        private void dgvEstudiantes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var estudiante = dgvEstudiantes.Rows[e.RowIndex].DataBoundItem as Estudiante;
            if (estudiante == null) return;

            // (MateriaForm no disponible) Mostrar las materias en el panel derecho
            CargarMateriasParaEstudiante(estudiante.IdEstudiante);
        }

        private void dgvEstudiantes_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvEstudiantes.CurrentRow == null) return;
            var estudiante = dgvEstudiantes.CurrentRow.DataBoundItem as Estudiante;
            if (estudiante == null) return;
            CargarMateriasParaEstudiante(estudiante.IdEstudiante);
        }

        private void btnMaterias_Click(object? sender, EventArgs e)
        {
            if (dgvEstudiantes.CurrentRow == null) return;
            var estudiante = dgvEstudiantes.CurrentRow.DataBoundItem as Estudiante;
            if (estudiante == null) return;

            // Mostrar materias del estudiante en la vista derecha
            CargarMateriasParaEstudiante(estudiante.IdEstudiante);
        }

        private void toggleCompletada_CheckedChanged(object sender, EventArgs e)
        {
            // No aplica en la vista actual; este toggle puede ser usado en futuras extensiones.
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvEstudiantes.CurrentRow == null) return;

            var estudiante = dgvEstudiantes.CurrentRow.DataBoundItem as Estudiante;
            if (estudiante == null) return;

            try
            {
                if (MessageBox.Show($"¿Eliminar a '{estudiante.Nombre} {estudiante.Apellidos}'?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _estudianteService.Eliminar(estudiante.IdEstudiante);
                    CargarEstudiantes();
                    dgvMaterias.DataSource = null;
                    lblStatus.Text = "Estudiante eliminado!";
                }
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvMaterias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
