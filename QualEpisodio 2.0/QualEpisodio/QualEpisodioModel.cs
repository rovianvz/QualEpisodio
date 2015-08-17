using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QualEpisodio
{
    public class QualEpisodioModel
    {
        private String serie;
        private int temporada;
        private int episodio;
        private byte[] imagem;
        private String path;

        public byte[] Imagem
        {
            get { return imagem; }
            set { imagem = value; }
        }

        public String Serie
        {
            get { return serie; }
            set { serie = value; }
        }

        public String Path
        {
            get { return path; }
            set { path = value; }
        }
        

        public int Temporada
        {
            get { return temporada; }
            set { temporada = value; }
        }
        

        public int Episodio
        {
            get { return episodio; }
            set { episodio = value; }
        }

        public QualEpisodioModel(String _serie, int _temporada, int _episodio)
        {
            serie = _serie;
            temporada = _temporada;
            episodio = _episodio;
        }

        public QualEpisodioModel()
        {

        }


    }
}
