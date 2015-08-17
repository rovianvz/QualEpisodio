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
    public class BibliotecaDAL
    {
        private DBHelper m_dbHelper;

        public BibliotecaDAL()
        {
            m_dbHelper = DBHelper.Instance;
        }

        public void InsereBiblioteca(BibliotecaModel _pasta)
        {
            string strSQL = string.Format("INSERT INTO BIBLIOTECA (DIRETORIO) VALUES ('{0}')",
                                                             _pasta.Diretorio);


            m_dbHelper.Connection.Open();
            using (SQLiteCommand cmd = new SQLiteCommand(strSQL, m_dbHelper.Connection))
            {
                cmd.ExecuteNonQuery();
            }
            m_dbHelper.Connection.Close();
        }

        public List<BibliotecaModel> ListBibliotecas()
        {
            string strSQL = "SELECT * FROM BIBLIOTECA";

            List<BibliotecaModel> diretorios = new List<BibliotecaModel>();
            m_dbHelper.Connection.Open();
            using (SQLiteCommand cmd = new SQLiteCommand(strSQL, m_dbHelper.Connection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (reader.Read())
                    {
                        BibliotecaModel diretorio = new BibliotecaModel
                        (
                            reader.GetString(0)
                        );
                        diretorios.Add(diretorio);
                    }
                }
            }
            m_dbHelper.Connection.Close();
            return diretorios;
        }
    }
}
