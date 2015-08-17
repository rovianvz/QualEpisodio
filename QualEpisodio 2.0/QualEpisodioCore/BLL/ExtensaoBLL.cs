using QualEpisodioCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualEpisodioCore.DAL
{
    public class ExtensaoBLL
    {
        private ExtensaoDAL m_udtExtensaoDAL;

        public ExtensaoBLL()
        {
            m_udtExtensaoDAL = new ExtensaoDAL();
        }

        public void InsereSerie(ExtensaoModel _extensao)
        {
            m_udtExtensaoDAL.InsereExtensao(_extensao);
        }

        public void InsereSeries(List<ExtensaoModel> _extensoes)
        {
            foreach (ExtensaoModel extensao in _extensoes)
                m_udtExtensaoDAL.InsereExtensao(extensao, null);

        }

        public List<ExtensaoModel> ListaExtensoes()
        {
            return m_udtExtensaoDAL.ListaExtensoes();
        }
    }
}
