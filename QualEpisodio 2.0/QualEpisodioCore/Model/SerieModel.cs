using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualEpisodioCore.Model
{
    public class SerieModel
    {
        private string m_strNome;
        private int m_intUltimoBaixadoTemporada;
        private int m_intUltimoBaixadoEpisodio;
        private int m_udtUltimoAssistidoTemporada;
        private int m_udtUltimoAssistidoEpisodio;
        private string m_strCaminhoImagem;

        public SerieModel(string _strNome)
        {
            this.m_strNome = _strNome;
            this.m_udtUltimoAssistidoEpisodio = 0;
            this.m_udtUltimoAssistidoTemporada = 0;
            this.m_intUltimoBaixadoEpisodio = 0;
            this.m_intUltimoBaixadoTemporada = 0;
        }

        public SerieModel(string _strNome, int _ultBaixTemp, int _ultBaixEpi, int _ultAssTemp, int _ultAssEpi, string _caminho)
        {
            this.m_strNome = _strNome;
            this.m_udtUltimoAssistidoEpisodio = _ultAssEpi;
            this.m_udtUltimoAssistidoTemporada = _ultAssTemp;
            this.m_intUltimoBaixadoEpisodio = _ultBaixEpi;
            this.m_intUltimoBaixadoTemporada = _ultBaixTemp;
            this.m_strCaminhoImagem = _caminho;
        }

        public string Nome
        {
            get { return m_strNome; }
            set { m_strNome = value; }
        }

        public int UltimoBaixadoTemporada
        {
            get { return m_intUltimoBaixadoTemporada; }
            set { m_intUltimoBaixadoTemporada = value; }
        }

        public int UltimoBaixadoEpisodio
        {
            get { return m_intUltimoBaixadoEpisodio; }
            set { m_intUltimoBaixadoEpisodio = value; }
        }

        public int UltimoAssistidoTemporada
        {
            get { return m_udtUltimoAssistidoTemporada; }
            set { m_udtUltimoAssistidoTemporada = value; }
        }

        public int UltimoAssistidoEpisodio
        {
            get { return m_udtUltimoAssistidoEpisodio; }
            set { m_udtUltimoAssistidoEpisodio = value; }
        }
        
        public string CaminhoImagem
        {
            get { return m_strCaminhoImagem; }
            set { m_strCaminhoImagem = value; }
        }


    }
}
