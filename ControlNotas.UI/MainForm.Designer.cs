namespace ControlNotas.UI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvEstudiantes = new DataGridView();
            dgvMaterias = new DataGridView();
            btnEliminar = new CustomButton();
            btnMaterias = new CustomButton();
            btnCrearMateria = new CustomButton();
            btnEditarMateria = new CustomButton();
            lblStatus = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvEstudiantes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvMaterias).BeginInit();
            SuspendLayout();
            // 
            // dgvEstudiantes
            // 
            dgvEstudiantes.BackgroundColor = SystemColors.ButtonHighlight;
            dgvEstudiantes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEstudiantes.Location = new Point(14, 83);
            dgvEstudiantes.Margin = new Padding(3, 4, 3, 4);
            dgvEstudiantes.Name = "dgvEstudiantes";
            dgvEstudiantes.ReadOnly = true;
            dgvEstudiantes.RowHeadersWidth = 51;
            dgvEstudiantes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEstudiantes.Size = new Size(366, 427);
            dgvEstudiantes.TabIndex = 0;
            dgvEstudiantes.CellDoubleClick += dgvEstudiantes_CellDoubleClick;
            dgvEstudiantes.SelectionChanged += dgvEstudiantes_SelectionChanged;
            // 
            // dgvMaterias
            // 
            dgvMaterias.AllowUserToAddRows = false;
            dgvMaterias.AllowUserToDeleteRows = false;
            dgvMaterias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMaterias.BackgroundColor = SystemColors.ButtonHighlight;
            dgvMaterias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMaterias.Location = new Point(424, 83);
            dgvMaterias.Margin = new Padding(3, 4, 3, 4);
            dgvMaterias.Name = "dgvMaterias";
            dgvMaterias.ReadOnly = true;
            dgvMaterias.RowHeadersWidth = 51;
            dgvMaterias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMaterias.Size = new Size(584, 427);
            dgvMaterias.TabIndex = 1;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.Red;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(424, 559);
            btnEliminar.Margin = new Padding(3, 4, 3, 4);
            btnEliminar.MinimumSize = new Size(90, 40);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(106, 41);
            btnEliminar.TabIndex = 4;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnMaterias
            // 
            btnMaterias.BackColor = Color.Transparent;
            btnMaterias.FlatStyle = FlatStyle.Flat;
            btnMaterias.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnMaterias.ForeColor = Color.White;
            btnMaterias.Location = new Point(12, 12);
            btnMaterias.MinimumSize = new Size(90, 40);
            btnMaterias.Name = "btnMaterias";
            btnMaterias.Size = new Size(120, 40);
            btnMaterias.TabIndex = 7;
            btnMaterias.Text = "Materias/Parciales";
            btnMaterias.UseVisualStyleBackColor = true;
            btnMaterias.Click += btnMaterias_Click;
            // 
            // btnCrearMateria
            // 
            btnCrearMateria.BackColor = Color.Transparent;
            btnCrearMateria.FlatStyle = FlatStyle.Flat;
            btnCrearMateria.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCrearMateria.ForeColor = Color.White;
            btnCrearMateria.Location = new Point(686, 559);
            btnCrearMateria.Margin = new Padding(3, 4, 3, 4);
            btnCrearMateria.MinimumSize = new Size(90, 40);
            btnCrearMateria.Name = "btnCrearMateria";
            btnCrearMateria.Size = new Size(103, 40);
            btnCrearMateria.TabIndex = 9;
            btnCrearMateria.Text = "Crear";
            btnCrearMateria.UseVisualStyleBackColor = true;
            btnCrearMateria.Click += btnCrearMateria_Click;
            // 
            // btnEditarMateria
            // 
            btnEditarMateria.BackColor = Color.Transparent;
            btnEditarMateria.FlatStyle = FlatStyle.Flat;
            btnEditarMateria.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEditarMateria.ForeColor = Color.White;
            btnEditarMateria.Location = new Point(552, 559);
            btnEditarMateria.Margin = new Padding(3, 4, 3, 4);
            btnEditarMateria.MinimumSize = new Size(90, 40);
            btnEditarMateria.Name = "btnEditarMateria";
            btnEditarMateria.Size = new Size(103, 40);
            btnEditarMateria.TabIndex = 10;
            btnEditarMateria.Text = "Editar";
            btnEditarMateria.UseVisualStyleBackColor = true;
            btnEditarMateria.Click += btnEditarMateria_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(34, 721);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 20);
            lblStatus.TabIndex = 5;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(1127, 704);
            Controls.Add(dgvMaterias);
            Controls.Add(btnCrearMateria);
            Controls.Add(btnEditarMateria);
            Controls.Add(lblStatus);
            Controls.Add(btnEliminar);
            Controls.Add(dgvEstudiantes);
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            Text = "Form1";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEstudiantes).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvMaterias).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvEstudiantes;
        private DataGridView dgvMaterias;
        private CustomButton btnEliminar;
        private CustomButton btnMaterias;
        private CustomButton btnCrearMateria;
        private CustomButton btnEditarMateria;
        // El botón de eliminar derecho fue eliminado del diseño
        private Label lblStatus;
    }
}
