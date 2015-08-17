namespace QualEpisodio
{
    partial class FormTeste
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.lblPastaSelecionada = new System.Windows.Forms.Label();
            this.cbSeries = new System.Windows.Forms.ComboBox();
            this.lbEpisodios = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblUltAssistido = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUltBaixado = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Selecionar Pasta";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblPastaSelecionada
            // 
            this.lblPastaSelecionada.AutoSize = true;
            this.lblPastaSelecionada.Location = new System.Drawing.Point(144, 18);
            this.lblPastaSelecionada.Name = "lblPastaSelecionada";
            this.lblPastaSelecionada.Size = new System.Drawing.Size(0, 13);
            this.lblPastaSelecionada.TabIndex = 1;
            // 
            // cbSeries
            // 
            this.cbSeries.FormattingEnabled = true;
            this.cbSeries.Location = new System.Drawing.Point(575, 43);
            this.cbSeries.Name = "cbSeries";
            this.cbSeries.Size = new System.Drawing.Size(148, 21);
            this.cbSeries.TabIndex = 5;
            this.cbSeries.SelectedIndexChanged += new System.EventHandler(this.cbSeries_SelectedIndexChanged);
            // 
            // lbEpisodios
            // 
            this.lbEpisodios.FormattingEnabled = true;
            this.lbEpisodios.Location = new System.Drawing.Point(13, 43);
            this.lbEpisodios.Name = "lbEpisodios";
            this.lbEpisodios.Size = new System.Drawing.Size(556, 355);
            this.lbEpisodios.TabIndex = 6;
            this.lbEpisodios.DoubleClick += new System.EventHandler(this.lbEpisodios_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(579, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Último Assistido:";
            // 
            // lblUltAssistido
            // 
            this.lblUltAssistido.AutoSize = true;
            this.lblUltAssistido.Location = new System.Drawing.Point(579, 88);
            this.lblUltAssistido.Name = "lblUltAssistido";
            this.lblUltAssistido.Size = new System.Drawing.Size(0, 13);
            this.lblUltAssistido.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(579, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Último Baixado:";
            // 
            // lblUltBaixado
            // 
            this.lblUltBaixado.AutoSize = true;
            this.lblUltBaixado.Location = new System.Drawing.Point(582, 122);
            this.lblUltBaixado.Name = "lblUltBaixado";
            this.lblUltBaixado.Size = new System.Drawing.Size(0, 13);
            this.lblUltBaixado.TabIndex = 10;
            // 
            // FormTeste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 409);
            this.Controls.Add(this.lblUltBaixado);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblUltAssistido);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbEpisodios);
            this.Controls.Add(this.cbSeries);
            this.Controls.Add(this.lblPastaSelecionada);
            this.Controls.Add(this.button1);
            this.Name = "FormTeste";
            this.Text = "FormTeste";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblPastaSelecionada;
        private System.Windows.Forms.ComboBox cbSeries;
        private System.Windows.Forms.ListBox lbEpisodios;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblUltAssistido;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblUltBaixado;
    }
}