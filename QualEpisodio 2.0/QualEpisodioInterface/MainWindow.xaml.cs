using QualEpisodioCore;
using QualEpisodioCore.BLL;
using QualEpisodioCore.Common;
using QualEpisodioCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QualEpisodioInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerieBLL m_serieBLL;

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                InicializacaoDev.Inicializa();

                m_serieBLL = new SerieBLL();

                cbSeries.Items.Clear();
                cbSeries.Items.Add("Selecione a serie");
                foreach (SerieModel serie in m_serieBLL.ListaSeries())
                {
                    cbSeries.Items.Add(serie.Nome);
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        private void cbSeries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSeries.SelectedIndex != 0)
            {
                SerieModel serieSelecionada = m_serieBLL.PegaSerie(cbSeries.SelectedItem.ToString());

                Biblioteca biblio = new Biblioteca();

                
                dgEpisodios.Items.Clear();
                int tempAnterior = 0;
                foreach (EpisodioModel em in biblio.ListEpisodiosEmDisco(serieSelecionada))
                {
                    if (em.Temporada > tempAnterior)
                        dgEpisodios.Items.Add(" --- ");

                    tempAnterior = em.Temporada;

                    if(em.Temporada == serieSelecionada.UltimoAssistidoTemporada && em.Episodio == serieSelecionada.UltimoAssistidoEpisodio)
                        dgEpisodios.Items.Add(em);
                    else if (em.Temporada == serieSelecionada.UltimoBaixadoTemporada && em.Episodio == serieSelecionada.UltimoBaixadoEpisodio)
                        dgEpisodios.Items.Add(em);
                    else
                        dgEpisodios.Items.Add(em);
                }
            }
        }

        private void lvEpisodios_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgEpisodios.SelectedIndex > 0)
            {
                if (dgEpisodios.SelectedItem.ToString() != " --- ")
                {
                    System.Diagnostics.Process.Start(((EpisodioModel)dgEpisodios.SelectedItem).CaminhoVideo);
                }
            }
        }
    }
}
