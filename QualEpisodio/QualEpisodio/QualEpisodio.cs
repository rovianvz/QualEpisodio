using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace QualEpisodio
{
    public partial class QualEpisodio : Form
    {
        bool assistida = true;
        bool inicializing = true;
        FormBase parent;

        public QualEpisodio(string serie, bool _assistida, FormBase _parent)
        {
            InitializeComponent();
            PlaceLowerRight();
            assistida = _assistida;
            Inicializa(serie);
            inicializing = false;
            parent = _parent;
        }

        private void PlaceLowerRight()
        {
            //Determine "rightmost" screen
            Screen rightmost = Screen.AllScreens[0];
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.Right > rightmost.WorkingArea.Right)
                    rightmost = screen;
            }

            this.Left = rightmost.WorkingArea.Right - this.Width;
            this.Top = rightmost.WorkingArea.Bottom - this.Height;
        }

        private void Inicializa(string _serie)
        {
            lblSeries.Text = _serie;
            QualEpisodioModel serie;
            serie = DBHelper.Instance.Select(_serie, assistida);
            
            nudEpi.Value = serie.Episodio;
            nudTemp.Value = serie.Temporada;
            if (serie.Imagem != null)
            {
                MemoryStream stream = new MemoryStream(serie.Imagem);
                Bitmap image = new Bitmap(stream);
                pbImagem.Image = image;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            QualEpisodioModel serie = new QualEpisodioModel(lblSeries.Text, (int)nudTemp.Value, (int)nudEpi.Value);

            serie.Imagem = DBHelper.Instance.Select(lblSeries.Text, assistida).Imagem;
            DBHelper.Instance.Update(serie, serie.Serie, assistida);
            
            parent.SetContextMenu();

            this.Close();
        }

        private void nudTemp_ValueChanged(object sender, EventArgs e)
        {
            if(!inicializing)
                nudEpi.Value = 1;
        }
    }
}
