using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using DndBuilder.Model;
using Mono.Data.Sqlite;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DndBuilder.Controllers
{
    public class DndBuilderController : ApiController
    {
        public DndBuilderController()
        {
        }

        /*
         * Inserts a new character object into the db       
         * Type: Post
         * Params: Character object
         * Returns: None
         */
        [HttpPost]
        [Route("character")]
        public IHttpActionResult Post([FromBody]Dictionary<string, object> req)
        {
            try
            {
                JArray jArray = JArray.Parse(req["userPoints"].ToString());
                int[] ap = jArray.ToObject<int[]>();

                // Convert to character object
                Character newCharacter = new Character(
                    req["name"].ToString(),
                    Convert.ToInt32(req["age"]), //Age
                    req["gender"].ToString(),
                    req["biography"].ToString(),
                    Convert.ToInt32(req["level"]), //Level
                    req["characterRace"].ToString(),
                    req["characterClass"].ToString(),
                    ap
                );

                Database db = new Database();
                db.Insert(newCharacter);

                Ok(); // Return 200
            }
            catch(ArgumentOutOfRangeException e)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            catch (SqliteException e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
            catch (ArgumentException e)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            return StatusCode(HttpStatusCode.Created);
        }

        /*
         * Fetches a page of results     
         * Type: Get
         * Params: page int
         * Returns: JSON containing min 0 max 10 pages
         */
        [HttpGet]
        [Route("character/page/{page}")]
        public string ListPage(int page)
        {
            try
            {
                Database db = new Database();
                return db.ListPage(page).ToString();
            }
            catch (SqliteException e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
            catch (ArgumentException e)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        /*
         * Updates an existing character in the DB     
         * Type: Put
         * Params: Character object
         * Returns: None
         */
        [HttpPut]
        [Route("character")]
        public IHttpActionResult UpdateCharacter([FromBody]Dictionary<string, object> req)
        {
            try
            {
                JArray jArray = JArray.Parse(req["userPoints"].ToString());
                int[] ap = jArray.ToObject<int[]>();

                // Convert to Character object
                Character newCharacter = new Character(
                    req["name"].ToString(),
                    Convert.ToInt32(req["age"]), //Age
                    req["gender"].ToString(),
                    req["biography"].ToString(),
                    Convert.ToInt32(req["level"]), //Level
                    req["characterRace"].ToString(),
                    req["characterClass"].ToString(),
                    ap
                );

                Database db = new Database();
                db.Update(newCharacter);

                return StatusCode(HttpStatusCode.OK);
            }
            catch (SqliteException e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
            catch (ArgumentException e)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        /*
         * Gets an existing record in the array
         * Type: Get
         * Params: Character object
         * Returns: JSON object with character info
         */
        [HttpGet]
        [Route("character/{name}")]
        public string Fetch(string name)
        {
            try
            {
                Database db = new Database();
                Character rVal = db.Fetch(HttpUtility.HtmlDecode(name));

                return rVal.ToJson().ToString();
            }
            catch (SqliteException e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
            catch (ArgumentException e)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        /*
         * Returns true/false if name exists/doesn't exist   
         * Type: Get
         * Params: name string
         * Returns: boolean
         */
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
            catch (SqliteException e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
            catch (ArgumentException e)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            return exists;
        }

        /*
         * Deletes a record based on name 
         * Type: Delete
         * Params: name string
         * Returns: None
         */
        [HttpDelete]
        [Route("character/{name}")]
        public IHttpActionResult DeleteCharacter(string name)
        {
            try
            {
                Database db = new Database();
                db.Delete(HttpUtility.HtmlDecode(name));

                return StatusCode(HttpStatusCode.OK);
            }
            catch (SqliteException e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
            catch (ArgumentException e)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        /*
         * Contacts an external API and returns a list of classes
         * Type: Get
         * Params: None
         * Returns: JSON
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
            catch (SqliteException e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
            catch (ArgumentException e)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

        }

        /*
         * Gets a specific class from an external API
         * Type: Get
         * Params: None
         * Returns: JSON
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
            catch (SqliteException e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
            catch (ArgumentException e)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

        }

        /*
         * Contacts an external API and returns a list of races
         * Type: Get
         * Params: None
         * Returns: JSON
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
            catch (SqliteException e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
            catch (ArgumentException e)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        /*
         * Contacts an external API and specific race
         * Type: Get
         * Params: dndRace string
         * Returns: JSON
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
            catch (SqliteException e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
            catch (ArgumentException e)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}
