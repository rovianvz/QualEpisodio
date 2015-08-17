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

namespace QualEpisodioInterface.controls
{
    /// <summary>
    /// Interaction logic for Episodio.xaml
    /// </summary>
    public partial class EpisodioControl : UserControl
    {
        EpisodioModel m_udtEpisodioModel;

        public EpisodioControl()
        {
            InitializeComponent();
        }

        public EpisodioModel Episodio
        {
            set
            {
                m_udtEpisodioModel = value;
                lblEpisodio.Content = string.Format("{0}x{1}", m_udtEpisodioModel.Temporada, m_udtEpisodioModel.Episodio);
                lblSerie.Content = m_udtEpisodioModel.Episodio;
            }
        }
    }
}
