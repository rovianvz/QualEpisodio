using QualEpisodioCore.DAL;
using QualEpisodioCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualEpisodioCore.BLL
{
    public class BibliotecaBLL
    {
        private BibliotecaDAL m_udtBibliotecaDAL;

        public BibliotecaBLL()
        {
            m_udtBibliotecaDAL = new BibliotecaDAL();
        }

        public void InsereBiblioteca(BibliotecaModel _biblioteca)
        {
            m_udtBibliotecaDAL.InsereBiblioteca(_biblioteca);
        }

        public List<BibliotecaModel> ListaBibliotecas()
        {
            return m_udtBibliotecaDAL.ListBibliotecas();
        }
    }
}
