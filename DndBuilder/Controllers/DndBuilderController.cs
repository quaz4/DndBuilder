using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using Mono.Data.Sqlite;

namespace DndBuilder.Controllers
{
    public class DndBuilderController : ApiController
    {
        private IDbConnection dbcon;

        public DndBuilderController() {

            const string connectionString = "URI=file:DndBuilder.db";
            dbcon = new SqliteConnection(connectionString);
            dbcon.Open();
        }

        [HttpGet]
        [Route("character/{name}")] 
        public int Get()
        {
            IDbCommand dbcmd = dbcon.CreateCommand();
            const string sql =
            "SELECT \n    name\nFROM \n    sqlite_master \nWHERE \n    type ='table' AND \n    name NOT LIKE 'sqlite_%';";
            //"CREATE TABLE Persons (PersonID int, LastName varchar(255), FirstName varchar(255), Address varchar(255), City varchar(255));";
            dbcmd.CommandText = sql;
            IDataReader reader = dbcmd.ExecuteReader();

            Console.WriteLine(reader.GetSchemaTable());

            while (reader.Read())
            {
                Console.WriteLine(reader.GetString(0));
            }
            // clean up
            reader.Dispose();
            dbcmd.Dispose();
            dbcon.Close();


            return 6;
        }

        [HttpPut]
        [Route("character/{name}")]
        public int Put([FromBody]Dictionary<string, object> req)
        {
            return 6;
        }

        /* Fuction: getImage
         * Type: Post
         * Parameters: zoom level, x coordinate, y coordinate
         * Return: byte[]
         * Assertion: function returns a dummy image, regardless of input.
         * Note: Post version of the previous method.
         */
        [HttpPost]
        [Route("character")]
        public int Post([FromBody]Dictionary<string, object> req)
        {
            Console.WriteLine(req);
            return 200;
        }
    }
}
