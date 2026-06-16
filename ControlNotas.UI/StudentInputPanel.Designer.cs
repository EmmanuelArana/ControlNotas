namespace ControlNotas.UI
{
    partial class panelInput
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
            txtNombre = new TextBox();
            txtApellido = new TextBox();
            txtParcial1 = new TextBox();
            txtParcial2 = new TextBox();
            txtParcial3 = new TextBox();
            cmbMateria = new ComboBox();
            lblError = new Label();
            btnGuardar = new Button();
            btnLimpiar = new Button();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(179, 39);
            txtNombre.Margin = new Padding(3, 4, 3, 4);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(251, 27);
            txtNombre.TabIndex = 0;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(179, 88);
            txtApellido.Margin = new Padding(3, 4, 3, 4);
            txtApellido.Multiline = true;
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(251, 31);
            txtApellido.TabIndex = 1;
            // 
            // cmbMateria
            // 
            cmbMateria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMateria.Items.AddRange(new object[] { "Matematicas", "Español", "Ciencias", "Sociales", "Ingles" });
            cmbMateria.Location = new Point(179, 136);
            cmbMateria.Name = "cmbMateria";
            cmbMateria.Size = new Size(251, 27);
            cmbMateria.TabIndex = 2;
            // 
            // txtParcial1
            // 
            txtParcial1.Location = new Point(179, 176);
            txtParcial1.Name = "txtParcial1";
            txtParcial1.Size = new Size(80, 27);
            txtParcial1.TabIndex = 3;
            // 
            // txtParcial2
            // 
            txtParcial2.Location = new Point(269, 176);
            txtParcial2.Name = "txtParcial2";
            txtParcial2.Size = new Size(80, 27);
            txtParcial2.TabIndex = 4;
            // 
            // txtParcial3
            // 
            txtParcial3.Location = new Point(359, 176);
            txtParcial3.Name = "txtParcial3";
            txtParcial3.Size = new Size(80, 27);
            txtParcial3.TabIndex = 5;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(179, 232);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 20);
            lblError.TabIndex = 4;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(344, 208);
            btnGuardar.Margin = new Padding(3, 4, 3, 4);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(86, 31);
            btnGuardar.TabIndex = 5;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(219, 208);
            btnLimpiar.Margin = new Padding(3, 4, 3, 4);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(86, 31);
            btnLimpiar.TabIndex = 6;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(54, 46);
            label1.Name = "label1";
            label1.Size = new Size(64, 20);
            label1.TabIndex = 7;
            label1.Text = "Nombre";
            // 
            // labelMateria
            // 
            labelMateria = new Label();
            labelMateria.AutoSize = true;
            labelMateria.Location = new Point(54, 140);
            labelMateria.Name = "labelMateria";
            labelMateria.Size = new Size(56, 20);
            labelMateria.TabIndex = 9;
            labelMateria.Text = "Materia";
            // 
            // labelParciales
            // 
            labelParciales = new Label();
            labelParciales.AutoSize = true;
            labelParciales.Location = new Point(54, 179);
            labelParciales.Name = "labelParciales";
            labelParciales.Size = new Size(67, 20);
            labelParciales.TabIndex = 10;
            labelParciales.Text = "Pruebas";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(54, 99);
            label2.Name = "label2";
            label2.Size = new Size(72, 20);
            label2.TabIndex = 8;
            label2.Text = "Apellidos";
            // 
            // panelInput
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            Controls.Add(label2);
            Controls.Add(labelMateria);
            Controls.Add(cmbMateria);
            Controls.Add(labelParciales);
            Controls.Add(txtParcial1);
            Controls.Add(txtParcial2);
            Controls.Add(txtParcial3);
            Controls.Add(label1);
            Controls.Add(btnLimpiar);
            Controls.Add(btnGuardar);
            Controls.Add(lblError);
            Controls.Add(txtApellido);
            Controls.Add(txtNombre);
            Margin = new Padding(3, 4, 3, 4);
            Name = "panelInput";
            Size = new Size(479, 238);
            Load += TaskInputPanel_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtNombre;
        private TextBox txtApellido;
        private ComboBox cmbMateria;
        private TextBox txtParcial1;
        private TextBox txtParcial2;
        private TextBox txtParcial3;
        private Label lblError;
        private Button btnGuardar;
        private Button btnLimpiar;
        private Label label1;
        private Label label2;
        private Label labelMateria;
        private Label labelParciales;
    }
}
