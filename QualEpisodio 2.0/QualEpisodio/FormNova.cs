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
    public partial class FormNova : Form
    {
        QualEpisodioModel serie;
        byte[] Imagem;
        bool Nova;
        FormGerenciar Gerenciar;
        FormBase Base;

        public FormNova(FormGerenciar _atualiza, FormBase _base, bool _nova, QualEpisodioModel _serie)
        {
            InitializeComponent();
            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            Stream myStream = myAssembly.GetManifestResourceStream("QualEpisodio.icon.ico");
            this.Icon = new Icon(myStream, 40, 40);
            serie = _serie;
            Nova = _nova;
            Gerenciar = _atualiza;
            Base = _base;

            if (!Nova)
            {
                Imagem = _serie.Imagem;
                tbxSerie.Text = serie.Serie;
                if (Imagem != null)
                {
                    MemoryStream ms = new MemoryStream(Imagem);
                    pbImagem.Image = Image.FromStream(ms);
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbxSerie.Text.Equals(String.Empty))
            {
                MessageBox.Show("O nome não pode ser em branco!", "Qual episódio?");
            }
            else
            {
                if (Nova)
                {
                    serie = new QualEpisodioModel(tbxSerie.Text, 1, 0);
                    if (Imagem != null)
                        serie.Imagem = Imagem;
                    DBHelper.Instance.Insert(serie);
                }
                else
                {
                    string oldName = serie.Serie;
                    serie.Serie = tbxSerie.Text;
                    if (Imagem != null)
                        serie.Imagem = Imagem;
                    DBHelper.Instance.Update(serie, oldName);
                }
                
                Gerenciar.ListarSeries();

                Base.SetContextMenu();

                this.Close();
            }

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            // Displays an OpenFileDialog so the user can select a Cursor.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "jpg|*.jpg";
            openFileDialog1.Title = "Selecione uma imagem";

            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CUR file was selected, open it.
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.BufferedStream bf = new BufferedStream(openFileDialog1.OpenFile());
                byte[] buffer = new byte[bf.Length];  
                bf.Read(buffer,0,buffer.Length);

                MemoryStream ms = new MemoryStream(buffer);
                
                pbImagem.Image = Redimensiona(Image.FromStream(ms));
                Imagem = imageToByteArray(pbImagem.Image);
            }
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        private Image Redimensiona(Image imageToResize)
        {
            
            //Nova altura da imagem
            float novaAltura = 50;
            //Nova largura da imagem
            float novaLagura = 50;

            //Cria um novo objeto do tipo Bitmap, é esse objeto que irá renderizar a imagem redimensionada
            Bitmap novaImagem = new Bitmap((int)novaAltura, (int)novaLagura);

            //Cria um objeto Graphics a partir do objeto Bitmap criado.
            Graphics g = Graphics.FromImage((Image)novaImagem);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //Renderiza a imagem no objeto Bitmap já com a nova largura e altura
            g.DrawImage(imageToResize, 0, 0, novaLagura, novaAltura);
            g.Dispose();

            //Salva a nova imagem no disco
            return novaImagem;
        }
    }
}
