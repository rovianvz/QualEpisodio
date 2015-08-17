using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;

namespace QualEpisodio
{
    public class DBHelper
    {
        private static DBHelper _instance;
        private static SQLiteConnection connection;

        //Constructor
        public DBHelper()
        {
            if (!Directory.Exists("Data"))
                Directory.CreateDirectory("Data");
            if(!File.Exists("Data\\qe.db"))
                CriaBanco();

            Initialize();
        }

        private void CriaBanco()
        {
            SQLiteConnection.CreateFile("Data\\qe.db");
            Initialize();

            string strSQL = @"CREATE TABLE Series (
                                Id 	 INTEGER 	  PRIMARY KEY AUTOINCREMENT,
                                Serie 	 VARCHAR(300) UNIQUE NOT NULL,
                                Imagem 	 BLOB 	,
                                Alteracao 	 DATETIME 	   NOT NULL
                                );
                            CREATE TABLE Assistidas (
                            Id INTEGER              ,
                            Temporada 	 INTEGER 	,
                            Episodio 	 INTEGER 	,
                            FOREIGN KEY(Id) REFERENCES Series(Id) ON DELETE CASCADE
                            );
                            CREATE TABLE Baixadas (
                            Id INTEGER              ,
                            Temporada 	 INTEGER 	,
                            Episodio 	 INTEGER 	,
                            FOREIGN KEY(Id) REFERENCES Series(Id) ON DELETE CASCADE
                            );";

            if (this.OpenConnection())
            {
                SQLiteCommand command = new SQLiteCommand("", connection);
                command.CommandText = strSQL;

                command.ExecuteNonQuery();

                connection.Close();
            }


        }

        public static DBHelper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DBHelper();

                return _instance;
            }
        }


        //Initialize values
        private void Initialize()
        {
            string connectionString = @"Data Source=Data\qe.db";

            if(connection == null)
                connection = new SQLiteConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch 
            {
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch 
            {
                return false;
            }
        }

        //Select statement
        public List<QualEpisodioModel> Select(bool _assistida = true)
        {
            string table;
            if (_assistida)
                table = "Assistidas";
            else
                table = "Baixadas";
            string query = string.Format(@"SELECT Serie, {0}.Temporada, {0}.Episodio FROM Series
                                           JOIN {0} ON Series.Id = {0}.Id", table);

            //Create a list to store the result
            List<QualEpisodioModel> list = new List<QualEpisodioModel>();
            
            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                //Create a data reader and Execute the command
                SQLiteDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    QualEpisodioModel serie = new QualEpisodioModel();
                    serie.Serie = dataReader.GetString(0);
                    serie.Temporada = dataReader.GetInt32(1);
                    serie.Episodio = dataReader.GetInt32(2);
                    list.Add(serie);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        //Select statement
        public List<String> SelectAllTitles()
        {
            string query = @"SELECT Serie FROM Series";

            //Create a list to store the result
            List<String> list = new List<String>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                //Create a data reader and Execute the command
                SQLiteDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list.Add(dataReader.GetString(0));
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        public QualEpisodioModel Select(string _serie, bool _assistida = true)
        {
            string table;
            if (_assistida)
                table = "Assistidas";
            else
                table = "Baixadas";
            
            string query = string.Format(@"SELECT Serie, {0}.Temporada, {0}.Episodio, Imagem FROM Series
                                            JOIN {0} ON Series.Id = {0}.Id
                                            WHERE Serie = '{1}'", table, _serie);

            QualEpisodioModel serie = new QualEpisodioModel();
            
            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                //Create a data reader and Execute the command
                SQLiteDataReader dataReader = cmd.ExecuteReader();

                
                    
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    serie.Serie = dataReader.GetString(0);
                    serie.Temporada = dataReader.GetInt32(1);
                    serie.Episodio = dataReader.GetInt32(2);
                    if (dataReader.GetValue(3) != DBNull.Value)
                        serie.Imagem = (byte[])dataReader.GetValue(3);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return serie;
            }
            else
            {
                return serie;
            }
        }

        public void Insert(QualEpisodioModel _serie)
        {
            string query = @"INSERT INTO Series (Serie, Imagem, Alteracao) 
                           VALUES (@serie, @image, 'now');
                           
                           INSERT INTO Assistidas (Id, Temporada, Episodio)
                           VALUES ((SELECT Id FROM Series WHERE Serie = @serie), @temporada, @episodio);
                           
                           INSERT INTO Baixadas (Id, Temporada, Episodio)
                           VALUES ((SELECT Id FROM Series WHERE Serie = @serie), @temporada, @episodio);";

            if (this.OpenConnection())
            {
                SQLiteCommand command = new SQLiteCommand("", connection);
                command.CommandText = query;

                command.Parameters.AddWithValue("@serie", _serie.Serie);
                command.Parameters.AddWithValue("@temporada", _serie.Temporada);
                command.Parameters.AddWithValue("@episodio", _serie.Episodio);
                command.Parameters.AddWithValue("@image", _serie.Imagem);

                command.ExecuteNonQuery();

                connection.Close();
            }
                            
        }


        public void Update(QualEpisodioModel _serie, String _oldName, bool _assistida = true)
        {
            string table;
            if (_assistida)
                table = "Assistidas";
            else
                table = "Baixadas";
            
            
            string query = string.Format(@"UPDATE Series 
                                           SET Serie = @serie, Imagem = @imagem, Alteracao = 'now'  
                                           WHERE Serie = @oldName;
                                           UPDATE {0} SET
                                           Temporada = @temporada, Episodio = @episodio
                                           WHERE Id = (SELECT Id FROM Series WHERE Serie = @serie);", table);

            if (this.OpenConnection())
            {
                SQLiteCommand command = new SQLiteCommand("", connection);
                command.CommandText = query;

                command.Parameters.AddWithValue("@serie", _serie.Serie);
                command.Parameters.AddWithValue("@temporada", _serie.Temporada);
                command.Parameters.AddWithValue("@episodio", _serie.Episodio);
                command.Parameters.AddWithValue("@imagem", _serie.Imagem);
                command.Parameters.AddWithValue("@oldName", _oldName);

                command.ExecuteNonQuery();

                connection.Close();
            }

        }

        public void Update(QualEpisodioModel _serie, String _oldName)
        {
            string query = @"UPDATE Series 
                            SET Serie = @serie, Imagem = @imagem, Alteracao = 'now'  
                            WHERE Serie = @oldName;";

            if (this.OpenConnection())
            {
                SQLiteCommand command = new SQLiteCommand("", connection);
                command.CommandText = query;

                command.Parameters.AddWithValue("@serie", _serie.Serie);
                command.Parameters.AddWithValue("@imagem", _serie.Imagem);
                command.Parameters.AddWithValue("@oldName", _oldName);

                command.ExecuteNonQuery();

                connection.Close();
            }

        }


        public void Delete(String _Name)
        {
            string query = "DELETE FROM Series " +
                           "WHERE Serie = @name";

            if (this.OpenConnection())
            {
                SQLiteCommand command = new SQLiteCommand("", connection);
                command.CommandText = query;

                command.Parameters.AddWithValue("@name", _Name);

                command.ExecuteNonQuery();

                connection.Close();
            }

        }

    }
}
