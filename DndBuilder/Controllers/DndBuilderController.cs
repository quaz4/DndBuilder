using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DndBuilder.Model;
using Mono.Data.Sqlite;
using Newtonsoft.Json.Linq;

namespace DndBuilder.Controllers
{
    public class DndBuilderController : ApiController
    {
        public DndBuilderController()
        {
        }

        //[HttpGet]
        //[Route("character/{name}")]
        //public int Get()
        //{
        //    IDbCommand dbcmd = dbcon.CreateCommand();
        //    const string sql =
        //    "SELECT \n    name\nFROM \n    sqlite_master \nWHERE \n    type ='table' AND \n    name NOT LIKE 'sqlite_%';";
        //    //"CREATE TABLE Persons (PersonID int, LastName varchar(255), FirstName varchar(255), Address varchar(255), City varchar(255));";
        //    dbcmd.CommandText = sql;
        //    IDataReader reader = dbcmd.ExecuteReader();

        //    Console.WriteLine(reader.GetSchemaTable());

        //    while (reader.Read())
        //    {
        //        Console.WriteLine(reader.GetString(0));
        //    }
        //    // clean up
        //    reader.Dispose();
        //    dbcmd.Dispose();
        //    dbcon.Close();


        //    return 6;
        //}

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
        public void Post([FromBody]Dictionary<string, object> req)
        {
            try
            {
                JArray jArray = JArray.Parse(req["userPoints"].ToString());
                int[] ap = jArray.ToObject<int[]>();

                Character newCharacter = new Character(
                    req["name"].ToString(),
                    Convert.ToInt32(req["age"]), //Age
                    req["gender"].ToString(),
                    req["biography"].ToString(),
                    Convert.ToInt32(req["level"]), //Level
                    req["characterClass"].ToString(),
                    req["characterRace"].ToString(),
                    ap
                );

                Database db = new Database();
                db.Insert(newCharacter);
            }
            // TODO
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /* Fuction: getImage
         * Type: Post
         * Parameters: zoom level, x coordinate, y coordinate
         * Return: byte[]
         * Assertion: function returns a dummy image, regardless of input.
         * Note: Post version of the previous method.
         */
        [HttpPut]
        [Route("character")]
        public void UpdateCharacter([FromBody]Dictionary<string, object> req)
        {
            try
            {
                JArray jArray = JArray.Parse(req["userPoints"].ToString());
                int[] ap = jArray.ToObject<int[]>();

                Character newCharacter = new Character(
                    req["name"].ToString(),
                    Convert.ToInt32(req["age"]), //Age
                    req["gender"].ToString(),
                    req["biography"].ToString(),
                    Convert.ToInt32(req["level"]), //Level
                    req["characterClass"].ToString(),
                    req["characterRace"].ToString(),
                    ap
                );

                Database db = new Database();
                db.Update(newCharacter);
            }
            // TODO
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [HttpGet]
        [Route("character/exists/{name}")]
        public bool Exists(string name)
        {
            bool exists = false;

            try
            {
                Database db = new Database();
                exists = db.Exists(name);
            }
            // TODO
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return exists;
        }

        /* Fuction: getImage
         * Type: Post
         * Parameters: zoom level, x coordinate, y coordinate
         * Return: byte[]
         * Assertion: function returns a dummy image, regardless of input.
         * Note: Post version of the previous method.
         */
        [HttpGet]
        [Route("classes")]
        public string GetClasses()
        {
            try
            {
                Dnd5eApi api = new Dnd5eApi(Constants.API_URL);
                JObject result = api.GetClasses();

                return result.ToString(Newtonsoft.Json.Formatting.None);
            }
            catch
            {
                throw new HttpResponseException(this.Request.CreateResponse<object>
                    (HttpStatusCode.InternalServerError, "Error occurred: An unknown error has occured"));  
            }

        }

        /* Fuction: getImage
         * Type: Post
         * Parameters: zoom level, x coordinate, y coordinate
         * Return: byte[]
         * Assertion: function returns a dummy image, regardless of input.
         * Note: Post version of the previous method.
         */
        [HttpGet]
        [Route("class/{dndClass}")]
        public string GetClass(string dndClass)
        {
            try
            {
                Dnd5eApi api = new Dnd5eApi(Constants.API_URL);
                JObject result = api.GetClass(dndClass);

                return result.ToString(Newtonsoft.Json.Formatting.None);
            }
            catch
            {
                // TODO
                throw new HttpResponseException(this.Request.CreateResponse<object>
                    (HttpStatusCode.InternalServerError, "Error occurred: An unknown error has occured"));
            }

        }

        /* Fuction: getImage
         * Type: Post
         * Parameters: zoom level, x coordinate, y coordinate
         * Return: byte[]
         * Assertion: function returns a dummy image, regardless of input.
         * Note: Post version of the previous method.
         */
        [HttpGet]
        [Route("races")]
        public string GetRaces()
        {
            try
            {
                Dnd5eApi api = new Dnd5eApi(Constants.API_URL);
                JObject result = api.GetRaces();

                return result.ToString(Newtonsoft.Json.Formatting.None);
            }
            catch
            {
                // TODO
                throw new HttpResponseException(this.Request.CreateResponse<object>
                    (HttpStatusCode.InternalServerError, "Error occurred: An unknown error has occured"));
            }
        }

        /* Fuction: getImage
         * Type: Post
         * Parameters: zoom level, x coordinate, y coordinate
         * Return: byte[]
         * Assertion: function returns a dummy image, regardless of input.
         * Note: Post version of the previous method.
         */
        [HttpGet]
        [Route("race/{dndRace}")]
        public string GetRace(string dndRace)
        {
            try
            {
                Dnd5eApi api = new Dnd5eApi(Constants.API_URL);
                JObject result = api.GetRace(dndRace);

                return result.ToString(Newtonsoft.Json.Formatting.None);
            }
            catch
            {
                // TODO
                throw new HttpResponseException(this.Request.CreateResponse<object>
                    (HttpStatusCode.InternalServerError, "Error occurred: An unknown error has occured"));
            }

        }
    }
}
