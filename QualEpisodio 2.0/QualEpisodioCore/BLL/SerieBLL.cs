using QualEpisodioCore.Common;
using QualEpisodioCore.DAL;
using QualEpisodioCore.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualEpisodioCore.BLL
{
    public class SerieBLL
    {
        private SerieDAL m_udtSerieDAL;

        public SerieBLL()
        {
            m_udtSerieDAL = new SerieDAL();
        }

        public void InsereSerie(SerieModel _serie)
        {
            m_udtSerieDAL.InsereSerie(_serie);
        }

        public void InsereSeries(List<SerieModel> _series)
        {
            foreach (SerieModel serie in _series)
                m_udtSerieDAL.InsereSerie(serie, null);

        }

        public void AtualizaSerie(SerieModel _serie)
        {
            m_udtSerieDAL.AtualizaSerie(_serie);
        }

        public void AtualizaSeries(List<SerieModel> _series)
        {
            foreach (SerieModel serie in _series)
                m_udtSerieDAL.AtualizaSerie(serie);

        }

        public List<SerieModel> ListaSeries()
        {
            return m_udtSerieDAL.ListSeries();
        }

        public SerieModel PegaSerie(string _nome)
        {
            return m_udtSerieDAL.PagaSerie(_nome);
        }

    }
}
