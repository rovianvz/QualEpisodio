using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualEpisodioCore.Model
{
    public class BibliotecaModel
    {
        string m_strDiretorio;

        public BibliotecaModel(string _strDiretorio)
        {
            this.m_strDiretorio = _strDiretorio;
        }

        public string Diretorio
        {
            get { return m_strDiretorio; }
            set { m_strDiretorio = value; }
        }
    }
}
