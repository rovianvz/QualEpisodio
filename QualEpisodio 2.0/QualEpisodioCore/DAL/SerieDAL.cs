using QualEpisodioCore.Common;
using QualEpisodioCore.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualEpisodioCore.DAL
{
    class SerieDAL
    {
        private DBHelper m_dbHelper;

        public SerieDAL()
        {
            m_dbHelper = DBHelper.Instance;
        }

        public void InsereSerie(SerieModel _serie, SQLiteTransaction _dbTransaction = null)
        {
            string strSQL = string.Format("INSERT INTO SERIE (NOME, ULTIMO_BAIXADO_TEMPORADA, " +
                                                             "ULTIMO_BAIXADO_EPISODIO, ULTIMO_ASSISTIDO_TEMPORADA, " +
                                                             "ULTIMO_ASSISTIDO_EPISODIO, CAMINHO_IMAGEM) VALUES " +
                                                             "('{0}', {1}, {2}, {3}, {4}, '{5}')",
                                                             _serie.Nome,
                                                             _serie.UltimoBaixadoTemporada,
                                                             _serie.UltimoBaixadoEpisodio,
                                                             _serie.UltimoAssistidoTemporada,
                                                             _serie.UltimoAssistidoEpisodio,
                                                             _serie.CaminhoImagem);


            m_dbHelper.Connection.Open();
            using (SQLiteCommand cmd = new SQLiteCommand(strSQL, m_dbHelper.Connection))
            {
                cmd.ExecuteNonQuery();
            }
            m_dbHelper.Connection.Close();
        }

        public void AtualizaSerie(SerieModel _serie, SQLiteTransaction _dbTransaction = null)
        {
            string strSQL = string.Format("UPDATE SERIE SET (ULTIMO_BAIXADO_TEMPORADA = {1}, " +
                                                             "ULTIMO_BAIXADO_EPISODIO = {2}, " +
                                                             "ULTIMO_ASSISTIDO_TEMPORADA = {3}, " +
                                                             "ULTIMO_ASSISTIDO_EPISODIO = {4}, " +
                                                             "CAMINHO_IMAGEM = '{5}') WHERE " +
                                                             "NOME = {0}",
                                                             _serie.Nome,
                                                             _serie.UltimoBaixadoTemporada,
                                                             _serie.UltimoBaixadoEpisodio,
                                                             _serie.UltimoAssistidoTemporada,
                                                             _serie.UltimoAssistidoEpisodio,
                                                             _serie.CaminhoImagem);

            m_dbHelper.Connection.Open();
            using (SQLiteCommand cmd = new SQLiteCommand(strSQL, m_dbHelper.Connection))
            {
                cmd.ExecuteNonQuery();
            }
            m_dbHelper.Connection.Close();
        }

        public List<SerieModel> ListSeries()
        {
            string strSQL = "SELECT * FROM SERIE";

            List<SerieModel> series = new List<SerieModel>();
            m_dbHelper.Connection.Open();
            using (SQLiteCommand cmd = new SQLiteCommand(strSQL, m_dbHelper.Connection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (reader.Read())
                    {
                        SerieModel serie = new SerieModel
                        (
                            reader.GetString(0),
                            reader.GetInt32(1),
                            reader.GetInt32(2),
                            reader.GetInt32(3),
                            reader.GetInt32(4),
                            reader.GetString(5)
                        );
                        series.Add(serie);
                    }
                }
            }
            m_dbHelper.Connection.Close();
            return series;
        }

        public SerieModel PagaSerie(string _nome)
        {
            string strSQL = string.Format("SELECT * FROM SERIE WHERE NOME = '{0}'", _nome);

            SerieModel serie = null;
            m_dbHelper.Connection.Open();
            using (SQLiteCommand cmd = new SQLiteCommand(strSQL, m_dbHelper.Connection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        serie = new SerieModel
                        (
                            reader.GetString(0),
                            reader.GetInt32(1),
                            reader.GetInt32(2),
                            reader.GetInt32(3),
                            reader.GetInt32(4),
                            reader.GetString(5)
                        );
                    }
                }
            }
            m_dbHelper.Connection.Close();
            return serie;
        }
    }
}
