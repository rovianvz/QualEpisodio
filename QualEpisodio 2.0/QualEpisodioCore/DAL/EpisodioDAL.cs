using QualEpisodioCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualEpisodioCore.DAL
{
    class EpisodioDAL
    {
        private DBHelper m_dbHelper;

        public EpisodioDAL()
        {
            m_dbHelper = DBHelper.Instance;
        }
    }
}
