namespace QualEpisodio
{
    partial class QualEpisodio
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
            this.pbImagem = new System.Windows.Forms.PictureBox();
            this.lblSeries = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nudTemp = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nudEpi = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbImagem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTemp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEpi)).BeginInit();
            this.SuspendLayout();
            // 
            // pbImagem
            // 
            this.pbImagem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbImagem.Location = new System.Drawing.Point(13, 13);
            this.pbImagem.Name = "pbImagem";
            this.pbImagem.Size = new System.Drawing.Size(50, 50);
            this.pbImagem.TabIndex = 0;
            this.pbImagem.TabStop = false;
            // 
            // lblSeries
            // 
            this.lblSeries.AutoSize = true;
            this.lblSeries.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeries.Location = new System.Drawing.Point(69, 13);
            this.lblSeries.MinimumSize = new System.Drawing.Size(245, 0);
            this.lblSeries.Name = "lblSeries";
            this.lblSeries.Size = new System.Drawing.Size(245, 20);
            this.lblSeries.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Temporada:";
            // 
            // nudTemp
            // 
            this.nudTemp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nudTemp.Location = new System.Drawing.Point(140, 43);
            this.nudTemp.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nudTemp.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTemp.Name = "nudTemp";
            this.nudTemp.Size = new System.Drawing.Size(35, 16);
            this.nudTemp.TabIndex = 3;
            this.nudTemp.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTemp.ValueChanged += new System.EventHandler(this.nudTemp_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(181, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Episódio:";
            // 
            // nudEpi
            // 
            this.nudEpi.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nudEpi.Location = new System.Drawing.Point(237, 43);
            this.nudEpi.Name = "nudEpi";
            this.nudEpi.Size = new System.Drawing.Size(35, 16);
            this.nudEpi.TabIndex = 5;
            // 
            // btnOK
            // 
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOK.Location = new System.Drawing.Point(278, 41);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(38, 20);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // QualEpisodio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 75);
            this.ControlBox = false;
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.nudEpi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudTemp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblSeries);
            this.Controls.Add(this.pbImagem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "QualEpisodio";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            ((System.ComponentModel.ISupportInitialize)(this.pbImagem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTemp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEpi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbImagem;
        private System.Windows.Forms.Label lblSeries;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudTemp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudEpi;
        private System.Windows.Forms.Button btnOK;
    }
}