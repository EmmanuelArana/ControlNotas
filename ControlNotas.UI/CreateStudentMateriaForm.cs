using System;
using System.Windows.Forms;
using ControlNotas.Common;

namespace ControlNotas.UI
{
    public class CreateStudentMateriaForm : Form
    {
        private panelInput inputPanel;
        public StudentWithMateriaInput? ResultPayload { get; private set; }

        public CreateStudentMateriaForm()
        {
            InitializeComponent();
            // Asegurar que el panel inicia limpio (Id=0) para crear un nuevo estudiante
            try
            {
                inputPanel.btnLimpiar_Click(this, EventArgs.Empty);
            }
            catch
            {
                // si no está disponible, ignorar
            }
        }

        // Constructor para edición: recibe payload existente y lo carga en el panel
        public CreateStudentMateriaForm(StudentWithMateriaInput payload) : this()
        {
            if (payload != null)
            {
                inputPanel.CargarStudentWithMateria(payload);
            }
        }

        private void InitializeComponent()
        {
            this.inputPanel = new panelInput();
            this.SuspendLayout();
            // 
            // inputPanel
            // 
            this.inputPanel.Dock = DockStyle.Fill;
            this.inputPanel.Location = new System.Drawing.Point(0, 0);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(500, 260);
            this.inputPanel.TabIndex = 0;
            this.inputPanel.GuardarEstudianteMateria += InputPanel_GuardarEstudianteMateria;
            // 
            // CreateStudentMateriaForm
            // 
            this.ClientSize = new System.Drawing.Size(500, 260);
            this.Controls.Add(this.inputPanel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Crear estudiante y materia";
            this.ResumeLayout(false);
        }

        private void InputPanel_GuardarEstudianteMateria(object? sender, StudentWithMateriaInput e)
        {
            // Evitar doble envío: desuscribimos el evento y deshabilitamos el panel inmediatamente
            try
            {
                if (inputPanel != null)
                {
                    inputPanel.GuardarEstudianteMateria -= InputPanel_GuardarEstudianteMateria;
                    inputPanel.Enabled = false;
                }
            }
            catch
            {
                // ignorar
            }

            // Guardamos el payload y cerramos con OK
            ResultPayload = e;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
