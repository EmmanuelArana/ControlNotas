namespace ControlNotas.UI
{
    partial class TaskInputPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtTitulo = new TextBox();
            txtDescripcion = new TextBox();
            cmbPrioridad = new ComboBox();
            dtpFechaLimite = new DateTimePicker();
            lblError = new Label();
            btnGuardar = new Button();
            btnLimpiar = new Button();
            SuspendLayout();
            // 
            // txtTitulo
            // 
            txtTitulo.Location = new Point(157, 29);
            txtTitulo.Name = "txtTitulo";
            txtTitulo.Size = new Size(220, 23);
            txtTitulo.TabIndex = 0;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(157, 89);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(220, 60);
            txtDescripcion.TabIndex = 1;
            // 
            // cmbPrioridad
            // 
            cmbPrioridad.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPrioridad.FormattingEnabled = true;
            cmbPrioridad.Location = new Point(157, 210);
            cmbPrioridad.Name = "cmbPrioridad";
            cmbPrioridad.Size = new Size(220, 23);
            cmbPrioridad.TabIndex = 2;
            // 
            // dtpFechaLimite
            // 
            dtpFechaLimite.Location = new Point(157, 256);
            dtpFechaLimite.Name = "dtpFechaLimite";
            dtpFechaLimite.Size = new Size(220, 23);
            dtpFechaLimite.TabIndex = 3;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(157, 174);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 15);
            lblError.TabIndex = 4;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(302, 314);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 23);
            btnGuardar.TabIndex = 5;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(13, 314);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(75, 23);
            btnLimpiar.TabIndex = 6;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // TaskInputPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnLimpiar);
            Controls.Add(btnGuardar);
            Controls.Add(lblError);
            Controls.Add(dtpFechaLimite);
            Controls.Add(cmbPrioridad);
            Controls.Add(txtDescripcion);
            Controls.Add(txtTitulo);
            Name = "TaskInputPanel";
            Size = new Size(419, 360);
            Load += TaskInputPanel_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtTitulo;
        private TextBox txtDescripcion;
        private ComboBox cmbPrioridad;
        private DateTimePicker dtpFechaLimite;
        private Label lblError;
        private Button btnGuardar;
        private Button btnLimpiar;
    }
}
