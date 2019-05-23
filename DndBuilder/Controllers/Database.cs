﻿using System;
//using System.Data;
using DndBuilder.Model;
using Mono.Data.Sqlite;
using Newtonsoft.Json.Linq;

namespace DndBuilder
{
    public class Database
    {
        public Database() {}

        public bool IsSetup()
        {
            Console.WriteLine("In function IsSetup");
            // Connect to DB
            SqliteConnection m_dbConn = new SqliteConnection("Data Source=" + Constants.DB_NAME + "; Version=3;");
            m_dbConn.Open();

            // Checks if any tables exist
            const string sql =
            "SELECT name " +
            "FROM sqlite_master " +
            "WHERE type='table' " +
            "AND name NOT LIKE 'sqlite_%';";

            SqliteCommand dbcmd = new SqliteCommand(sql, m_dbConn);

            bool characterTable = false;

            try 
            {
                SqliteDataReader reader = dbcmd.ExecuteReader();

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
            // TODO
            catch (Exception e) when (e is NullReferenceException || e is SqliteException
            || e is InvalidCastException)
            {
                //exception handling goes here
                Console.WriteLine("It went wrong boss");
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
            m_dbConn.Close();

            Console.WriteLine("DB VALID: " + rVal);

            return rVal;
        }

        public void SetupDatabase()
        {
            try 
            { 
                // Connect to DB
                SqliteConnection m_dbConn = new SqliteConnection("Data Source =" + Constants.DB_NAME + "; Version=3;");
                m_dbConn.Open();

                string sql =
                    "CREATE TABLE Characters (" +
                        "character_id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                        "name TEXT NOT NULL," +
                        "age INT NOT NULL CHECK(age > 0 AND age < 500)," +
                        "gender TEXT NOT NULL," +
                        "biography CHAR(500) NOT NULL," +
                        "level INT NOT NULL CHECK(level > 0 AND level < 21)," +
                        "race TEXT NOT NULL," +
                        "class TEXT NOT NULL," +
                        "constitution INT NOT NULL," +
                        "dexterity INT NOT NULL," +
                        "strength INT NOT NULL," +
                        "charisma INT NOT NULL," +
                        "intelligence INT NOT NULL," +
                        "wisdom INT NOT NULL," +

                        "UNIQUE(name COLLATE NOCASE)" +
                    ");";

                SqliteCommand dbcmd = new SqliteCommand(sql, m_dbConn);
            
                int result = dbcmd.ExecuteNonQuery();

                dbcmd.Dispose();
                m_dbConn.Close();
            }
            // TODO
            catch (SqliteException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Insert(Character character)
        {
            try
            {
                //Crease the database connection object – assumes the database has been created.
                using(SqliteConnection m_dbConn = new SqliteConnection("Data Source =" + Constants.DB_NAME + "; Version=3;"))
                {
                    m_dbConn.Open();

                    // Set up the query with place holders
                    SqliteCommand insertSQL = new SqliteCommand(
                        "INSERT INTO Characters( " +
                            "name," +
                            "age," +
                            "gender," +
                            "biography," +
                            "class," +
                            "race," +
                            "level," +
                            "constitution," +
                            "dexterity," +
                            "strength," +
                            "charisma," +
                            "intelligence," +
                            "wisdom) " +
                        "VALUES(" +
                            "@name, " +
                            "@age, " +
                            "@gender, " +
                            "@biography, " +
                            "@class, " +
                            "@race, " +
                            "@level, " +
                            "@constitution, " +
                            "@dexterity, " +
                            "@strength, " +
                            "@charisma, " +
                            "@intelligence, " +
                            "@wisdom " +
                        ")", m_dbConn);

                    insertSQL.Parameters.Add(new SqliteParameter("name", character.GetName()));
                    insertSQL.Parameters.Add(new SqliteParameter("age", character.GetAge()));
                    insertSQL.Parameters.Add(new SqliteParameter("gender", character.GetGender()));
                    insertSQL.Parameters.Add(new SqliteParameter("biography", character.GetBiography()));
                    insertSQL.Parameters.Add(new SqliteParameter("class", character.GetClass()));
                    insertSQL.Parameters.Add(new SqliteParameter("race", character.GetRace()));
                    insertSQL.Parameters.Add(new SqliteParameter("level", character.GetLevel()));

                    insertSQL.Parameters.Add(new SqliteParameter("constitution", character.GetConstitution()));
                    insertSQL.Parameters.Add(new SqliteParameter("dexterity", character.GetDexterity()));
                    insertSQL.Parameters.Add(new SqliteParameter("strength", character.GetStrength()));
                    insertSQL.Parameters.Add(new SqliteParameter("charisma", character.GetCharisma()));
                    insertSQL.Parameters.Add(new SqliteParameter("intelligence", character.Intelligence));
                    insertSQL.Parameters.Add(new SqliteParameter("wisdom", character.GetWisdom()));

                    insertSQL.ExecuteNonQuery(); //execute the query

                    m_dbConn.Close();
                }
            }
            catch (Exception e) when (e is SqliteException || e is InvalidCastException)
            {
                //error handling goes here
                Console.WriteLine(e);
            }
        }

        public void Update(Character character)
        {
            try
            {
                //Crease the database connection object – assumes the database has been created.
                using (SqliteConnection m_dbConn = new SqliteConnection("Data Source =" + Constants.DB_NAME + "; Version=3;"))
                {
                    m_dbConn.Open();

                    // Set up the query with place holders
                    SqliteCommand insertSQL = new SqliteCommand(
                        "UPDATE Characters( " +
                            "name = @name" +
                            "age = @age," +
                            "gender = @gender," +
                            "biography = @biography," +
                            "class = @class," +
                            "race = @race," +
                            "level = @level," +
                            "constitution = @constitution," +
                            "dexterity = @dexterity," +
                            "strength = @strength," +
                            "charisma = @charisma," +
                            "intelligence = @intelligence," +
                            "wisdom = @wisdom" +
                        ") ", m_dbConn);

                    insertSQL.Parameters.Add(new SqliteParameter("name", character.GetName()));
                    insertSQL.Parameters.Add(new SqliteParameter("age", character.GetAge()));
                    insertSQL.Parameters.Add(new SqliteParameter("gender", character.GetGender()));
                    insertSQL.Parameters.Add(new SqliteParameter("biography", character.GetBiography()));
                    insertSQL.Parameters.Add(new SqliteParameter("class", character.GetClass()));
                    insertSQL.Parameters.Add(new SqliteParameter("race", character.GetRace()));
                    insertSQL.Parameters.Add(new SqliteParameter("level", character.GetLevel()));

                    insertSQL.Parameters.Add(new SqliteParameter("constitution", character.GetConstitution()));
                    insertSQL.Parameters.Add(new SqliteParameter("dexterity", character.GetDexterity()));
                    insertSQL.Parameters.Add(new SqliteParameter("strength", character.GetStrength()));
                    insertSQL.Parameters.Add(new SqliteParameter("charisma", character.GetCharisma()));
                    insertSQL.Parameters.Add(new SqliteParameter("intelligence", character.Intelligence));
                    insertSQL.Parameters.Add(new SqliteParameter("wisdom", character.GetWisdom()));

                    insertSQL.ExecuteNonQuery(); //execute the query

                    m_dbConn.Close();
                }
            }
            catch (Exception e) when (e is SqliteException || e is InvalidCastException)
            {
                //error handling goes here
                Console.WriteLine(e);
            }
        }

        public Character Fetch(string name)
        {
            Character character = null;

            try
            {
                //Crease the database connection object – assumes the database has been created.
                using (SqliteConnection m_dbConn = new SqliteConnection("Data Source =" + Constants.DB_NAME + "; Version=3;"))
                {
                    m_dbConn.Open();

                    // Set up the query with place holders
                    SqliteCommand existsSQL = new SqliteCommand(
                        "SELECT" +
                            "name," +
                            "age," +
                            "gender," +
                            "biography," +
                            "class," +
                            "race," +
                            "level," +
                            "constitution," +
                            "dexterity," +
                            "strength," +
                            "charisma," +
                            "intelligence," +
                            "wisdom" +
                        "FROM Characters " +
                        "WHERE name LIKE @name"
                    , m_dbConn);

                    existsSQL.Parameters.Add(new SqliteParameter("name", name));

                    SqliteDataReader reader = existsSQL.ExecuteReader(); //execute the query;

                    reader.Read();

                    int[] ap = {
                            reader.GetInt32(7), // Constitution
                            reader.GetInt32(8), // Dexterity
                            reader.GetInt32(9), // Strength
                            reader.GetInt32(10), // Charisma
                            reader.GetInt32(11), // Intelligence
                            reader.GetInt32(12) // Wisdom
                    };

                    character = new Character(
                        reader.GetString(0), // Name
                        reader.GetInt32(1), // Age
                        reader.GetString(2), // Gender
                        reader.GetString(3), // Biography
                        reader.GetInt32(4), // Level
                        reader.GetString(5), // Race
                        reader.GetString(6), // Class
                        ap
                    );

                    // clean up reader
                    reader.Dispose();

                    m_dbConn.Close();
                }
            }
            catch (Exception e) when (e is SqliteException || e is InvalidCastException)
            {
                // TODO: error handling goes here
                Console.WriteLine(e);
                character = null;
            }

            return character;
        }

        public bool Exists(string name)
        {
            bool found = false;

            try
            {
                //Crease the database connection object – assumes the database has been created.
                using (SqliteConnection m_dbConn = new SqliteConnection("Data Source =" + Constants.DB_NAME + "; Version=3;"))
                {
                    m_dbConn.Open();

                    // Set up the query with place holders
                    SqliteCommand existsSQL = new SqliteCommand(
                        "SELECT COUNT(name) as found FROM Characters " +
                        "WHERE name LIKE @name"
                    , m_dbConn);

                    existsSQL.Parameters.Add(new SqliteParameter("name", name));

                    SqliteDataReader reader = existsSQL.ExecuteReader(); //execute the query;

                    reader.Read();

                    if(reader.GetInt32(0) > 0)
                    {
                        found = true;
                    }

                    // clean up reader
                    reader.Dispose();

                    m_dbConn.Close();
                }
            }
            catch (Exception e) when (e is SqliteException || e is InvalidCastException)
            {
                // TODO: error handling goes here
                Console.WriteLine(e);
            }

            return found;
        }

        public void Delete(string name) 
        {
            try
            {
                //Crease the database connection object – assumes the database has been created.
                using (SqliteConnection m_dbConn = new SqliteConnection("Data Source =" + Constants.DB_NAME + ";Version=3;"))
                {
                    m_dbConn.Open();

                    // Set up the query with place holders
                    SqliteCommand deleteSQL = new SqliteCommand(
                        "DELETE FROM Characters " +
                        "WHERE name LIKE @name"
                    , m_dbConn);

                    deleteSQL.Parameters.Add(new SqliteParameter("name", name));

                    deleteSQL.ExecuteNonQuery(); //execute the query

                    m_dbConn.Close();
                }
            }
            catch (Exception e) when (e is SqliteException || e is InvalidCastException)
            {
                //error handling goes here
                Console.WriteLine(e);
            }
        }

        public JObject ListPage(int page)
        {
            JObject rVal = new JObject();
            JArray jArray = new JArray();

            try
            {
                //Crease the database connection object – assumes the database has been created.
                using (SqliteConnection m_dbConn = new SqliteConnection("Data Source =" + Constants.DB_NAME + "; Version=3;"))
                {
                    m_dbConn.Open();

                    // Set up the query with place holders
                    SqliteCommand existsSQL = new SqliteCommand(
                        "SELECT " +
                            "name, " +
                            "class, " +
                            "race, " +
                            "level " +
                        "FROM Characters " +
                        "LIMIT " + Constants.PAGE_LIMIT + " " +
                        "OFFSET " + Constants.PAGE_LIMIT * page
                    , m_dbConn);

                    SqliteDataReader reader = existsSQL.ExecuteReader(); //execute the query;

                    while(reader.Read())
                    {
                        jArray.Add(new JObject(
                            new JProperty("name", reader.GetString(0)),
                            new JProperty("class", reader.GetString(1)),
                            new JProperty("race", reader.GetString(2)),
                            new JProperty("level", reader.GetInt32(3))     
                        ));
                    }

                    // clean up reader
                    reader.Dispose();

                    m_dbConn.Close();
                }
            }
            catch (Exception e) when (e is SqliteException || e is InvalidCastException)
            {
                // TODO: error handling goes here
                Console.WriteLine(e);
            }

            rVal.Add(new JProperty("results", jArray));

            return rVal;
        }
    }
}
