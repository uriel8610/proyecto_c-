namespace Proyecto_interfaz
{
    partial class Reporte
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.dgvReporte = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.RtxtInicio = new System.Windows.Forms.TextBox();
            this.RtxtFinal = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(363, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(269, 29);
            this.label2.TabIndex = 35;
            this.label2.Text = "REPORTE DE CITAS";
            // 
            // dgvReporte
            // 
            this.dgvReporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReporte.Location = new System.Drawing.Point(16, 144);
            this.dgvReporte.Name = "dgvReporte";
            this.dgvReporte.Size = new System.Drawing.Size(944, 165);
            this.dgvReporte.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(431, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 29);
            this.label1.TabIndex = 33;
            this.label1.Text = "PERIODO";
            // 
            // RtxtInicio
            // 
            this.RtxtInicio.Enabled = false;
            this.RtxtInicio.Location = new System.Drawing.Point(275, 105);
            this.RtxtInicio.Name = "RtxtInicio";
            this.RtxtInicio.Size = new System.Drawing.Size(76, 20);
            this.RtxtInicio.TabIndex = 32;
            // 
            // RtxtFinal
            // 
            this.RtxtFinal.Enabled = false;
            this.RtxtFinal.Location = new System.Drawing.Point(724, 102);
            this.RtxtFinal.Name = "RtxtFinal";
            this.RtxtFinal.Size = new System.Drawing.Size(76, 20);
            this.RtxtFinal.TabIndex = 31;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(609, 105);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 13);
            this.label11.TabIndex = 30;
            this.label11.Text = "Fecha de Termino:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(175, 109);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Fecha de Inicio:";
            // 
            // Reporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 331);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvReporte);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RtxtInicio);
            this.Controls.Add(this.RtxtFinal);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Name = "Reporte";
            this.Text = "Reporte";
            this.Load += new System.EventHandler(this.Reporte_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvReporte;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox RtxtInicio;
        private System.Windows.Forms.TextBox RtxtFinal;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
    }
}