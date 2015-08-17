using QualEpisodioCore.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualEpisodioCore.BLL
{
    public class EpisodioBLL
    {
        private EpisodioDAL m_udtEpisodioDAL;

        public EpisodioBLL()
        {
            m_udtEpisodioDAL = new EpisodioDAL();
        }
    }
}
