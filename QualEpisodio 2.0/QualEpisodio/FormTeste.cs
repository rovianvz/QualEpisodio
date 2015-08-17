using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QualEpisodio
{
    public partial class FormTeste : Form
    {
        public FormTeste()
        {
            InitializeComponent();

            cbSeries.Items.AddRange(DBHelper.Instance.SelectAllSeries().ToArray());

            InitializeListSeries();
        }

        List<QualEpisodioModel> episodios;

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                InitializeListSeries();
            }
        }

        private void InitializeListSeries()
        {
            String folder = @"C:\Users\Rovian\Documents\Shared";
            lblPastaSelecionada.Text = folderBrowserDialog1.SelectedPath;
            if (lblPastaSelecionada.Text.Length > 0)
                folder = lblPastaSelecionada.Text;

            StringBuilder sb = new StringBuilder();
            videos = new List<string>();
            DirSearchRec(folder);

            episodios = new List<QualEpisodioModel>();

            List<string> series = DBHelper.Instance.SelectAllSeries();

            foreach (string serie in series)
            {
                List<string> nome = serie.Split(' ').ToList();

                nome.RemoveAll(x => x.Length < 3);

                sb.Append("### " + serie + "###").Append("\r\n");

                foreach (string episodio in videos)
                {
                    if (NomeBate(episodio, nome))
                    {
                        sb.Append("  " + serie + ": ");
                        // Split on one or more non-digit characters.
                        string[] numbers = Regex.Split(episodio, @"\D+");
                        numbers = numbers.Where(x => x.Length < 3).ToArray();
                        if (numbers.Length > 1)
                        {
                            try
                            {
                                QualEpisodioModel qem = new QualEpisodioModel();
                                qem.Serie = serie;
                                qem.Temporada = Int32.Parse(numbers[1]);
                                qem.Episodio = Int32.Parse(numbers[2]);
                                qem.Path = episodio;
                                episodios.Add(qem);
                            }
                            catch { }
                            sb.Append("S" + numbers[1]);
                            sb.Append("E" + numbers[2]);
                            sb.Append("\r\n");
                        }
                    }
                }

            }
        }

        private bool NomeBate(string nome, List<string> nomes)
        {
            bool bate = true;
            foreach (string s in nomes)
            {
                if (!nome.Contains(s))
                {
                    bate = false;
                    break;
                }
            }

            return bate;
        }

        List<string> videos;

        private void DirSearchRec(string sDir)
        {
            

            DirectoryInfo pasta = new DirectoryInfo(sDir);

            foreach (DirectoryInfo d in pasta.GetDirectories())
            {
                foreach (FileInfo f in GetFiles(d))
                {
                    videos.Add(f.FullName);
                }
                DirSearchRec(d.FullName);
            }

        }

        private FileInfo[] GetFiles(DirectoryInfo dir) {

            string filter = "mkv|rmvb|mp4|avi|mpeg|mpg";
            // ArrayList will hold all file names
            List<FileInfo> files = new List<FileInfo>();
 
            // Create an array of filter string
            string[] MultipleFilters = filter.Split('|');
 
            // for each filter find mathing file names
            foreach (string FileFilter in MultipleFilters)
            {
                // add found file names to array list
                files.AddRange(dir.GetFiles(string.Format("*.{0}", FileFilter)));
            }
 
            // returns string array of relevant file names
            return files.ToArray();
        }

        private void cbSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbEpisodios.Items.Clear();
            
            if (cbSeries.SelectedIndex >= 0)
            {
                UpdateLabels();
            }
        }

        private void UpdateLabels()
        {
            String serie = cbSeries.SelectedItem.ToString();
            baixado = DBHelper.Instance.Ultimo(serie, false);
            assistida = DBHelper.Instance.Ultimo(serie, true);

            lblUltAssistido.Text = string.Format("S{0}E{1}", assistida.Temporada, assistida.Episodio);
            lblUltBaixado.Text = string.Format("S{0}E{1}", baixado.Temporada, baixado.Episodio);

            List<QualEpisodioModel> episodiosSerie = episodios.Where(x => x.Serie.Equals(serie)).ToList();
            foreach (QualEpisodioModel qem in episodiosSerie)
            {
                lbEpisodios.Items.Add(string.Format("{0} - S{1}E{2} #{3}", qem.Serie, qem.Temporada, qem.Episodio, qem.Path));
            }
        }

        QualEpisodioModel baixado;
        QualEpisodioModel assistida;

        private void lbEpisodios_DoubleClick(object sender, EventArgs e)
        {
            if (lbEpisodios.SelectedIndex > 0)
            {
                String linhaSelecionada = lbEpisodios.SelectedItem.ToString();
                DialogResult dialogResult = MessageBox.Show("Deseja marcar como último assistido?", "Qual episódio", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                    QualEpisodioModel serie = new QualEpisodioModel(linhaSelecionada.Substring(0, linhaSelecionada.IndexOf("-") - 1), 100, 100);
                    serie.Imagem = DBHelper.Instance.Select(true, linhaSelecionada.Substring(0, linhaSelecionada.IndexOf("-") - 1)).Imagem;
                    DBHelper.Instance.Update(serie, serie.Serie, true);
                }
                UpdateLabels();
                System.Diagnostics.Process.Start(@"C:\Users\Rovian\Documents\Shared\The Listener\The.Listener.S05E01.720p.HDTV.x264-KILLERS.mkv");
            }

        }
    }
}
