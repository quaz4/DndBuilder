using System;
using System.Data;
using System.IO;
using DndBuilder.Model;
using Mono.Data.Sqlite;
using Newtonsoft.Json.Linq;

namespace DndBuilder
{
    public class Database
    {
        public const string DB_NAME = "DndBuilder.sqlite";
        IDbConnection dbcon;

        public Database()
        {
            // Connect to DB
            const string connectionString = "URI=file:DndBuilder.db";
            this.dbcon = new SqliteConnection(connectionString);
            this.dbcon.Open();
        }

        public bool IsSetup()
        {
            Console.WriteLine("In function IsSetup");
            IDbCommand dbcmd = this.dbcon.CreateCommand();

            // Checks if any tables exist
            const string sql =
            "SELECT name " +
            "FROM sqlite_master " +
            "WHERE type='table' " +
            "AND name NOT LIKE 'sqlite_%';";

            dbcmd.CommandText = sql;
            Console.WriteLine("Created command");

            bool characterTable = false;

            try 
            {
                IDataReader reader = dbcmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(reader.GetString(0));
                    if (reader.GetString(0) == "Characters")
                    {
                        characterTable = true;
                    }
                }

                // clean up reader
                reader.Dispose();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Executed command");

            bool rVal = false;

            // If character table exists, return true
            if (characterTable)
            {
                rVal = true;
            }

            // clean up
            dbcmd.Dispose();

            Console.WriteLine("DB VALID: " + rVal);

            return rVal;
        }

        public void SetupDatabase()
        {
            IDbCommand dbcmd = dbcon.CreateCommand();

            string sql =
                "CREATE TABLE Characters (" +
                    "character_id INT NOT NULL UNIQUE PRIMARY KEY," +
                    "name TEXT NOT NULL," +
                    "gender TEXT NOT NULL," +
                    "biography CHAR(500) NOT NULL," +
                    "level INT NOT NULL CHECK(level > 0 AND level < 21)," +
                    "race TEXT NOT NULL," +
                    "class TEXT NOT NULL," +
                    "spellcaster BOOLEAN NOT NULL," +
                    "hit_points INT NOT NULL," +
                    "constitution INT NOT NULL," +
                    "dexterity INT NOT NULL," +
                    "strength INT NOT NULL," +
                    "charisma INT NOT NULL," +
                    "inteligence INT NOT NULL," +
                    "wisdom INT NOT NULL," +

                    "UNIQUE(name COLLATE NOCASE)" +
                ");";

            dbcmd.CommandText = sql;


            try
            {
                int result = dbcmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            // clean up
            dbcmd.Dispose();
        }

        public Character Find(string name)
        {
            return null;
        }

        public bool Exists(string name)
        {
            return false;
        }

        public void CloseConnection() 
        {
            dbcon.Close();
        }

        ~Database()
        {
            // TODO: Change this, amy said its...iffy at best
            CloseConnection();
        }
    }
}
