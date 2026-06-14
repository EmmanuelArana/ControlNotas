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
            dgvTareas = new DataGridView();
            toggleCompletada = new ToggleSwitch();
            label1 = new Label();
            btnEliminar = new Button();
            lblStatus = new Label();
            panelInput = new panelInput();
            ((System.ComponentModel.ISupportInitialize)dgvTareas).BeginInit();
            SuspendLayout();
            // 
            // dgvTareas
            // 
            dgvTareas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTareas.Location = new Point(14, 16);
            dgvTareas.Margin = new Padding(3, 4, 3, 4);
            dgvTareas.Name = "dgvTareas";
            dgvTareas.ReadOnly = true;
            dgvTareas.RowHeadersWidth = 51;
            dgvTareas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTareas.Size = new Size(610, 309);
            dgvTareas.TabIndex = 0;
            dgvTareas.CellDoubleClick += dgvTareas_CellDoubleClick;
            // 
            // toggleCompletada
            // 
            toggleCompletada.AutoSize = true;
            toggleCompletada.BackColor = Color.Transparent;
            toggleCompletada.BackgroundImageLayout = ImageLayout.None;
            toggleCompletada.ForeColor = Color.Transparent;
            toggleCompletada.Location = new Point(654, 291);
            toggleCompletada.Margin = new Padding(3, 4, 3, 4);
            toggleCompletada.MinimumSize = new Size(57, 35);
            toggleCompletada.Name = "toggleCompletada";
            toggleCompletada.Size = new Size(126, 35);
            toggleCompletada.TabIndex = 2;
            toggleCompletada.Text = "toggleSwitch1";
            toggleCompletada.UseVisualStyleBackColor = true;
            toggleCompletada.CheckedChanged += toggleCompletada_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(654, 251);
            label1.Name = "label1";
            label1.Size = new Size(91, 20);
            label1.TabIndex = 3;
            label1.Text = "Completada";
            label1.Click += label1_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(77, 630);
            btnEliminar.Margin = new Padding(3, 4, 3, 4);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(86, 31);
            btnEliminar.TabIndex = 4;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(34, 721);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 20);
            lblStatus.TabIndex = 5;
            // 
            // panelInput
            // 
            panelInput.BackColor = SystemColors.ButtonHighlight;
            panelInput.Location = new Point(327, 407);
            panelInput.Margin = new Padding(3, 4, 3, 4);
            panelInput.Name = "panelInput";
            panelInput.Size = new Size(484, 254);
            panelInput.TabIndex = 6;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(839, 704);
            Controls.Add(panelInput);
            Controls.Add(lblStatus);
            Controls.Add(btnEliminar);
            Controls.Add(label1);
            Controls.Add(toggleCompletada);
            Controls.Add(dgvTareas);
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            Text = "Form1";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTareas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvTareas;
        private ToggleSwitch toggleCompletada;
        private Label label1;
        private Button btnEliminar;
        private Label lblStatus;
        private panelInput panelInput;
    }
}
