using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualEpisodioCore.Model
{
    public class ExtensaoModel
    {
        private string m_strExtensao;

        public ExtensaoModel(string _strExtensao)
        {
            m_strExtensao = _strExtensao;
        }

        public string Extensao
        {
            get { return m_strExtensao; }
            set { m_strExtensao = value; }
        }

        public override string ToString()
        {
            return m_strExtensao;
        }
    }
}
