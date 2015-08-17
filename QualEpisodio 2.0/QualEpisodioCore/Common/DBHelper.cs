using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;

namespace QualEpisodioCore.Common
{
    public class DBHelper
    {
        private static DBHelper m_instance;
        private static SQLiteConnection m_udtConnection;

        public SQLiteConnection Connection
        {
            get { return DBHelper.m_udtConnection; }
        }

        //Constructor
        public DBHelper()
        {
            if (!Directory.Exists("Data"))
                Directory.CreateDirectory("Data");
            if(!File.Exists(@"Data\qe.db"))
                CriaBanco();

            Initialize();
        }

        private void CriaBanco()
        {
            SQLiteConnection.CreateFile(@"Data\qe.db");
            Initialize();

            string strSQL = @"CREATE TABLE SERIE (
                                NOME VARCHAR(300) PRIMARY KEY,
                                ULTIMO_BAIXADO_TEMPORADA INTEGER,
                                ULTIMO_BAIXADO_EPISODIO INTEGER,
                                ULTIMO_ASSISTIDO_TEMPORADA INTEGER,
                                ULTIMO_ASSISTIDO_EPISODIO INTEGER,
                                CAMINHO_IMAGEM VARCHAR(300)
                            );
                            
                            CREATE TABLE EPISODIO (
                                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                                TEMPORADA INTEGER,
                                EPISODIO INTEGER,
                                SERIE INTEGER,
                                CAMINHO_IMAGEM VARCHAR(300),
                                CAMINHO_VIDEO VARCHAR(300),
                                FOREIGN KEY(SERIE) REFERENCES SERIE(NOME) ON DELETE CASCADE
                            );
                            
                            CREATE TABLE BIBLIOTECA (
                                DIRETORIO VARCHAR(300) PRIMARY KEY
                            );

                            CREATE TABLE EXTENSAO (
                                EXTENSAO VARCHAR(10) PRIMARY KEY
                            );

                            INSERT INTO EXTENSAO (EXTENSAO) VALUES ('mkv');
                            INSERT INTO EXTENSAO (EXTENSAO) VALUES ('rmvb');
                            INSERT INTO EXTENSAO (EXTENSAO) VALUES ('mp4');
                            INSERT INTO EXTENSAO (EXTENSAO) VALUES ('avi');
                            INSERT INTO EXTENSAO (EXTENSAO) VALUES ('mpeg');
                            INSERT INTO EXTENSAO (EXTENSAO) VALUES ('mpg')";

            m_udtConnection.Open();
            SQLiteCommand command = new SQLiteCommand("", m_udtConnection);
            command.CommandText = strSQL;
            command.ExecuteNonQuery();
            m_udtConnection.Close();
        }

        public static DBHelper Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new DBHelper();

                return m_instance;
            }
        }


        //Initialize values
        private void Initialize()
        {
            string connectionString = @"Data Source=Data\qe.db";

            if(m_udtConnection == null)
                m_udtConnection = new SQLiteConnection(connectionString);
        }
    }
}
