using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
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

        /* Fuction: getImage
         * Type: Post
         * Parameters: zoom level, x coordinate, y coordinate
         * Return: byte[]
         * Assertion: function returns a dummy image, regardless of input.
         * Note: Post version of the previous method.
         */
        [HttpPost]
        [Route("character")]
        public IHttpActionResult Post([FromBody]Dictionary<string, object> req)
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

            return StatusCode(HttpStatusCode.Created);
        }

        [HttpGet]
        [Route("character/page/{page}")]
        public string ListPage(int page)
        {


            try
            {
                Database db = new Database();
                return db.ListPage(page).ToString(Formatting.None);
            }
            // TODO
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
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

        [HttpDelete]
        [Route("character/{name}")]
        public void DeleteCharacter(string name)
        {
            try
            {
                Database db = new Database();
                db.Delete(name);
            }
            // TODO
            catch (Exception e)
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
