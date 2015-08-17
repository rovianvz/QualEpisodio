using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualEpisodioCore.Model
{
    public class EpisodioModel
    {
        private string m_strSerie;
        private int m_intTemporada;
        private int m_intEpisodio;
        private string m_strCaminhoVideo;
        private string m_strCaminhoImagem;

        public EpisodioModel(string _udtSerie, int _intTemporada, int _intEpisodio)
        {
            this.m_strSerie = _udtSerie;
            this.m_intTemporada = _intTemporada;
            this.m_intEpisodio = _intEpisodio;
        }

        public string Serie
        {
            get { return this.m_strSerie; }
            set { this.m_strSerie = value; }
        }

        public int Temporada
        {
            get { return this.m_intTemporada; }
            set { this.m_intTemporada = value; }
        }

        public int Episodio
        {
            get { return m_intEpisodio; }
            set { m_intEpisodio = value; }
        }

        public string CaminhoVideo
        {
            get { return m_strCaminhoVideo; }
            set { m_strCaminhoVideo = value; }
        }


        public string CaminhoImagem
        {
            get { return m_strCaminhoImagem; }
            set { m_strCaminhoImagem = value; }
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}x{2}", m_strSerie, m_intTemporada, m_intEpisodio);
        }

    }
}
