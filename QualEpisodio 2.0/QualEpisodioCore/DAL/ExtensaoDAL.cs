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
    public class ExtensaoDAL
    {
        private DBHelper m_dbHelper;

        public ExtensaoDAL()
        {
            m_dbHelper = DBHelper.Instance;
        }

        public void InsereExtensao(ExtensaoModel _extensao, SQLiteTransaction _dbTransaction = null)
        {
            string strSQL = string.Format("INSERT INTO EXTENSAO (EXTENSAO) VALUES ('{0}')",
                                                             _extensao);


            m_dbHelper.Connection.Open();
            using (SQLiteCommand cmd = new SQLiteCommand(strSQL, m_dbHelper.Connection))
            {
                cmd.ExecuteNonQuery();
            }
            m_dbHelper.Connection.Close();
        }

        public List<ExtensaoModel> ListaExtensoes()
        {
            string strSQL = "SELECT * FROM EXTENSAO";

            List<ExtensaoModel> extensoes = new List<ExtensaoModel>();
            m_dbHelper.Connection.Open();
            using (SQLiteCommand cmd = new SQLiteCommand(strSQL, m_dbHelper.Connection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (reader.Read())
                    {
                        ExtensaoModel extensao = new ExtensaoModel
                        (
                            reader.GetString(0)
                        );
                        extensoes.Add(extensao);
                    }
                }
            }
            m_dbHelper.Connection.Close();
            return extensoes;
        }
    }
}
